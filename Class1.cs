using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

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

namespace FinalYearProject
{
    public partial class Settings : Form
    {
        #region Face detection variables

        VideoCaptureDevice cam;
        FilterInfoCollection filter;
        FaceRecognizer recognizer = new LBPHFaceRecognizer(1, 8, 8, 8, 100);
        Form lockscreen = new LockScreen();
        String[] names = { "User" };
        List<int> faceLabels = new List<int>();
        //Images for finding face
        Image<Bgr, byte> currentFrame;
        private int prediction;
        Image<Gray, byte> result;
        Image<Gray, byte> grayFrame;

        //For aquiring 10 images in a row
        List<Mat> resultImages = new List<Mat>();

        //Classifier
        static readonly CascadeClassifier cascadeClassifier = new CascadeClassifier(Resources.haarcascade_frontalcatface_extended);//Our face detection method ;

        #endregion

        public Settings()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            
            
            filter = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filter)
                devicesCmb.Items.Add(device.Name);
            devicesCmb.SelectedIndex = 0;
            cam = new VideoCaptureDevice();
        }



        private void capture_Click(object sender, EventArgs e)
        {
            recognizer.Read(Application.StartupPath + "\\TrainingResult\\trainedRecognizer.txt");
            cam = new VideoCaptureDevice(filter[devicesCmb.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewFrame;
            cam.Start();

        }

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
                //MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20)); //old method
                Rectangle[] facesDetected = cascadeClassifier.DetectMultiScale(grayFrame, 1.2,1);

                //Action for each element detected
                for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                {
                    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                    //of the background noise
                    facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                    facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                    facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                    facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);
                    result = grayFrame.Copy(facesDetected[i]).Resize(100, 100, Inter.Cubic);
                    result._EqualizeHist();
                    prediction = recognizer.Predict(result).Label;
                    example.Image = result.ToBitmap();
                    string person = (prediction >= 0) ? names[prediction] : "Unknown";
                    //draw the face detected in the 0th (gray) channel with blue color
                    currentFrame.Draw(person, new Point(facesDetected[i].X, facesDetected[i].Y), FontFace.HersheyPlain, 1.5, new Bgr(Color.Cyan), 1, LineType.AntiAlias, false);
                    currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);
                }

            }

            pic.Image = currentFrame.Convert<Bgr, byte>().ToBitmap();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (cam.IsRunning)
            {
                recognizer.Dispose();
                cam.Stop();
            }
        }

        private void train_Click(object sender, EventArgs e)
        {
            cam = new VideoCaptureDevice(filter[devicesCmb.SelectedIndex].MonikerString);
            cam.NewFrame += Cam_NewTrainFrame;
            cam.Start();
        }

        private void Cam_NewTrainFrame(object sender, NewFrameEventArgs eventArgs)
        {
            //Get the current frame form capture device
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            currentFrame = bitmap.ToImage<Bgr, byte>().Resize(320, 240, Inter.Cubic);
            grayFrame = currentFrame.Convert<Gray, byte>();

            //Convert it to Grayscale
            if (grayFrame != null)
            {
                

                //Face Detector
                //MCvAvgComp[][] facesDetected = gray_frame.DetectHaarCascade(Face, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20)); //old method
                Rectangle[] facesDetected = cascadeClassifier.DetectMultiScale(grayFrame);

                //Action for each element detected
                for (int i = 0; i < facesDetected.Length; i++)// (Rectangle face_found in facesDetected)
                {
                    //This will focus in on the face from the haar results its not perfect but it will remove a majoriy
                    //of the background noise
                    facesDetected[i].X += (int)(facesDetected[i].Height * 0.15);
                    facesDetected[i].Y += (int)(facesDetected[i].Width * 0.22);
                    facesDetected[i].Height -= (int)(facesDetected[i].Height * 0.3);
                    facesDetected[i].Width -= (int)(facesDetected[i].Width * 0.35);

                    result = grayFrame.Copy(facesDetected[i]).Resize(100, 100, Inter.Cubic);
                    result._EqualizeHist();
                    example.Image = result.ToBitmap();
                    //draw the face detected in the 0th (gray) channel with blue color
                    currentFrame.Draw(facesDetected[i], new Bgr(Color.Red), 2);
                    resultImages.Add(result.Mat);
                    faceLabels.Add(0);
                }

            }

                pic.Image = currentFrame.Convert<Bgr, byte>().ToBitmap();
        }
        private void StopTarin_Click(object sender, EventArgs e)
        {
            
            
            cam.Stop();
            cam.NewFrame -= Cam_NewTrainFrame;
        }

        private void stopCap_Click(object sender, EventArgs e)
        {
            cam.Stop();
            cam.NewFrame -= Cam_NewFrame;
        }

        private void lockBtn_Click(object sender, EventArgs e)
        {
            lockscreen.Show();
        }
    }

        
}

