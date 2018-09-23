using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TEProtectorV1.TabsController;
using TEProtectorV1.UserControlContent;
using System.Windows.Threading;
using System.Diagnostics;
using TEProtectorV1.TEProtectorDB;
using System.Globalization;

namespace TEProtectorV1.Windows
{
    /// <summary>
    /// Interaction logic for LockScreen.xaml
    /// </summary>
    public partial class LockScreen
    {
        HomeController home;
        private string PassUnlock { get; set; }
        private bool EnablePassword { get; set; }
        private int Timeshutdown { get; set; }
        private int Timeshortbreaklock { get; set; }
        private int Timelongbreaklock { get; set; }

        private bool TransparentLockScreen { get; set; }
        private bool ColorLockScreen { get; set; }
        private string HexColor { get; set; }
        private bool ImageLockScreen { get; set; }
        private string LinkImage { get; set; }

        private bool LockSoundDefault { get; set; }
        private string NameLockSoundDefault { get; set; }
        private bool LockSoundCustom { get; set; }
        private string LinkLockSoundCustom { get; set; }

        public DispatcherTimer timerforshutdown;
        public DispatcherTimer timerforshortbreaklock;
        public DispatcherTimer timerforlongbreaklock;
        private DispatcherTimer showclock;

        private MediaPlayer LockSound;
        private bool IsPlay { get; set; }
        private bool IsMute { get; set; }

        public LockScreen(HomeController a, string mode)
        {
            InitializeComponent();

            home = a;
            LockSound = new MediaPlayer();
            
            ImageBrush imageExcercise = new ImageBrush();
            imageExcercise.ImageSource = new BitmapImage(new Uri(GetExercise(), UriKind.Relative));
            gridExercise.Background = imageExcercise;

            SetWarning();

            using (var db = new AccountTEDBContext())
            {
                Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();

                Timeshutdown = account.timeshutdown * 60;
                Timeshortbreaklock = account.shortbreaklock;
                Timelongbreaklock = account.longbreaklock * 60;

                EnablePassword = account.enablepass;
                PassUnlock = account.Password;

                TransparentLockScreen = account.manhinhmo;
                ColorLockScreen = account.manhinhdonsac;
                if (ColorLockScreen == true)
                    HexColor = account.mamau;
                ImageLockScreen = account.manhinhhinhanh;
                if (ImageLockScreen == true)
                    LinkImage = GetLink(account.linkanh);

                LockSoundDefault = account.locksounddefault;
                if (LockSoundDefault == true)
                    NameLockSoundDefault = account.namelocksounddefault;
                LockSoundCustom = account.locksoundcustom;
                if (LockSoundCustom == true)
                    LinkLockSoundCustom = GetLink(account.linksoundcustom);

            }
            if(TransparentLockScreen == true)
            {
                var background = new SolidColorBrush(Colors.Black) { Opacity = 0.7 };
                gridDisplayColor.Background = background;
            }
            else if(ColorLockScreen == true)
            {
                BrushConverter bc = new BrushConverter();
                var background = new SolidColorBrush();
                background = (SolidColorBrush)bc.ConvertFrom(HexColor);
                gridDisplayColor.Background = background;
            }
            else if(ImageLockScreen == true)
            {
                ImageBrush myBrush = new ImageBrush();
                Image image = new Image() { Source = new BitmapImage(new Uri(LinkImage)) };
                myBrush.ImageSource = image.Source;
                gridDisplayBackground.Background = myBrush;
            }

            if(LockSoundDefault == true)
            {
                switch (NameLockSoundDefault)
                {
                    case "Nhạc số 1":
                        LockSound.Open(new Uri("Sound/LockSound/Sound1.mp3", UriKind.Relative));
                        LockSound.Play();
                        IsPlay = true;
                        break;
                    case "Nhạc số 2":
                        LockSound.Open(new Uri("Sound/LockSound/Sound2.mp3", UriKind.Relative));
                        LockSound.Play();
                        IsPlay = true;
                        break;
                    case "Nhạc số 3":
                        LockSound.Open(new Uri("Sound/LockSound/Sound3.mp3", UriKind.Relative));
                        LockSound.Play();
                        IsPlay = true;
                        break;
                    case "Nhạc số 4":
                        LockSound.Open(new Uri("Sound/LockSound/Sound4.mp3", UriKind.Relative));
                        LockSound.Play();
                        IsPlay = true;
                        break;
                    case "Nhạc số 5":
                        LockSound.Open(new Uri("Sound/LockSound/Sound5.mp3", UriKind.Relative));
                        LockSound.Play();
                        IsPlay = true;
                        break;
                }
            }
            else if(LockSoundCustom == true)
            {
                LockSound.Open(new Uri(LinkLockSoundCustom));
                LockSound.Play();
                IsPlay = true;
            }
            

            PasswordBox.IsEnabled = (EnablePassword == true) ? true : false;

            timerforshutdown = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            timerforshutdown.Tick += ShutdownWindows;

            timerforshortbreaklock = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            timerforshortbreaklock.Tick += TimeShortBreakUnlock;

            timerforlongbreaklock = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            timerforlongbreaklock.Tick += TimeLongBreakUnlock;

            showclock = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            showclock.Tick += ShowClock;
            showclock.Start();

            if (mode == "shutdown")
                timerforshutdown.Start();
            else if(mode == "shortbreak")
                timerforshortbreaklock.Start();
            else
                timerforlongbreaklock.Start();
        }
        void ShowClock(object sender, EventArgs e)
        {
            string getTime = DateTime.Now.ToString("hh:mm tt");
            DateTime currentTime = Convert.ToDateTime(getTime);
            txtShowTime.Text = currentTime.ToString("hh:mm");
        }

