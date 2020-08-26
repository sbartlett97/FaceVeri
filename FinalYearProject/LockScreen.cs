using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Runtime.InteropServices;
using System.Diagnostics;
using FinalYearProject.Properties;
using System.Collections.Generic;

namespace FinalYearProject
{

    public partial class LockScreen : Form
    {

        #region Keyboard Suppression Variables
        // Structure contain information about low-level keyboard input event 
        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr extra;
        }
        //System level functions to be used for hook and unhook keyboard input  
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int id, LowLevelKeyboardProc callback, IntPtr hMod, uint dwThreadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool UnhookWindowsHookEx(IntPtr hook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hook, int nCode, IntPtr wp, IntPtr lp);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string name);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern short GetAsyncKeyState(Keys key);
        //Declaring Global objects     
        private IntPtr ptrHook;
        private LowLevelKeyboardProc objKeyboardProcess;
        #endregion

        #region variables

        //Video capture variables
        VideoCaptureDevice cam;
        FilterInfoCollection filter;

        DateTime curDate;
        //Face recognition objects
        private List<FaceRecognizer> recognisers = new List<FaceRecognizer>();
        static readonly CascadeClassifier classifier = new CascadeClassifier(Application.StartupPath + "/Cascades/haarcascade_frontalface_default.xml");

        //Image objects
        private Image<Bgr, byte> currentFrame;
        private Image<Gray, byte> grayFrame;
        private Image<Gray, byte> result;
        delegate void CloseMethod();

        #endregion

        #region form methods
        public LockScreen()
        {
            InitializeComponent();
            //load our recognizers from storage
            timeLabel.BackColor = Color.Transparent;
            curDate = DateTime.UtcNow;
            timeLabel.Text = curDate.Hour.ToString() + ":" + curDate.Minute.ToString();
            ProcessModule objCurrentModule = Process.GetCurrentProcess().MainModule;
            objKeyboardProcess = new LowLevelKeyboardProc(captureKey);
            ptrHook = SetWindowsHookEx(13, objKeyboardProcess, GetModuleHandle(objCurrentModule.ModuleName), 0);
        }

        private void LockScreen_Load(object sender, EventArgs e)
        {
            this.TopMost = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            this.Show();
            this.Activate();

            for (int i = 0; i < Properties.Settings.Default.numberOfProfiles; i++)
            {
                FaceRecognizer recogniser = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
                recogniser.Read(Application.StartupPath + $"\\Profiles\\profile{i + 1}.txt");
                recognisers.Add(recogniser);
            }

            //set the lockscreen form to fullscreen on-top of all windows


            //setup the video capture device
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter) 
            { 
                if(device.Name == Properties.Settings.Default.webcam)
                {
                    cam = new VideoCaptureDevice(device.MonikerString);
                }
            }
            cam.Start();

            cam.NewFrame += Cam_NewFrame;
          
        }

        private void LockScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }


        #endregion

        #region main method

        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //Get the current frame form capture device
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            currentFrame = bitmap.ToImage<Bgr, byte>().Resize(320, 240, Inter.Cubic);
            grayFrame = currentFrame.Convert<Gray, byte>();

            //Convert it to Grayscale
            if (grayFrame != null)
            {
                //Face Detector
                Rectangle[] facesDetected = classifier.DetectMultiScale(grayFrame);
                //Action only for most promissing detection
                for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                {
                    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                    //of the background noise
                    facesDetected[0].X += (int)(facesDetected[0].Height * 0.15);
                    facesDetected[0].Y += (int)(facesDetected[0].Width * 0.22);
                    facesDetected[0].Height -= (int)(facesDetected[0].Height * 0.3);
                    facesDetected[0].Width -= (int)(facesDetected[0].Width * 0.35);
                    result = grayFrame.Copy(facesDetected[0]).Resize(100, 100, Inter.Cubic);
                    result._EqualizeHist();
                    foreach(FaceRecognizer recogniser in recognisers)
                    {
                        if (recogniser.Predict(result).Label == 0)
                        {
                            
                            CloseMethod method = new CloseMethod(CloseForm);
                            this.BeginInvoke(method);
                        }
                    }
                    //foreach(FaceRecognizer recogniser in recognisers)
                    //{
                    //    detectionLabel = recogniser.Predict(result).Label;
                    //}

                }
            }
        }
        private void CloseForm()
        {
            cam.Stop();
            cam.NewFrame -= Cam_NewFrame;
            if (!this.IsDisposed)
            {
                if (this.InvokeRequired)
                {
                    CloseMethod method = new CloseMethod(CloseForm);
                    this.Invoke(method);
                }
                else
                {
                    this.Close();
                }
            }
        }



        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            curDate = DateTime.UtcNow;
            string hrs = (curDate.Hour < 10) ? "0" + curDate.Hour.ToString() : curDate.Hour.ToString();
            string mins = (curDate.Minute < 10) ? "0" + curDate.Minute.ToString() : curDate.Minute.ToString();
            timeLabel.Text = hrs+ ":" + mins;
        }


        #region Suppress Keyboard Inputs
        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Starts Here */

        

        private IntPtr captureKey(int nCode, IntPtr wp, IntPtr lp)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT objKeyInfo = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lp, typeof(KBDLLHOOKSTRUCT));

                // Disabling Windows keys 
                if(objKeyInfo.key == Keys.Escape && !HasAltModifier(objKeyInfo.flags) && (ModifierKeys & Keys.Control) != Keys.Control)
                {
                    this.passWrdBx.Visible = true;
                    this.submitPass.Visible = true;
                    return (IntPtr)0;
                }
                else if (objKeyInfo.key == Keys.RWin || objKeyInfo.key == Keys.LWin || objKeyInfo.key == Keys.Tab && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.Escape && (ModifierKeys & Keys.Control) == Keys.Control || objKeyInfo.key == Keys.Escape && HasAltModifier(objKeyInfo.flags) || objKeyInfo.key == Keys.F4 && HasAltModifier(objKeyInfo.flags))
                {
                    return (IntPtr)1; // if 0 is returned then All the above keys will be enabled
                }
            }
            return CallNextHookEx(ptrHook, nCode, wp, lp);
        }

        bool HasAltModifier(int flags)
        {
            return (flags & 0x20) == 0x20;
        }

        /* Code to Disable WinKey, Alt+Tab, Ctrl+Esc Ends Here */
        #endregion


        #region password methods
        private void passWrdBx_Click(object sender, EventArgs e)
        {
            this.passWrdBx.PasswordChar = '*';
            this.passWrdBx.Text = "";
        }

        private void passWrdBx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == (char)Keys.Return)
            {
                checkPassword();
            }
        }

        private void submitPass_Click(object sender, EventArgs e)
        {
            checkPassword();
        }

        private void checkPassword() {
            if(passWrdBx.Text == Resources.Pasword){
                CloseMethod method = new CloseMethod(CloseForm);
                this.BeginInvoke(method);
            }
            else {
                this.passWrdBx.Text = "";
            }
        }
        #endregion
    }
}
