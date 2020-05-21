namespace FinalYearProject
{
    partial class EnrollFace
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
            this.camViewer = new System.Windows.Forms.PictureBox();
            this.beginCap = new System.Windows.Forms.Button();
            this.capProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.camViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // camViewer
            // 
            this.camViewer.Location = new System.Drawing.Point(33, 30);
            this.camViewer.Name = "camViewer";
            this.camViewer.Size = new System.Drawing.Size(320, 240);
            this.camViewer.TabIndex = 0;
            this.camViewer.TabStop = false;
            // 
            // beginCap
            // 
            this.beginCap.Location = new System.Drawing.Point(152, 318);
            this.beginCap.Name = "beginCap";
            this.beginCap.Size = new System.Drawing.Size(75, 23);
            this.beginCap.TabIndex = 1;
            this.beginCap.Text = "Start";
            this.beginCap.UseVisualStyleBackColor = true;
            this.beginCap.Click += new System.EventHandler(this.beginCap_Click);
            // 
            // capProgress
            // 
            this.capProgress.Location = new System.Drawing.Point(82, 276);
            this.capProgress.Maximum = 800;
            this.capProgress.Name = "capProgress";
            this.capProgress.Size = new System.Drawing.Size(213, 14);
            this.capProgress.Step = 1;
            this.capProgress.TabIndex = 2;
            this.capProgress.Visible = false;
            // 
            // EnrollFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 390);
            this.Controls.Add(this.capProgress);
            this.Controls.Add(this.beginCap);
            this.Controls.Add(this.camViewer);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.Name = "EnrollFace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Enroll Face";
            this.Load += new System.EventHandler(this.EnrollFace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.camViewer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox camViewer;
        private System.Windows.Forms.Button beginCap;
        private System.Windows.Forms.ProgressBar capProgress;
    }
}