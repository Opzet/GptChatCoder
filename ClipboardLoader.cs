using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;


namespace CodeAss
{
    public partial class ClipboardLoader : Form
    {
        private int currentChunkNumber = 1;
        private int totalChunks;
        private string chunksDirectory;


        public ClipboardLoader()
        {
            InitializeComponent();

        }

        private void btnChunkProject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(repoPath.Text))
            {
                feedbackLabel.Text = "Please provide the repository local path.";
                return;
            }

            btnChunkProject.Enabled = false;
            btnLoadNextChunk.Enabled = false;

            chunksDirectory = GetUniqueTempDirectory();

            // Set the output file path with .txt extension in the source directory
            //string outputFile = Path.Combine(solutionDir, "OutputForGPT.txt");
            //txtOutputPath.Text = outputFile;

            // Chunk Repo
            RepoToGpt.TraverseAndConvert(repoPath.Text, chunksDirectory);

            totalChunks = Directory.GetFiles(chunksDirectory, "chunk_*.txt").Length;

            // Initialize the progress bar after determining the total chunks
            progressBar1.Minimum = 1;
            progressBar1.Maximum = totalChunks;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            feedbackLabel.Text = $"Chunking completed. {totalChunks} chunks created.";
            btnLoadNextChunk.Enabled = true;
        }



        private string GetUniqueTempDirectory()
        {
            string tempDirectoryBase = Path.GetTempPath();
            string uniqueDirectory;

            do
            {
                uniqueDirectory = Path.Combine(tempDirectoryBase, Path.GetRandomFileName());
            } while (Directory.Exists(uniqueDirectory));

            Directory.CreateDirectory(uniqueDirectory);

            return uniqueDirectory;
        }


        private void ClipboardLoader_Load(object sender, EventArgs e)
        {
            
            statusLabel.Text = $"Ready> Select Solution Path";
            
            // Get the directory of the solution file (.sln)
            string solutionDir = GetSolutionDirectory();

            // Set the source directory of the solution as the local path
            repoPath.Text = solutionDir;

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



        private void btnLoadNextChunk_Click(object sender, EventArgs e)
        {

            
            if (currentChunkNumber <= totalChunks)
            {
                string chunkPath = Path.Combine(chunksDirectory, $"chunk_{currentChunkNumber}.txt");
                Clipboard.SetText(File.ReadAllText(chunkPath));
                progressBar1.Value = currentChunkNumber;

                statusLabel.Text = $"Loaded chunk {currentChunkNumber} of {totalChunks}";
                currentChunkNumber++;
            }
            else
            {
                statusLabel.Text = "All chunks loaded!";
                btnChunkProject.Enabled = false;
            }

        }

        private void btnSelectRepoPath_Click(object sender, EventArgs e)
        {

            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                DialogResult result = folderBrowserDialog.ShowDialog();
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(folderBrowserDialog.SelectedPath))
                {
                    repoPath.Text = folderBrowserDialog.SelectedPath;

                    // Reset chunk number and progress bar if the user selects a new path
                    currentChunkNumber = 1;
                    if (progressBar1.Maximum > progressBar1.Minimum)
                        progressBar1.Value = 1;
                }
            }
            btnChunkProject.Enabled = true;
            btnLoadNextChunk.Enabled = false;
        }

        private async void btnCloneRepo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRepoUrl.Text) || string.IsNullOrEmpty(repoPath.Text))
            {
                feedbackLabel.Text = "Please provide the repository URL and local path.";
                return;
            }

            statusLabel.Text = "Cloning repository...";

            try
            {
                await GitRepositoryManager.CloneRepositoryAsync(txtRepoUrl.Text, repoPath.Text);
                feedbackLabel.Text = "Repository cloned successfully!";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text = $"Error: {ex.Message}";
                // TODO: Log the error for debugging.
            }
            finally
            {
                statusLabel.Text = "Ready> ";
            }
        }
    }
}
