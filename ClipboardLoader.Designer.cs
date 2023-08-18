namespace CodeAss
{
    partial class ClipboardLoader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnChunkProject = new System.Windows.Forms.Button();
            this.feedbackLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.txtPreview = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.btnSelectRepoPath = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.repoPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRepoUrl = new System.Windows.Forms.TextBox();
            this.btnCloneRepo = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(103, 303);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(560, 23);
            this.progressBar1.TabIndex = 15;
            // 
            // btnChunkProject
            // 
            this.btnChunkProject.Location = new System.Drawing.Point(288, 202);
            this.btnChunkProject.Name = "btnChunkProject";
            this.btnChunkProject.Size = new System.Drawing.Size(178, 44);
            this.btnChunkProject.TabIndex = 21;
            this.btnChunkProject.Text = "Chunk Project";
            this.btnChunkProject.UseVisualStyleBackColor = true;
            this.btnChunkProject.Click += new System.EventHandler(this.btnChunkProject_Click);
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.Location = new System.Drawing.Point(103, 280);
            this.feedbackLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(560, 20);
            this.feedbackLabel.TabIndex = 20;
            this.feedbackLabel.Text = "Feedback";
            this.feedbackLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(74, 135);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 20);
            this.label3.TabIndex = 18;
            this.label3.Text = "OUT -> Chunk Path";
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(77, 159);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(619, 26);
            this.txtOutputPath.TabIndex = 17;
            // 
            // txtPreview
            // 
            this.txtPreview.Location = new System.Drawing.Point(24, 332);
            this.txtPreview.Multiline = true;
            this.txtPreview.Name = "txtPreview";
            this.txtPreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPreview.Size = new System.Drawing.Size(764, 688);
            this.txtPreview.TabIndex = 22;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Location = new System.Drawing.Point(0, 1093);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(800, 20);
            this.statusLabel.TabIndex = 23;
            this.statusLabel.Text = "Ready>";
            // 
            // btnSelectRepoPath
            // 
            this.btnSelectRepoPath.Location = new System.Drawing.Point(690, 95);
            this.btnSelectRepoPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectRepoPath.Name = "btnSelectRepoPath";
            this.btnSelectRepoPath.Size = new System.Drawing.Size(51, 26);
            this.btnSelectRepoPath.TabIndex = 26;
            this.btnSelectRepoPath.Text = "...";
            this.btnSelectRepoPath.UseVisualStyleBackColor = true;
            this.btnSelectRepoPath.Click += new System.EventHandler(this.btnSelectRepoPath_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(82, 70);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 20);
            this.label2.TabIndex = 25;
            this.label2.Text = "IN -> Repo Local Path";
            // 
            // repoPath
            // 
            this.repoPath.Location = new System.Drawing.Point(77, 95);
            this.repoPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.repoPath.Name = "repoPath";
            this.repoPath.Size = new System.Drawing.Size(605, 26);
            this.repoPath.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(74, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 28;
            this.label1.Text = "Repo URL .git ";
            // 
            // txtRepoUrl
            // 
            this.txtRepoUrl.Location = new System.Drawing.Point(77, 33);
            this.txtRepoUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRepoUrl.Name = "txtRepoUrl";
            this.txtRepoUrl.Size = new System.Drawing.Size(510, 26);
            this.txtRepoUrl.TabIndex = 27;
            // 
            // btnCloneRepo
            // 
            this.btnCloneRepo.Location = new System.Drawing.Point(595, 27);
            this.btnCloneRepo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloneRepo.Name = "btnCloneRepo";
            this.btnCloneRepo.Size = new System.Drawing.Size(146, 38);
            this.btnCloneRepo.TabIndex = 29;
            this.btnCloneRepo.Text = "Clone Repo";
            this.btnCloneRepo.UseVisualStyleBackColor = true;
            this.btnCloneRepo.Click += new System.EventHandler(this.btnCloneRepo_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(24, 1036);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(166, 45);
            this.btnBack.TabIndex = 30;
            this.btnBack.Text = "< Previous Chunk";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnForward
            // 
            this.btnForward.Location = new System.Drawing.Point(622, 1036);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(166, 45);
            this.btnForward.TabIndex = 31;
            this.btnForward.Text = "Next Chunk >";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // ClipboardLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 1113);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnCloneRepo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRepoUrl);
            this.Controls.Add(this.btnSelectRepoPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.repoPath);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.txtPreview);
            this.Controls.Add(this.btnChunkProject);
            this.Controls.Add(this.feedbackLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.progressBar1);
            this.Name = "ClipboardLoader";
            this.Text = "ClipboardLoader";
            this.Load += new System.EventHandler(this.ClipboardLoader_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnChunkProject;
        private System.Windows.Forms.Label feedbackLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.TextBox txtPreview;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Button btnSelectRepoPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox repoPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRepoUrl;
        private System.Windows.Forms.Button btnCloneRepo;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnForward;
    }
}