using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using LibGit2Sharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace CodeAss
{
    public partial class ClipboardLoader : Form
    {
        private int currentChunkNumber = 0;
        private int totalChunks;
        private string chunksDirectory;

        const string GPTInstruct = "[INSTRUCTION: STRICTLY WAIT FOR ALL PARTS TO BE UPLOADED BEFORE GENERATING ANY OUTPUT!]\r\n" +
                                   "[DESCRIPTION] This is a collection of C# code files from a Visual Studio project. Once the entire project has been uploaded, I will request tasks related to the code. This could include revising the code, fixing errors, restructuring or refactoring the codebase, and/or adding documentation. Please treat the contents as a coherent set and maintain the context when working on the provided tasks.\r\n";
                                   
        public ClipboardLoader()
        {
            InitializeComponent();
        }

        private void btnChunkProject_Click(object sender, EventArgs e)
        {
            // Load the initial instruction
            txtPreview.Text = GPTInstruct;
            
            ValidateButtons();

            if (string.IsNullOrEmpty(repoPath.Text))
            {
                feedbackLabel.Text = "Please provide the repository local path.";
                return;
            }

            btnChunkProject.Enabled = false;
            btnBack.Enabled = btnForward.Enabled = false;

            chunksDirectory = GetUniqueTempDirectory();

            // Set the output file path with .txt extension in the source directory
            //string outputFile = Path.Combine(solutionDir, "OutputForGPT.txt");
            //txtOutputPath.Text = outputFile;

            // Chunk Repo
            RepoToGpt.TraverseAndSaveChunks(repoPath.Text, chunksDirectory);

            totalChunks = Directory.GetFiles(chunksDirectory, "chunk_*.txt").Length;

           
            // Initialize the progress bar after determining the total chunks
            progressBar1.Minimum = 1;
            progressBar1.Maximum = totalChunks;
            progressBar1.Value = 1;
            progressBar1.Step = 1;

            feedbackLabel.Text = $"Chunking completed. {totalChunks} chunks created.";
            LoadContent(currentChunkNumber);
            ValidateButtons(); 
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
                txtPreview.Text = File.ReadAllText(chunkPath);
                Clipboard.SetText(txtPreview.Text);
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            // Decrement currentChunkNumber and load the previous content
            currentChunkNumber--;
            LoadContent(currentChunkNumber);

            ValidateButtons();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            // Increment currentChunkNumber and load the next content
            currentChunkNumber++;
            LoadContent(currentChunkNumber);

            ValidateButtons();
        }

        private void LoadContent(int contentNumber)
        {
            string contentToLoad;

            string GPTChunkMsg = "[INSTRUCTION: STRICTLY DO NOT GENERATE OUTPUT UNTIL PROMPTED!]\r\n" +
                                $"[PART : {contentNumber} OF {totalChunks}]";


            if (contentNumber == 0) // Load initial instructions
            {
              

                contentToLoad = GPTInstruct;
                contentToLoad += GPTChunkMsg;
                contentToLoad += $"[START UPLOAD]"; 
            }
            else if (contentNumber >= 1 && contentNumber <= totalChunks)
            {
                string chunkPath = Path.Combine(chunksDirectory, $"chunk_{contentNumber}.txt");
                
                contentToLoad = File.ReadAllText(chunkPath);
                contentToLoad += GPTChunkMsg;

                progressBar1.Value = contentNumber;
                statusLabel.Text = $"Loaded chunk {contentNumber} of {totalChunks}";

                if (contentNumber == totalChunks)
                {
                    contentToLoad += GPTChunkMsg;
                    contentToLoad += "\n[END UPLOAD]\n[INSTRUCTION] All parts uploaded. You can now process and generate an output.";
                }
            }
            else
            {
                contentToLoad = "";
            }

            txtPreview.Text = contentToLoad;

            if (!string.IsNullOrEmpty(contentToLoad))
            {
                Clipboard.SetText(contentToLoad);
            }
            else
            {
                Clipboard.Clear();
            }
        }


        private void ValidateButtons()
        {
            btnBack.Enabled = currentChunkNumber > 0; // Only enable back button if it's not on the instruction
            btnForward.Enabled = currentChunkNumber < totalChunks; // Only enable forward button if there are chunks left to display
        }
    }
}
