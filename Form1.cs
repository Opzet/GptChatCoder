using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeAss
{
    public partial class Form1 : Form
    {
        private readonly IGitRepositoryManager gitRepoManager;
        private readonly IGPTSeedingService gptSeedingService;
        private Timer progressUpdateTimer;

        public Form1(IGitRepositoryManager gitRepoManager, IGPTSeedingService gptSeedingService)
        {
            InitializeComponent();
            this.gitRepoManager = gitRepoManager;
            this.gptSeedingService = gptSeedingService;

            // Create and initialize the progressUpdateTimer
            progressUpdateTimer = new Timer();
            progressUpdateTimer.Interval = 100;
            progressUpdateTimer.Tick += ProgressUpdateTimer_Tick;
        }

        private async void btnCloneRepo_Click(object sender, EventArgs e)
        {
            string repoUrl = txtRepoUrl.Text;
            string localPath = txtLocalPath.Text;

            try
            {
                btnCloneRepo.Enabled = false;
                feedbackLabel.Text = "Cloning repository...";
                statusLabel.Text = "Cloning in progress...";

                progressBar.Value = 0;
                progressBar.Visible = true;

                await gitRepoManager.CloneRepositoryAsync(repoUrl, localPath);

                feedbackLabel.Text = "Repository cloned successfully!";
                statusLabel.Text = "Clone operation completed.";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text = "Error: " + ex.Message;
                statusLabel.Text = "Clone operation failed.";
            }
            finally
            {
                btnCloneRepo.Enabled = true;
                progressBar.Visible = false;
            }
        }



        private void ProgressUpdateTimer_Tick(object sender, EventArgs e)
        {
            // Update UI with transfer progress
            if (gitRepoManager.ProgressState != null)
            {
                progressBar.Maximum = (int)gitRepoManager.ProgressState.TotalObjects;
                progressBar.Value = (int)gitRepoManager.ProgressState.ReceivedObjects;
            }
        }

        private void btnSeedGPT_Click(object sender, EventArgs e)
        {
            string localPath = txtLocalPath.Text;
            string outputPath = txtOutputPath.Text;

            try
            {
                btnSeedGPT.Enabled = false;
                feedbackLabel.Text = "Seeding GPT...";
                statusLabel.Text = "GPT seeding in progress...";

                // Delete the output file if it already exists
                if (File.Exists(outputPath))
                {
                    File.Delete(outputPath);
                }

                // Generate the text to the output file
                gptSeedingService.GenerateTextFromRepo(localPath, outputPath);

                feedbackLabel.Text = "GPT seeding completed!";
                statusLabel.Text = "GPT seeding operation completed.";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text = "Error: " + ex.Message;
                statusLabel.Text = "GPT seeding operation failed.";
            }
            finally
            {
                btnSeedGPT.Enabled = true;
            }
        }


        private void btnSelectRepoPath_Click(object sender, EventArgs e)
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    txtLocalPath.Text = folderBrowserDialog.SelectedPath;
                }
            }
        }

        private void btnSelectOutputPath_Click(object sender, EventArgs e)
        {
            using (var saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files|*.txt";
                saveFileDialog.Title = "Save GPT Output";
                DialogResult result = saveFileDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(saveFileDialog.FileName))
                {
                    txtOutputPath.Text = saveFileDialog.FileName;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Debugger.IsAttached)
            {
                // Get the directory of the solution file (.sln)
                string solutionDir = GetSolutionDirectory();

                // Set the source directory of the solution as the local path
                txtLocalPath.Text = solutionDir;

                // Set the output file path with .txt extension in the source directory
                string outputFile = Path.Combine(solutionDir, "OutputForGPT.txt");
                txtOutputPath.Text = outputFile;
            }
        }
        private string GetSolutionDirectory()
        {
            // Get the directory of the current executing assembly (WinForms project path)
            string currentAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string projectPath = Path.GetDirectoryName(currentAssemblyPath);

            // Navigate up the directory tree until the .sln file is found
            string solutionDir = projectPath;
            while (!string.IsNullOrEmpty(solutionDir))
            {
                string[] solutionFiles = Directory.GetFiles(solutionDir, "*.sln");
                if (solutionFiles.Length > 0)
                {
                    // Solution file found, return the directory containing it
                    return solutionDir;
                }

                // Move one level up in the directory tree
                solutionDir = Path.GetDirectoryName(solutionDir);
            }

            // If no .sln file is found, return the original project path
            return projectPath;
        }
    }
}
