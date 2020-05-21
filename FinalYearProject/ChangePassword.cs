using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalYearProject
{
    public partial class ChangePassword : Form
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        //checkbox for toggling password visibility
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                // default character (not hidden)
                oldPass.PasswordChar = '\0';
                newPass.PasswordChar = '\0';
                confirmPass.PasswordChar = '\0';
            }
            else 
            {
                // * replaces shown text to conceal password
                oldPass.PasswordChar = '*';
                newPass.PasswordChar = '*';
                confirmPass.PasswordChar = '*';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((oldPass.Text == Properties.Settings.Default.password) && (newPass.Text == confirmPass.Text))
            {
                Properties.Settings.Default.password = newPass.Text;
                Properties.Settings.Default.Save();
                this.Close();
            }
            else 
            {
                oldPass.Text = "";
                newPass.Text = "";
                confirmPass.Text = "";
                MessageBox.Show("The entered password didn't match, please try again.");

            }
        }
    }
}