        private void ShutdownWindows(object sender, EventArgs e)
        {
            Timeshutdown -= 1;
            if (Timeshutdown == 0)
                Process.Start("shutdown", "/s /t 0");
            else
            {
                TimeSpan c = TimeSpan.FromSeconds(Timeshutdown);
                txtUnlockTimeLeft.Text = c.ToString(@"mm\:ss");
            }
        }
        private void TimeShortBreakUnlock(object sender, EventArgs e)
        {
            Timeshortbreaklock -= 1;
            if (Timeshortbreaklock == 0)
                Unlock();
            else
            {
                TimeSpan c = TimeSpan.FromSeconds(Timeshortbreaklock);
                txtUnlockTimeLeft.Text = c.ToString(@"mm\:ss");
            }
        }
        private void TimeLongBreakUnlock(object sender, EventArgs e)
        {
            Timelongbreaklock -= 1;
            if (Timelongbreaklock == 0)
                Unlock();
            else
            {
                TimeSpan c = TimeSpan.FromSeconds(Timelongbreaklock);
                txtUnlockTimeLeft.Text = c.ToString(@"mm\:ss");
            }
        }

        private void Unlock()
        {
            LockSound.Stop();
            Close();
        }

        private void BtnUnlock_Click(object sender, RoutedEventArgs e)
        {
            if (EnablePassword == true)
            {
                if (PasswordBox.Password == PassUnlock)
                    Unlock();
            }
            else
                Unlock();
        }

