using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

using System.Threading;
using System.Threading.Tasks;
using Emgu.CV.Face;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Collections.Generic;
using FinalYearProject.Properties;
using System.Linq;
using System.Resources;

namespace FinalYearProject
{
    public partial class Settings : Form
    {
        //device filter for finding imaging devices
        FilterInfoCollection filter;

        //array for keeping track of what settings have been changed
        private List<String> changeFlags = new List<String>();

        public Settings()
        {
            InitializeComponent();
            checkEnrolledFaces();

        }

        #region form loading
        private void Form1_Load_1(object sender, EventArgs e)
        {
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter)
                devicesCmb.Items.Add(device.Name);
            devicesCmb.SelectedIndex = devicesCmb.Items.IndexOf(Properties.Settings.Default.webcam) != -1 ? devicesCmb.Items.IndexOf(Properties.Settings.Default.webcam) : 0;
            loadDefaultValues();

        }

        private void loadDefaultValues() {
            lockMinutes.Value = Properties.Settings.Default.locktimer;
            Properties.Settings.Default.webcam = devicesCmb.SelectedItem.ToString();
            Properties.Settings.Default.Save();
        }
        #endregion

        #region save changes

        private void lockMinutes_ValueChanged(object sender, EventArgs e)
        {
            if (!changeFlags.Contains("timer"))
            {
                changeFlags.Add("timer");
            }
        }

        private void saveChanges()
        {
            foreach (String flag in changeFlags)
            {
                switch (flag)
                {
                    case "cam":
                        Properties.Settings.Default.webcam = devicesCmb.SelectedItem.ToString();
                        Console.WriteLine(Properties.Settings.Default.webcam);
                        break;
                    case "timer":
                        Properties.Settings.Default.locktimer = (int)lockMinutes.Value;
                        break;
                    default:
                        break;
                }
            }
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Reload();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            saveChanges();
        }

        private void stopCap_Click(object sender, EventArgs e)
        {
            Form changePass = new ChangePassword();
            changePass.ShowDialog();
        }
        #endregion

        private void enrollFace_Click(object sender, EventArgs e)
        {
            Form enrollFace = new EnrollFace();
            enrollFace.ShowDialog();
            checkEnrolledFaces();
        }

        private void checkEnrolledFaces()
        {
            if (Properties.Settings.Default.numberOfProfiles == Properties.Settings.Default.maximumProfiles)
            {
                enrollFace.Enabled = false;
            }
        }

        private void devicesCmb_SelectedValueChanged(object sender, EventArgs e)
        {
            if (!changeFlags.Contains("cam"))
            {
                changeFlags.Add("cam");
            }
        }

        private void saveAndCloseBtn_Click(object sender, EventArgs e)
        {
            saveChanges();
            this.Close();
        }
    }


}

