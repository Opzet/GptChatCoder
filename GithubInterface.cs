using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        private TransferProgress progressState;

        public TransferProgress ProgressState => progressState;

        public event EventHandler<TransferProgressEventArgs> TransferProgressChanged;

        public void CloneRepository(string repoUrl, string localPath)
        {
            Repository.Clone(repoUrl, localPath);
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

        private void TraverseAndConvert(string path, StreamWriter writer)
        {
            foreach (var item in Directory.GetFileSystemEntries(path))
            {
                if (Directory.Exists(item))
                {
                    TraverseAndConvert(item, writer);
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
                    writer.WriteLine($"[FILE] {relativePath}");
                    writer.WriteLine(content);
                    writer.WriteLine("[ENDFILE]");
                }
            }
        }

        private bool IsRequiredFileExtension(string extension)
        {
            string[] requiredExtensions = {
                                        ".cs", ".config", ".csproj", ".sln", ".resx", ".Designer.cs"
                                    };
            return requiredExtensions.Contains(extension);
        }

        private string GetRelativePath(string fullPath, string basePath)
        {
            Uri baseUri = new Uri(basePath);
            Uri fullUri = new Uri(fullPath);
            Uri relativeUri = baseUri.MakeRelativeUri(fullUri);
            return Uri.UnescapeDataString(relativeUri.ToString());
        }

        private string ProcessContent(string content)
        {
            content = Regex.Replace(content, @"//.*$", "", RegexOptions.Multiline);
            content = Regex.Replace(content, @"/\*.*?\*/", "", RegexOptions.Singleline);
            content = Regex.Replace(content, @"#region.*?#endregion", "", RegexOptions.Singleline);
            content = Regex.Replace(content, @"#if.*?#endif", "", RegexOptions.Singleline);
            return content;
        }

        private string RemoveBlankLinesAndWhitespace(string content)
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