        private string GetLink(string link)
        {
            char[] a = link.ToCharArray();
            string b = "";
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == '\\')
                    b += "\\\\";
                else
                    b += a[i];
            }
            return b;
        }

        private void tgMuteUnMuteLockSound_Click(object sender, RoutedEventArgs e)
        {
            if (IsMute == true)
            {
                LockSound.Volume = 1;
                IsMute = false;
            }
            else
            {
                LockSound.Volume = 0;
                IsMute = true;
            }
        }

        private void tgPlayPauseLockSound_Click(object sender, RoutedEventArgs e)
        {
            if (IsPlay == true)
            {
                LockSound.Pause();
                IsPlay = false;
            }
            else
            {
                LockSound.Play();
                IsPlay = true;
            }
        }

        private string GetExercise()
        {
            Random rd = new Random();
            string linkExercise = "";
            int a = rd.Next(1, 9);
            switch (a)
            {
                case 1:
                    linkExercise = "Images/Excercise/DongTac1.png";
                    break;
                case 2:
                    linkExercise = "Images/Excercise/DongTac2.png";
                    break;
                case 3:
                    linkExercise = "Images/Excercise/DongTac3.png";
                    break;
                case 4:
                    linkExercise = "Images/Excercise/DongTac4.png";
                    break;
                case 5:
                    linkExercise = "Images/Excercise/DongTac5.png";
                    break;
                case 6:
                    linkExercise = "Images/Excercise/DongTac6.png";
                    break;
                case 7:
                    linkExercise = "Images/Excercise/DongTac7.png";
                    break;
                case 8:
                    linkExercise = "Images/Excercise/DongTac8.png";
                    break;
            }
            return linkExercise;
        }

        private void SetWarning()
        {
            Random random = new Random();
            List<string> listwarning = new List<string>();

            listwarning.Add("Ngồi lâu trước máy tính khiến dây chằng trở nên yếu.");
            listwarning.Add("Ngồi lâu trước máy tính khiến phổi giảm khả năng hấp thu oxy.");
            listwarning.Add("Ngồi lâu trước máy tính gây ảnh hưởng đến thị lực.");
            listwarning.Add("Ngồi lâu trước máy tính có thể dẫn đến chứng hay quên, đãng trí.");
            listwarning.Add("Ngồi lâu trước máy tính gây ảnh hưởng nhan sắc trầm trọng.");
            listwarning.Add("Ngồi lâu trước máy tính có thể dẫn đến vô sinh.");
            listwarning.Add("Ngồi lâu trước máy tính có thể khiến bạn mắc bệnh trĩ.");

            int a = random.Next(1, 8);
            txtWarning.Text = listwarning[a];
        }

        #region Disappear in alt tab
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper wndHelper = new WindowInteropHelper(this);

            int exStyle = (int)GetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE);

            exStyle |= (int)ExtendedWindowStyles.WS_EX_TOOLWINDOW;
            SetWindowLong(wndHelper.Handle, (int)GetWindowLongFields.GWL_EXSTYLE, (IntPtr)exStyle);
        }


        [Flags]
        public enum ExtendedWindowStyles
        {
            // ...
            WS_EX_TOOLWINDOW = 0x00000080,
            // ...
        }

        public enum GetWindowLongFields
        {
            // ...
            GWL_EXSTYLE = (-20),
            // ...
        }

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowLong(IntPtr hWnd, int nIndex);

        public static IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
        {
            int error = 0;
            IntPtr result = IntPtr.Zero;
            // Win32 SetWindowLong doesn't clear error on success
            SetLastError(0);

            if (IntPtr.Size == 4)
            {
                // use SetWindowLong
                Int32 tempResult = IntSetWindowLong(hWnd, nIndex, IntPtrToInt32(dwNewLong));
                error = Marshal.GetLastWin32Error();
                result = new IntPtr(tempResult);
            }
            else
            {
                // use SetWindowLongPtr
                result = IntSetWindowLongPtr(hWnd, nIndex, dwNewLong);
                error = Marshal.GetLastWin32Error();
            }

            if ((result == IntPtr.Zero) && (error != 0))
            {
                throw new System.ComponentModel.Win32Exception(error);
            }

            return result;
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        private static extern IntPtr IntSetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern Int32 IntSetWindowLong(IntPtr hWnd, int nIndex, Int32 dwNewLong);

        private static int IntPtrToInt32(IntPtr intPtr)
        {
            return unchecked((int)intPtr.ToInt64());
        }

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(int dwErrorCode);
        #endregion

        //detect alt + f4...
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Alt && e.SystemKey == Key.F4 || Keyboard.Modifiers == ModifierKeys.Control && e.SystemKey == Key.Escape)
            {
                e.Handled = true;
            }
            else
            {
                base.OnKeyDown(e);
            }
        }
    }
}
