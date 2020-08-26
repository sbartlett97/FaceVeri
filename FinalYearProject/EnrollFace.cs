using System;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
using AForge.Video;
using AForge.Video.DirectShow;
using System.Collections.Generic;

namespace FinalYearProject
{
    public partial class EnrollFace : Form
    {
        #region Face detection variables

        //variables for finding camera devices and creating a capture device
        FilterInfoCollection filter;
        VideoCaptureDevice cam;

        //face recogniser to be trained and saved for the system
        FaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);

        //amount of usable face samples captured
        private int capturedSamples;

        //List for the training labels (they will all be the same but we need enough for each face sample)
        List<int> faceLabels = new List<int>();

        //List containing the image data for the face samples
        List<Mat> resultImages = new List<Mat>();

        //Images for finding face
        Image<Bgr, byte> currentFrame;
        Image<Gray, byte> result;
        Image<Gray, byte> grayFrame;

        //method invokers for updating the progress bar and closing the form.
        MethodInvoker mi;
        delegate void CloseMethod();

        //empty cascade classifier - updated with appropriate classifier for face detection when needed
        private CascadeClassifier cascadeClassifier;


        //= new CascadeClassifier(Resources.haarcascade_frontalcatface_extended);//Our face detection method ;

        #endregion
        public EnrollFace()
        {
            InitializeComponent();
            MessageBox.Show("This form will capture youre facial features for use in unlocking your device. If you are wearing glasses, please remember to tick the apropriate box.");
            mi = new MethodInvoker(() => capProgress.Value = capturedSamples);

        }

        private void EnrollFace_Load(object sender, EventArgs e)
        {
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter)
                if (device.Name == Properties.Settings.Default.webcam)
                    cam = new VideoCaptureDevice(device.MonikerString);

        }


        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //Get the current frame form capture device
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            currentFrame = bitmap.ToImage<Bgr, byte>().Resize(320, 240, Inter.Cubic);
            camViewer.Image = currentFrame.Convert<Bgr, byte>().ToBitmap();
            //show the user what the camera is seeing

            //convert the image into a format readable by the recogniser
            grayFrame = currentFrame.Convert<Gray, byte>();

            //Convert it to Grayscale
            if (grayFrame != null)
            {

                //Face Detector
                Rectangle[] facesDetected = cascadeClassifier.DetectMultiScale(grayFrame);

                //Action for each element detected
                for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                {
                    capturedSamples++;
                    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                    //of the background noise
                    facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                    facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                    facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                    facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                    result = grayFrame.Copy(facesDetected[i]).Resize(100, 100, Inter.Cubic);
                    result._EqualizeHist();
                    //camViewer.Image = result.ToBitmap();
                    //draw the face detected in the 0th (gray) channel with blue color
                    currentFrame.ROI = facesDetected[i];
                    resultImages.Add(result.Mat);
                    faceLabels.Add(0);
                    capturedSamples++;
                    if (capProgress.InvokeRequired && capProgress.Value < capProgress.Maximum)
                    {
                        capProgress.Invoke(mi);
                    }
                    else if(capProgress.Value < capProgress.Maximum)
                    {
                        mi.Invoke();
                    }

                    if (capturedSamples == 1000)
                    {
                        CloseMethod method = new CloseMethod(CloseForm);
                        this.BeginInvoke(method);
                    }
                }
            }
        }

        private void trainRecogniser() 
        {
            //pass our lists of image data and labels to the recogniser so it can generate a profile
            recognizer.Train(resultImages.ToArray(), faceLabels.ToArray());
           
            //once our recogniser is trained dispose of our unecessary resources to save memory
            faceLabels.Clear();
            resultImages.Clear();
            //save our new profile for use in LockScreen.cs
            recognizer.Write(Application.StartupPath + $"\\Profiles\\profile{(Properties.Settings.Default.numberOfProfiles + 1)}.txt");
            Properties.Settings.Default.numberOfProfiles++;
            Properties.Settings.Default.Save();
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
                    trainRecogniser();
                    Console.WriteLine("Form is now closing");
                    MessageBox.Show("Done! Form will now close");
                    this.Close();
                }
            }
        }

        private void beginCap_Click(object sender, EventArgs e)
        {
            capProgress.Visible = true;
            cascadeClassifier = new CascadeClassifier(Application.StartupPath + "/Cascades/haarcascade_frontalface_default.xml");
            cam.NewFrame += Cam_NewFrame;
            cam.Start();
        }

        private void camViewer_Click(object sender, EventArgs e)
        {

        }
    }
}
