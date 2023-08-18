using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;



namespace CodeAss
{
    public partial class MainForm : Form
    {
        private readonly IGitRepositoryManager _repositoryManager;
        private readonly IGPTSeedingService _seedingService;
        private Timer _progressTimer;

        public MainForm(IGitRepositoryManager repositoryManager, IGPTSeedingService seedingService)
        {
            InitializeComponent();
            _repositoryManager = repositoryManager;
            _seedingService = seedingService;

            InitializeProgressTimer();
        }

        /// <summary>
        /// Initializes the progress timer with required configurations.
        /// </summary>
        private void InitializeProgressTimer()
        {
            _progressTimer = new Timer
            {
                Interval = 100
            };
            _progressTimer.Tick += HandleProgressUpdate;
        }

        /// <summary>
        /// Handles repository cloning upon button click.
        /// </summary>
        /// 
        private async void btnCloneRepo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRepoUrl.Text) || string.IsNullOrEmpty(txtLocalPath.Text))
            {
                feedbackLabel.Text = "Please provide the repository URL and local path.";
                return;
            }

            ToggleButtonState(btnCloneRepo, false);
            DisplayProgress("Cloning repository...");

            try
            {
                await _repositoryManager.CloneRepositoryAsync(txtRepoUrl.Text, txtLocalPath.Text);
                feedbackLabel.Text = "Repository cloned successfully!";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text = $"Error: {ex.Message}";
                // TODO: Log the error for debugging.
            }
            finally
            {
                ResetProgress();
                ToggleButtonState(btnCloneRepo, true);
            }
        }

        /// <summary>
        /// Updates the UI to reflect progress state.
        /// </summary>
        private void HandleProgressUpdate(object sender, EventArgs e)
        {
            var progressState = _repositoryManager.ProgressState;
            if (progressState != null)
            {
                progressBar.Maximum = (int)progressState.TotalObjects;
                progressBar.Value = (int)progressState.ReceivedObjects;
            }
        }

        /// <summary>
        /// Handles GPT seeding upon button click.
        /// </summary>
        private void btnSeedGPT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtLocalPath.Text) || string.IsNullOrEmpty(txtOutputPath.Text))
            {
                feedbackLabel.Text = "Please provide the local path and output path.";
                return;
            }

            ToggleButtonState(btnSeedGPT, false);
            DisplayProgress("Seeding GPT...");

            try
            {
                if (File.Exists(txtOutputPath.Text))
                {
                    File.Delete(txtOutputPath.Text);
                }

                _seedingService.GenerateTextFromRepo(txtLocalPath.Text, txtOutputPath.Text);
                feedbackLabel.Text = "GPT seeding completed!";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text = $"Error: {ex.Message}";
                // TODO: Log the error for debugging.
            }
            finally
            {
                ResetProgress();
                ToggleButtonState(btnSeedGPT, true);
            }
        }

        /// <summary>
        /// Displays progress information.
        /// </summary>
        private void DisplayProgress(string message)
        {
            progressBar.Visible = true;
            progressBar.Value = 0;
            feedbackLabel.Text = message;
            statusLabel.Text = $"{message} in progress...";
        }

        /// <summary>
        /// Resets the progress display.
        /// </summary>
        private void ResetProgress()
        {
            progressBar.Visible = false;
            statusLabel.Text = "Ready";
        }

        /// <summary>
        /// Toggles the enabled state of a button.
        /// </summary>
        private void ToggleButtonState(Button button, bool isEnabled)
        {
            button.Enabled = isEnabled;
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
