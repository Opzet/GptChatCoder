namespace CodeAss
{
    partial class Form1
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
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 17);
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(160, 160);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(456, 23);
            this.progressBar.TabIndex = 9;
            // 
            // feedbackLabel
            // 
            this.feedbackLabel.AutoSize = true;
            this.feedbackLabel.Location = new System.Drawing.Point(160, 270);
            this.feedbackLabel.Name = "feedbackLabel";
            this.feedbackLabel.Size = new System.Drawing.Size(0, 13);
            this.feedbackLabel.TabIndex = 12;
            // 
            // btnSelectRepoPath
            // 
            this.btnSelectRepoPath.Location = new System.Drawing.Point(584, 81);
            this.btnSelectRepoPath.Name = "btnSelectRepoPath";
            this.btnSelectRepoPath.Size = new System.Drawing.Size(34, 20);
            this.btnSelectRepoPath.TabIndex = 8;
            this.btnSelectRepoPath.Text = "...";
            this.btnSelectRepoPath.UseVisualStyleBackColor = true;
            this.btnSelectRepoPath.Click += new System.EventHandler(this.btnSelectRepoPath_Click);
            // 
            // btnSelectOutputPath
            // 
            this.btnSelectOutputPath.Location = new System.Drawing.Point(584, 187);
            this.btnSelectOutputPath.Name = "btnSelectOutputPath";
            this.btnSelectOutputPath.Size = new System.Drawing.Size(34, 20);
            this.btnSelectOutputPath.TabIndex = 10;
            this.btnSelectOutputPath.Text = "...";
            this.btnSelectOutputPath.UseVisualStyleBackColor = true;
            this.btnSelectOutputPath.Click += new System.EventHandler(this.btnSelectOutputPath_Click);
            // 
            // btnCloneRepo
            // 
            this.btnCloneRepo.Location = new System.Drawing.Point(162, 118);
            this.btnCloneRepo.Name = "btnCloneRepo";
            this.btnCloneRepo.Size = new System.Drawing.Size(97, 25);
            this.btnCloneRepo.TabIndex = 0;
            this.btnCloneRepo.Text = "Clone Repo";
            this.btnCloneRepo.UseVisualStyleBackColor = true;
            this.btnCloneRepo.Click += new System.EventHandler(this.btnCloneRepo_Click);
            // 
            // btnSeedGPT
            // 
            this.btnSeedGPT.Location = new System.Drawing.Point(163, 227);
            this.btnSeedGPT.Name = "btnSeedGPT";
            this.btnSeedGPT.Size = new System.Drawing.Size(97, 25);
            this.btnSeedGPT.TabIndex = 1;
            this.btnSeedGPT.Text = "Seed GPT";
            this.btnSeedGPT.UseVisualStyleBackColor = true;
            this.btnSeedGPT.Click += new System.EventHandler(this.btnSeedGPT_Click);
            // 
            // txtRepoUrl
            // 
            this.txtRepoUrl.Location = new System.Drawing.Point(162, 45);
            this.txtRepoUrl.Name = "txtRepoUrl";
            this.txtRepoUrl.Size = new System.Drawing.Size(456, 20);
            this.txtRepoUrl.TabIndex = 2;
            // 
            // txtLocalPath
            // 
            this.txtLocalPath.Location = new System.Drawing.Point(163, 83);
            this.txtLocalPath.Name = "txtLocalPath";
            this.txtLocalPath.Size = new System.Drawing.Size(414, 20);
            this.txtLocalPath.TabIndex = 3;
            // 
            // txtOutputPath
            // 
            this.txtOutputPath.Location = new System.Drawing.Point(162, 188);
            this.txtOutputPath.Name = "txtOutputPath";
            this.txtOutputPath.Size = new System.Drawing.Size(414, 20);
            this.txtOutputPath.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Repo URL .git ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(160, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Repo Local Path";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(160, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "GPT Repo Decoded Path";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
            this.Name = "Form1";
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

