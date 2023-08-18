using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeAss
{
    public static class RepoToGpt
    {
        /*
            The goal is to modify the TraverseAndConvert function such that it prepares the content for upload to ChatGPT in chunks. 
        
            Moreover, you'd want the content to be delivered in a way that ChatGPT understands that nothing should be output until all parts of the project are uploaded.

            Here's an approach:

            Include a Starting Message: Begin by letting ChatGPT know that a series of files will be uploaded and that no reply should be made until the end of the upload.
            Use a File Counter: This ensures you know how many files are being processed. It's a good way to check whether you've missed any files in case of interruptions or other unforeseen scenarios.
            Mark the End: Similarly to the starting message, include a clear end message.
            Here's a revised version of the TraverseAndConvert function, including these ideas:


            A simple GUI application using Windows Forms to load parts from a project into the clipboard, 
            providing a progress bar and a button for the user to load each portion sequentially for pasting into ChatGPT. 
        
            The idea is to break the content into manageable chunks and allow the user to load each chunk into the clipboard using the button, 
            all the while showing the progress.


        */

        private const int MAX_FILE_SIZE = 2048;  // Set to ChatGPT's limit or desired value.
        private static  int currentFileNumber = 1;
        private static string buffer = "";


        public static void TraverseAndConvert(string path, string outputDirectory)
        {
            foreach (var item in Directory.GetFileSystemEntries(path))
            {
                if (Directory.Exists(item))
                {
                    TraverseAndConvert(item, outputDirectory);
                }
                else
                {
                    string extension = Path.GetExtension(item).ToLower();
                    if (!IsRequiredFileExtension(extension))
                    {
                        continue;
                    }

                    string relativePath = GetRelativePath(item, path);
                    string content = File.ReadAllText(item, Encoding.Default);

                    content = ProcessContent(content);
                    content = RemoveBlankLinesAndWhitespace(content);

                    string formattedContent = $"[FILE] {relativePath}\n{content}\n[ENDFILE]\n";

                    buffer += formattedContent;

                    while (buffer.Length >= MAX_FILE_SIZE)
                    {
                        int lastFileEnd = buffer.Substring(0, MAX_FILE_SIZE).LastIndexOf("[ENDFILE]");
                        string toWrite = buffer.Substring(0, lastFileEnd + "[ENDFILE]".Length);

                        File.WriteAllText(Path.Combine(outputDirectory, $"chunk_{currentFileNumber}.txt"), toWrite);
                        buffer = buffer.Substring(lastFileEnd + "[ENDFILE]".Length);

                        currentFileNumber++;
                    }
                }
            }

            // Flush remaining content in buffer
            if (buffer.Length > 0)
            {
                File.WriteAllText(Path.Combine(outputDirectory, $"chunk_{currentFileNumber}.txt"), buffer);
                buffer = "";
            }
        }

        public static Task GenerateTextFromRepo(string localPath, string outputPath)
        {
            return Task.Run(() =>
            {
                using (var writer = new StreamWriter(outputPath))
                {
                    TraverseAndConvertOneFile(localPath, writer);
                }
            });
        }

        private static void TraverseAndConvertOneFile(string path, StreamWriter writer)
        {
            writer.WriteLine("[START UPLOAD]"); // Indicate the start of the upload
            writer.WriteLine("[INSTRUCTION] Wait for all parts to be uploaded before generating any output.");

            TraverseAndConvertRecursive(path, writer);

            writer.WriteLine("[END UPLOAD]"); // Mark the end of the upload process
            writer.WriteLine("[INSTRUCTION] All parts uploaded. You can now process and generate an output.");
        }

        private static void TraverseAndConvertRecursive(string path, StreamWriter writer)
        {
            int fileCounter = 0; // For tracking the number of files processed

            foreach (var item in Directory.GetFileSystemEntries(path))
            {
                if (Directory.Exists(item))
                {
                    TraverseAndConvertRecursive(item, writer);
                }
                else
                {
                    string extension = Path.GetExtension(item).ToLower();
                    if (!IsRequiredFileExtension(extension))
                    {
                        continue;
                    }

                    string relativePath = GetRelativePath(item, path);
                    string content = string.Empty;

                    using (var fs = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var reader = new StreamReader(fs))
                        {
                            content = reader.ReadToEnd();
                        }
                    }

                    content = ProcessContent(content);
                    content = RemoveBlankLinesAndWhitespace(content);

                    fileCounter++;
                    writer.WriteLine($"[PART {fileCounter}]"); // Indicate a new part
                    writer.WriteLine($"[FILE] {relativePath}");
                    writer.WriteLine(content);
                    writer.WriteLine("[ENDFILE]");
                }
            }
        }


        private static bool IsRequiredFileExtension(string extension)
        {
            string[] requiredExtensions = {
                                            ".cs", ".config", ".csproj", ".sln", ".resx", ".Designer.cs"
                                          };
            return requiredExtensions.Contains(extension);
        }


        private static string GetRelativePath(string fullPath, string basePath)
        {
            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);
            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        private static string ProcessContent(string content)
        {
            content = Regex.Replace(content, @"//.*$", "", RegexOptions.Multiline);
            content = Regex.Replace(content, @"/\*.*?\*/", "", RegexOptions.Singleline);
            content = Regex.Replace(content, @"#region.*?#endregion", "", RegexOptions.Singleline);
            content = Regex.Replace(content, @"#if.*?#endif", "", RegexOptions.Singleline);
            return content;
        }


        private static string RemoveBlankLinesAndWhitespace(string content)
        {
            string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            List<string> nonEmptyLines = new List<string>();
            bool previousLineWasEmpty = false;

            foreach (string line in lines)
            {
                bool isCurrentLineEmpty = string.IsNullOrWhiteSpace(line.Trim());
                if (isCurrentLineEmpty)
                {
                    continue;
                }
                if (!isCurrentLineEmpty)
                {
                    nonEmptyLines.Add(line.TrimEnd());
                }
                previousLineWasEmpty = isCurrentLineEmpty;
            }
            return string.Join(Environment.NewLine, nonEmptyLines);
        }
    }
}
