using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using TEProtectorV1.TabsController;
using TEProtectorV1.UserControlContent;
using System.Windows.Threading;

namespace TEProtectorV1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.NotifyIcon notifyIcon;

        public HomeController showhome;
        public ChildModeController showchildmode;
        public FeaturesController showfeatures;
        public SettingController showsetting;

        Windows.OpenNotify notify;
        public DisplayName idaccount;

        public DispatcherTimer showclock;

        public MainWindow()
        {
            InitializeComponent();

            notifyIcon = new System.Windows.Forms.NotifyIcon();
            notifyIcon.MouseDoubleClick += NotifyIcon_MouseDoubleClick;
            notifyIcon.Icon = new System.Drawing.Icon("Images/Logo/Icon.ico");

            notify = new Windows.OpenNotify(this, notifyIcon);
            idaccount = new DisplayName();

            gridAccount.Children.Add(idaccount);

            gridAccount.Children[0].Visibility = Visibility.Visible;

            idaccount.btnLogout.Click += (sender, e) =>
            {
                Properties.Settings.Default.isLogin = false;
                Properties.Settings.Default.idaccess = "";
                Properties.Settings.Default.Save();

                showhome.timerforshortbreak.Stop();
                showhome.timerforusing.Stop();

                Windows.Login lg = new Windows.Login();
                lg.Show();
                this.Close();

            };
            idaccount.idaccount.Click += (sender, e) =>
            {
                Windows.ChangePassword changepass = new Windows.ChangePassword();
                changepass.ShowDialog();
            };

            showhome = new HomeController(this);
            showchildmode = new ChildModeController();
            showfeatures = new FeaturesController();
            showsetting = new SettingController();

            showTabs.Children.Add(showhome.home);
            showTabs.Children.Add(showchildmode.childmode);
            showTabs.Children.Add(showfeatures.features);
            showTabs.Children.Add(showsetting.setting);

            showTabs.Children[0].Visibility = Visibility.Visible;
            
            this.MaxHeight = SystemParameters.WorkArea.Height;

            showclock = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            showclock.Tick += ShowClock;
            showclock.Start();
        }

        void ShowClock(object sender, EventArgs e)
        {
            string getTime = DateTime.Now.ToString("hh:mm");
            DateTime currentTime = Convert.ToDateTime(getTime);
            txtShowClock.Text = currentTime.ToString("hh:mm");
            txttt.Text = currentTime.ToString("tt", CultureInfo.InvariantCulture);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Windows.FormClose a = new Windows.FormClose();
            a.ShowDialog();
            if (a.isExited == true)
            {
                Properties.Settings.Default.isLogin = false;
                Properties.Settings.Default.idaccess = "";
                Properties.Settings.Default.Save();
                Process.Start("CopyHostsFile\\DeleteHostsFile.exe");
                Environment.Exit(0);
            }
        }


        private void BtnMinSize_Click(object sender, RoutedEventArgs e)
        {
            notifyIcon.Visible = true;
            WindowState = WindowState.Minimized;
            ShowInTaskbar = false;
        }
        private void NotifyIcon_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            notify.txtPass.Password = "";
            notify.Show();
        }

        private void BtnMaxSize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            showTabs.Children[0].Visibility = Visibility.Visible;
            showTabs.Children[1].Visibility = Visibility.Hidden;
            showTabs.Children[2].Visibility = Visibility.Hidden;
            showTabs.Children[3].Visibility = Visibility.Hidden;
        }

        private void BtnChildMode_Click(object sender, RoutedEventArgs e)
        {
            showTabs.Children[1].Visibility = Visibility.Visible;
            showTabs.Children[0].Visibility = Visibility.Hidden;
            showTabs.Children[2].Visibility = Visibility.Hidden;
            showTabs.Children[3].Visibility = Visibility.Hidden;
        }
        private void BtnFeatures_Click(object sender, RoutedEventArgs e)
        {
            showTabs.Children[2].Visibility = Visibility.Visible;
            showTabs.Children[0].Visibility = Visibility.Hidden;
            showTabs.Children[1].Visibility = Visibility.Hidden;
            showTabs.Children[3].Visibility = Visibility.Hidden;
        }
        private void BtnSetting_Click(object sender, RoutedEventArgs e)
        {
            showTabs.Children[3].Visibility = Visibility.Visible;
            showTabs.Children[0].Visibility = Visibility.Hidden;
            showTabs.Children[1].Visibility = Visibility.Hidden;
            showTabs.Children[2].Visibility = Visibility.Hidden;
        }


        private void BtnHelp_Click(object sender, RoutedEventArgs e)
        {
            Windows.Tutorial tut = new Windows.Tutorial();
            tut.ShowDialog();
        }

        private void BtnAbout_Click(object sender, RoutedEventArgs e)
        {
            Windows.AboutUs about = new Windows.AboutUs();
            about.ShowDialog();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    Windows.FormClose a = new Windows.FormClose();
        //    a.ShowDialog();
        //    if (a.isExited == true)
        //    {
        //        //if (detect.CheckTask("TEProtectorDuo") == true)
        //        //{
        //        //    detect.EndTask("TEProtectorDuo");
        //        //}
        //        //detectapp.Abort();
        //        //e.Cancel = false;
        //        Application.Current.Shutdown();
        //    }
        //    else
        //        e.Cancel = true;
        //}

        //DetectApp detect;
        //Thread detectapp;

        //private void Window_ContentRendered(object sender, EventArgs e)
        //{
        //    detect = new DetectApp();
        //    detectapp = new Thread(detect.DenyApp);
        //    detectapp.IsBackground = true;
        //    detectapp.Start();
        //}

        private void Label_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
        }
        
        private void ChildMode_Click(object sender, RoutedEventArgs e)
        {
            if (ChildMode.IsChecked == true)
            {
                var On = new Uri(@"/TEProtectorV1;component/Images/OnOff/On-enable.png", UriKind.Relative);
                OnChildMode.Source = new BitmapImage(On);

                var Off = new Uri(@"/TEProtectorV1;component/Images/OnOff/Off-disable.png", UriKind.Relative);
                OffChildMode.Source = new BitmapImage(Off);
            }
            else
            {
                var On = new Uri(@"/TEProtectorV1;component/Images/OnOff/On-disable.png", UriKind.Relative);
                OnChildMode.Source = new BitmapImage(On);

                var Off = new Uri(@"/TEProtectorV1;component/Images/OnOff/Off-enable.png", UriKind.Relative);
                OffChildMode.Source = new BitmapImage(Off);
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                gridbtnHome.Width = 200;
                txtHome.Height = 22;
                txtHome.TextWrapping = TextWrapping.NoWrap;

                gridBtnChildMode.Width = 200;
                txtChildMode.Height = 22;
                txtChildMode.TextWrapping = TextWrapping.NoWrap;

                gridbtnFeatures.Width = 200;
                txtFeatures.Height = 22;
                txtFeatures.TextWrapping = TextWrapping.NoWrap;

                gridbtnSetting.Width = 200;
                txtSetting.Height = 22;
                txtSetting.TextWrapping = TextWrapping.NoWrap;
            }
            else
            {
                gridbtnHome.Width = 140;
                txtHome.Height = 40;
                txtHome.TextWrapping = TextWrapping.Wrap;

                gridBtnChildMode.Width = 140;
                txtChildMode.Height = 40;
                txtChildMode.TextWrapping = TextWrapping.Wrap;

                gridbtnFeatures.Width = 140;
                txtFeatures.Height = 40;
                txtFeatures.TextWrapping = TextWrapping.Wrap;

                gridbtnSetting.Width = 100;
                txtSetting.Height = 41;
                txtSetting.TextWrapping = TextWrapping.Wrap;
            }
        }

    }
}
