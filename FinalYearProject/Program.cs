using FinalYearProject.Properties;
using System;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows.Forms;

namespace FinalYearProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MyCustomApplicationContext());
        }
    }

    public class MyCustomApplicationContext : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private static System.Timers.Timer lockTimer;
        public MyCustomApplicationContext()
        {
            // Initialize Tray Icon
            trayIcon = new NotifyIcon()
            {
                Icon = Resources.AppIcon,
                ContextMenu = new ContextMenu(new MenuItem[] {
                    new MenuItem("Exit", Exit), new MenuItem("Settings", showSettings)
                }),
                Visible = true
            };
            checkForProfiles();
            
        }

        void checkForProfiles() {
            if (Properties.Settings.Default.numberOfProfiles == 0)
            {
                MessageBox.Show("You have no face profiles enrolled! please add one via settings!");
                Form settings = new Settings();
                settings.ShowDialog();
                Properties.Settings.Default.Reload();
                checkForProfiles();
            }
            else
            {
                SetTimer();
            }
        }

        void showSettings(object sender, EventArgs e) {
            lockTimer.Stop();
            Form settings = new Settings();
            settings.ShowDialog();
            settings.Dispose();
            Properties.Settings.Default.Reload();
            SetTimer();
        }
        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            lockTimer = new System.Timers.Timer(1000);
            // Hook up the Elapsed event for the timer. 
            lockTimer.Elapsed += OnTimedEvent;
            lockTimer.AutoReset = true;
            lockTimer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            FormCollection openForms = Application.OpenForms;
            if (openForms.Count == 0) {
                uint lastInputMins = (GetIdleTime() / 60000);
                if (lastInputMins >= Properties.Settings.Default.locktimer)
                {
                    Properties.Settings.Default.locked = true;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                    Form lockscreen = new LockScreen();
                    lockscreen.ShowDialog();
                    lockscreen.Dispose();
                    Properties.Settings.Default.locked = false; ;
                    Properties.Settings.Default.Save();
                    Properties.Settings.Default.Reload();
                }
            }
        }


        void Exit(object sender, EventArgs e)
        {
            // Hide tray icon, otherwise it will remain shown until user mouses over it
            trayIcon.Visible = false;

            Application.Exit();
        }


        internal struct LASTINPUTINFO
        {
            public uint cbSize;

            public uint dwTime;
        }

        /// <summary>
        /// Helps to find the idle time, (in milliseconds) spent since the last user input
        /// </summary>

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        [DllImport("Kernel32.dll")]
        private static extern uint GetLastError();

        public static uint GetIdleTime()
        {
            LASTINPUTINFO lastInPut = new LASTINPUTINFO();
            lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
            GetLastInputInfo(ref lastInPut);
            return ((uint)Environment.TickCount - lastInPut.dwTime);
        }
    }
}
