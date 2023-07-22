using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

using LibGit2Sharp;

namespace CodeAss
{

    public interface IGitRepositoryManager
    {
        TransferProgress ProgressState { get; }

        void CloneRepository(string repoUrl, string localPath);
        Task CloneRepositoryAsync(string repoUrl, string localPath);
    }

    public interface IGPTSeedingService
    {
        Task GenerateTextFromRepo(string localPath, string outputPath);
    }

    public class TransferProgressEventArgs : EventArgs
    {
        public TransferProgress Progress { get; }

        public TransferProgressEventArgs(TransferProgress progress)
        {
            Progress = progress;
        }
    }

    public partial class CodeAss : IGitRepositoryManager, IGPTSeedingService
    {
        public TransferProgress progressState;

        public TransferProgress ProgressState => progressState;

        private Timer progressUpdateTimer;

        public event EventHandler<TransferProgressEventArgs> TransferProgressChanged;


        public void CloneRepository(string repoUrl, string localPath)
        {
            Repository.Clone(repoUrl, localPath);
        }


        public Task GenerateTextFromRepo(string localPath, string outputPath)
        {
            return Task.Run(() =>
            {
                using (var writer = new StreamWriter(outputPath))
                {
                    TraverseAndConvert(localPath, writer);
                }
            });
        }

        public void TraverseAndConvert(string path, StreamWriter writer)
        {
            foreach (var item in Directory.GetFileSystemEntries(path))
            {
                if (Directory.Exists(item))
                {
                    TraverseAndConvert(item, writer);
                }
                else
                {
                    // Skip files that are not required for ChatGPT (e.g., .dll, .exe, etc.)
                    string extension = Path.GetExtension(item).ToLower();
                    if (!IsRequiredFileExtension(extension))
                    {
                        continue;
                    }

                    string relativePath = GetRelativePath(item, path);
                    string content = string.Empty;

                    // Open the file in read-only mode using FileStream
                    using (var fs = new FileStream(item, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        using (var reader = new StreamReader(fs))
                        {
                            content = reader.ReadToEnd();
                        }
                    }

                    // You can add additional processing here if needed
                    // For instance, you might want to remove comments or extract code snippets.
                    content = ProcessContent(content);

                    // Remove blank lines and unnecessary whitespace at the end of each line
                    content = RemoveBlankLinesAndWhitespace(content);

                    writer.WriteLine($"[FILE] {relativePath}");

                    writer.WriteLine(content);


                    writer.WriteLine("[ENDFILE]");
                }
            }
        }

        private bool IsRequiredFileExtension(string extension)
        {
            // Add file extensions that are required for ChatGPT to review the complete solution
            string[] requiredExtensions = {
                                        ".cs", ".config", ".csproj", ".sln", ".resx", ".Designer.cs"
                                    }; // Example: Including .cs, .config, .csproj, .sln, .resx, .Designer.cs files
            return requiredExtensions.Contains(extension);
        }

        // Utility method to get relative path
        private string GetRelativePath(string fullPath, string basePath)
        {
            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);
            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        // Utility method for additional content processing (remove comments)
        private string ProcessContent(string content)
        {
            // Remove single-line comments (starting with //)
            content = Regex.Replace(content, @"//.*$", "", RegexOptions.Multiline);

            // Remove multi-line comments (/* ... */)
            content = Regex.Replace(content, @"/\*.*?\*/", "", RegexOptions.Singleline);

            // Remove region directives (#region ... #endregion)
            content = Regex.Replace(content, @"#region.*?#endregion", "", RegexOptions.Singleline);

            // Remove preprocessor directives (#if ... #endif)
            content = Regex.Replace(content, @"#if.*?#endif", "", RegexOptions.Singleline);

            return content;
        }

        private string RemoveBlankLinesAndWhitespace(string content)
        {
            // Split the content into lines
            string[] lines = content.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            // Remove blank lines and unnecessary whitespace at the end of lines
            List<string> nonEmptyLines = new List<string>();
            bool previousLineWasEmpty = false;

            foreach (string line in lines)
            {
                bool isCurrentLineEmpty = string.IsNullOrWhiteSpace(line.Trim());

                if (isCurrentLineEmpty) // && previousLineWasEmpty)
                {
                    // Skip consecutive blank lines
                    continue;
                }

                if (!isCurrentLineEmpty)
                {
                    // Add non-empty lines to the list
                    nonEmptyLines.Add(line.TrimEnd());
                }

                previousLineWasEmpty = isCurrentLineEmpty;
            }

            // Join the non-empty lines back together with appropriate line breaks
            string result = string.Join(Environment.NewLine, nonEmptyLines);

            return result;
        }



        public async Task CloneRepositoryAsync(string repoUrl, string localPath)
        {
            await Task.Run(() =>
            {
                Repository.Clone(repoUrl, localPath, new CloneOptions
                {
                    OnTransferProgress = (progress) =>
                    {
                        TransferProgressChanged?.Invoke(this, new TransferProgressEventArgs(progress));
                        return true;
                    },
                    OnCheckoutProgress = OnCheckoutProgressCallback
                });
            });
        }

        private void OnCheckoutProgressCallback(string path, int completedSteps, int totalSteps)
        {
            // Update UI with checkout progress if needed
            // Example: statusLabel.Text = $"Checking out: {path} ({completedSteps} / {totalSteps})";
        }
        private void ProgressUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // You can update the UI here based on the progress event handling
        }
    }


}
