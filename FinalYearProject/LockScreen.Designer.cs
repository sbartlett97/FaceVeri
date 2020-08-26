namespace FinalYearProject
{
    partial class LockScreen
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
            this.components = new System.ComponentModel.Container();
            this.timeLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.passWrdBx = new System.Windows.Forms.TextBox();
            this.submitPass = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("MS Outlook", 50F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(59, 120);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(204, 78);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "00:00";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // passWrdBx
            // 
            this.passWrdBx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passWrdBx.Location = new System.Drawing.Point(379, 260);
            this.passWrdBx.Name = "passWrdBx";
            this.passWrdBx.Size = new System.Drawing.Size(226, 20);
            this.passWrdBx.TabIndex = 1;
            this.passWrdBx.Text = "Enter Password...";
            this.passWrdBx.Visible = false;
            this.passWrdBx.Click += new System.EventHandler(this.passWrdBx_Click);
            this.passWrdBx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.passWrdBx_KeyPress);
            // 
            // submitPass
            // 
            this.submitPass.BackColor = System.Drawing.Color.White;
            this.submitPass.Location = new System.Drawing.Point(622, 258);
            this.submitPass.Name = "submitPass";
            this.submitPass.Size = new System.Drawing.Size(75, 23);
            this.submitPass.TabIndex = 2;
            this.submitPass.Text = "Submit";
            this.submitPass.UseVisualStyleBackColor = false;
            this.submitPass.Visible = false;
            this.submitPass.Click += new System.EventHandler(this.submitPass_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(67, 335);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(861, 29);
            this.label1.TabIndex = 3;
            this.label1.Text = "Look at the webcam tounlock the screen or press the Esc key to enter password";
            // 
            // LockScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(997, 457);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.submitPass);
            this.Controls.Add(this.passWrdBx);
            this.Controls.Add(this.timeLabel);
            this.Name = "LockScreen";
            this.Text = "LockScreen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LockScreen_FormClosing);
            this.Load += new System.EventHandler(this.LockScreen_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox passWrdBx;
        private System.Windows.Forms.Button submitPass;
        private System.Windows.Forms.Label label1;
    }
}