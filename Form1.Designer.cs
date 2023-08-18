namespace CodeAss
{
    partial class MainForm
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.feedbackLabel = new System.Windows.Forms.Label();
            this.btnSelectRepoPath = new System.Windows.Forms.Button();
            this.btnSelectOutputPath = new System.Windows.Forms.Button();
            this.btnCloneRepo = new System.Windows.Forms.Button();
            this.btnSeedGPT = new System.Windows.Forms.Button();
            this.txtRepoUrl = new System.Windows.Forms.TextBox();
            this.txtLocalPath = new System.Windows.Forms.TextBox();
            this.txtOutputPath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 670);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 15);
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(241, 225);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(684, 35);
            this.progressBar.TabIndex = 9;
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.Location = new System.Drawing.Point(240, 415);
            this.feedbackLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(0, 20);
            this.feedbackLabel.TabIndex = 12;
            // 
            // btnSelectRepoPath
            // 
            this.btnSelectRepoPath.Location = new System.Drawing.Point(876, 125);
            this.btnSelectRepoPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectRepoPath.Name = "btnSelectRepoPath";
            this.btnSelectRepoPath.Size = new System.Drawing.Size(51, 31);
            this.btnSelectRepoPath.TabIndex = 8;
            this.btnSelectRepoPath.Text = "...";
            this.btnSelectRepoPath.UseVisualStyleBackColor = true;
            this.btnSelectRepoPath.Click += new System.EventHandler(this.btnSelectRepoPath_Click);
            // 
            // btnSelectOutputPath
            // 
            this.btnSelectOutputPath.Location = new System.Drawing.Point(876, 288);
            this.btnSelectOutputPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSelectOutputPath.Name = "btnSelectOutputPath";
            this.btnSelectOutputPath.Size = new System.Drawing.Size(51, 31);
            this.btnSelectOutputPath.TabIndex = 10;
            this.btnSelectOutputPath.Text = "...";
            this.btnSelectOutputPath.UseVisualStyleBackColor = true;
            this.btnSelectOutputPath.Click += new System.EventHandler(this.btnSelectOutputPath_Click);
            // 
            // btnCloneRepo
            // 
            this.btnCloneRepo.Location = new System.Drawing.Point(243, 182);
            this.btnCloneRepo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCloneRepo.Name = "btnCloneRepo";
            this.btnCloneRepo.Size = new System.Drawing.Size(146, 38);
            this.btnCloneRepo.TabIndex = 0;
            this.btnCloneRepo.Text = "Clone Repo";
            this.btnCloneRepo.UseVisualStyleBackColor = true;
            this.btnCloneRepo.Click += new System.EventHandler(this.btnCloneRepo_Click);
            // 
            // btnSeedGPT
            // 
            this.btnSeedGPT.Location = new System.Drawing.Point(244, 349);
            this.btnSeedGPT.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSeedGPT.Name = "btnSeedGPT";
            this.btnSeedGPT.Size = new System.Drawing.Size(146, 38);
            this.btnSeedGPT.TabIndex = 1;
            this.btnSeedGPT.Text = "Seed GPT";
            this.btnSeedGPT.UseVisualStyleBackColor = true;
            this.btnSeedGPT.Click += new System.EventHandler(this.btnSeedGPT_Click);
            // 
            // txtRepoUrl
            // 
            this.txtRepoUrl.Location = new System.Drawing.Point(243, 69);
            this.txtRepoUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRepoUrl.Name = "txtRepoUrl";
            this.txtRepoUrl.Size = new System.Drawing.Size(682, 26);
            this.txtRepoUrl.TabIndex = 2;
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Location = new System.Drawing.Point(244, 128);
            this.txtLocalPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(619, 26);
            this.txtLocalPath.TabIndex = 3;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(243, 289);
            this.txtOutputPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(619, 26);
            this.txtOutputPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(240, 45);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Repo URL .git ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(240, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 6;
            this.label2.Text = "Repo Local Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(240, 265);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "GPT Repo Decoded Path";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.feedbackLabel);
            this.Controls.Add(this.btnSelectRepoPath);
            this.Controls.Add(this.btnSelectOutputPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtOutputPath);
            this.Controls.Add(this.txtLocalPath);
            this.Controls.Add(this.txtRepoUrl);
            this.Controls.Add(this.btnSeedGPT);
            this.Controls.Add(this.btnCloneRepo);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.Label feedbackLabel;
        private System.Windows.Forms.Button btnCloneRepo;
        private System.Windows.Forms.Button btnSelectRepoPath;
        private System.Windows.Forms.Button btnSelectOutputPath;
        private System.Windows.Forms.Button btnSeedGPT;
        private System.Windows.Forms.TextBox txtRepoUrl;
        private System.Windows.Forms.TextBox txtLocalPath;
        private System.Windows.Forms.TextBox txtOutputPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

