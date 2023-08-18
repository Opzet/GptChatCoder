using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using LibGit2Sharp;

namespace CodeAss
{
   
    public class TransferProgressEventArgs : EventArgs
    {
        public TransferProgress Progress { get; }

        public TransferProgressEventArgs(TransferProgress progress)
        {
            Progress = progress;
        }
    }

    public static class GitRepositoryManager
    {
        //private TransferProgress progressState;

        //public TransferProgress ProgressState => progressState;

        //public event EventHandler<TransferProgressEventArgs> TransferProgressChanged;

        public static void CloneRepository(string repoUrl, string localPath)
        {
            Repository.Clone(repoUrl, localPath);
        }

        public static async Task CloneRepositoryAsync(string repoUrl, string localPath)
        {
            await Task.Run(() =>
            {
                Repository.Clone(repoUrl, localPath, new CloneOptions
                {
                    OnTransferProgress = (progress) =>
                    {
                       // TransferProgressChanged?.Invoke(this, new TransferProgressEventArgs(progress));
                        return true;
                    },
                    OnCheckoutProgress = OnCheckoutProgressCallback
                });
            });
        }

        private static void OnCheckoutProgressCallback(string path, int completedSteps, int totalSteps)
        {
            // Update UI with checkout progress if needed
        }

      

    }
}
