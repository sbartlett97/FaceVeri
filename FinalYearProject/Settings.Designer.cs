namespace FinalYearProject
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.devicesCmb = new System.Windows.Forms.ComboBox();
            this.enrollFace = new System.Windows.Forms.Button();
            this.stopTarin = new System.Windows.Forms.Button();
            this.stopCap = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.saveBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lockMinutes = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.saveAndCloseBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.lockMinutes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Device";
            // 
            // devicesCmb
            // 
            this.devicesCmb.FormattingEnabled = true;
            this.devicesCmb.Location = new System.Drawing.Point(78, 68);
            this.devicesCmb.Name = "devicesCmb";
            this.devicesCmb.Size = new System.Drawing.Size(356, 21);
            this.devicesCmb.TabIndex = 3;
            this.devicesCmb.SelectedValueChanged += new System.EventHandler(this.devicesCmb_SelectedValueChanged);
            // 
            // enrollFace
            // 
            this.enrollFace.Location = new System.Drawing.Point(92, 211);
            this.enrollFace.Name = "enrollFace";
            this.enrollFace.Size = new System.Drawing.Size(75, 23);
            this.enrollFace.TabIndex = 4;
            this.enrollFace.Text = "&Enroll Face";
            this.enrollFace.UseVisualStyleBackColor = true;
            this.enrollFace.Click += new System.EventHandler(this.enrollFace_Click);
            // 
            // stopTarin
            // 
            this.stopTarin.Location = new System.Drawing.Point(291, 209);
            this.stopTarin.Name = "stopTarin";
            this.stopTarin.Size = new System.Drawing.Size(75, 23);
            this.stopTarin.TabIndex = 6;
            this.stopTarin.Text = "Re-Train";
            this.stopTarin.UseVisualStyleBackColor = true;
            // 
            // stopCap
            // 
            this.stopCap.Location = new System.Drawing.Point(176, 335);
            this.stopCap.Name = "stopCap";
            this.stopCap.Size = new System.Drawing.Size(113, 23);
            this.stopCap.TabIndex = 7;
            this.stopCap.Text = "&Change Password";
            this.stopCap.UseVisualStyleBackColor = true;
            this.stopCap.Click += new System.EventHandler(this.stopCap_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(94, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(258, 29);
            this.label3.TabIndex = 9;
            this.label3.Text = "Select  Capture Device";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(142, 156);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(187, 29);
            this.label4.TabIndex = 10;
            this.label4.Text = "Face recognizer";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(127, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(213, 29);
            this.label2.TabIndex = 11;
            this.label2.Text = "Password Settings";
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(78, 474);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(113, 23);
            this.saveBtn.TabIndex = 12;
            this.saveBtn.Text = "Save Changes";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(62, 385);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(336, 29);
            this.label5.TabIndex = 13;
            this.label5.Text = "Automatically lock screen after";
            // 
            // lockMinutes
            // 
            this.lockMinutes.Location = new System.Drawing.Point(121, 434);
            this.lockMinutes.Name = "lockMinutes";
            this.lockMinutes.Size = new System.Drawing.Size(120, 20);
            this.lockMinutes.TabIndex = 14;
            this.lockMinutes.ValueChanged += new System.EventHandler(this.lockMinutes_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(255, 436);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 18);
            this.label6.TabIndex = 15;
            this.label6.Text = "Minutes";
            // 
            // saveAndCloseBtn
            // 
            this.saveAndCloseBtn.Location = new System.Drawing.Point(321, 474);
            this.saveAndCloseBtn.Name = "saveAndCloseBtn";
            this.saveAndCloseBtn.Size = new System.Drawing.Size(113, 23);
            this.saveAndCloseBtn.TabIndex = 16;
            this.saveAndCloseBtn.Text = "Save and Exit";
            this.saveAndCloseBtn.UseVisualStyleBackColor = true;
            this.saveAndCloseBtn.Click += new System.EventHandler(this.saveAndCloseBtn_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 518);
            this.Controls.Add(this.saveAndCloseBtn);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lockMinutes);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.stopCap);
            this.Controls.Add(this.stopTarin);
            this.Controls.Add(this.enrollFace);
            this.Controls.Add(this.devicesCmb);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Face Recognition";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.lockMinutes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox devicesCmb;
        private System.Windows.Forms.Button enrollFace;
        private System.Windows.Forms.Button stopTarin;
        private System.Windows.Forms.Button stopCap;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown lockMinutes;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button saveAndCloseBtn;
    }
}