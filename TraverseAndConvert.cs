using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeAss
{
    public static class RepoToGpt
    {
        private const int MAX_FILE_SIZE = 2048;  // Set to ChatGPT's limit or desired value.
        private static int currentFileNumber = 1;
        private static string buffer = "";

        private static int FindLastFunctionEnd(string code, int maxIndex)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(code);
            var root = tree.GetRoot();

            // Consider various "function-like" constructs
            var functionNodes = root.DescendantNodes().Where(node =>
                node is MethodDeclarationSyntax ||
                node is ConstructorDeclarationSyntax ||
                node is DestructorDeclarationSyntax ||
                node is PropertyDeclarationSyntax ||
                node is DelegateDeclarationSyntax
            ).ToList();

            int lastFunctionEnd = -1;

            foreach (var functionNode in functionNodes)
            {
                var span = functionNode.Span;
                if (span.End <= maxIndex)
                {
                    lastFunctionEnd = span.End;
                }
                else
                {
                    break;  // Once we cross the maxIndex, we can break out of the loop.
                }
            }

            return lastFunctionEnd;
        }

        public static void TraverseAndSaveChunks(string path, string outputDirectory)
        {
            TraverseAndProcess(path, content =>
            {
                buffer += content;

                while (buffer.Length >= MAX_FILE_SIZE)
                {
                    int splitIndex = FindLastFunctionEnd(buffer, MAX_FILE_SIZE);
                    if (splitIndex == -1)
                    {
                        // This is an error case where you might not find a suitable split. 
                        // Here, you can decide on how to handle it. Maybe raise an exception or use a default split.
                        splitIndex = MAX_FILE_SIZE;
                    }

                    string toWrite = buffer.Substring(0, splitIndex + 1); // +1 to include the last brace
                    File.WriteAllText(Path.Combine(outputDirectory, $"chunk_{currentFileNumber}.txt"), toWrite);
                    buffer = buffer.Substring(splitIndex + 1);

                    currentFileNumber++;
                }
            });

            if (buffer.Length > 0)
            {
                File.WriteAllText(Path.Combine(outputDirectory, $"chunk_{currentFileNumber}.txt"), buffer);
                buffer = "";
            }
        }

        public static void TraverseAndWriteToStream(string path, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                writer.WriteLine("[START UPLOAD]");
                writer.WriteLine("[INSTRUCTION] Wait for all parts to be uploaded before generating any output.");
                TraverseAndProcess(path, content => writer.Write(content));
                writer.WriteLine("[END UPLOAD]");
                writer.WriteLine("[INSTRUCTION] All parts uploaded. You can now process and generate an output.");
            }
        }

        private static void TraverseAndProcess(string path, Action<string> actionOnContent)
        {
            foreach (var item in Directory.GetFileSystemEntries(path))
            {
                if (Directory.Exists(item))
                {
                    TraverseAndProcess(item, actionOnContent);
                }
                else
                {
                    if (!IsRequiredFileExtension(Path.GetExtension(item).ToLower()))
                    {
                        continue;
                    }

                    string relativePath = GetRelativePath(item, path);
                    string content = File.ReadAllText(item, Encoding.Default);

                    content = ProcessContent(content);
                    content = RemoveBlankLinesAndWhitespace(content);

                    string formattedContent = $"[FILE] {relativePath}\n{content}\n[ENDFILE]\n";
                    actionOnContent(formattedContent);
                }
            }
        }

        private static bool IsRequiredFileExtension(string extension)
        {
            string[] requiredExtensions = { ".cs", ".config", ".csproj", ".sln", ".resx", ".Designer.cs" };
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

            foreach (string line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line.Trim()))
                {
                    nonEmptyLines.Add(line.TrimEnd());
                }
            }
            return string.Join(Environment.NewLine, nonEmptyLines);
        }
    }
}
