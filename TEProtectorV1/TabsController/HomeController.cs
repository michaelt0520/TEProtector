using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEProtectorV1.UserControlContent;
using TEProtectorV1.Windows;
using System.Windows;
using System.Windows.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TEProtectorV1.TEProtectorDB;
using Notifications.Wpf;
using System.Windows.Media;

namespace TEProtectorV1.TabsController
{
    public class HomeController
    {
        NotificationManager notificationManager;
        private bool EnableNotification { get; set; }
        private int TimeNotification { get; set; }
        private bool DefaultNotification { get; set; }
        private bool CustomNotification { get; set; }

        private bool NotiSoundDefault { get; set; }
        private string NameNotiSoundDefault { get; set; }
        private bool NotiSoundCustom { get; set; }
        private string LinkNotiSoundCustom { get; set; }

        private bool flagIsLocked { get; set; }

        public HomeContent home;

        public DispatcherTimer timerforusing;
        public DispatcherTimer timerforshortbreak;

        private MediaPlayer NotiSound;

        public int Timeusing { get; set; }
        public int Timeshutdown { get; set; }
        public int Timeshortbreak { get; set; }
        public int Timeshortbreaklock { get; set; }
        public int Timelongbreak { get; set; }
        public int Timelongbreaklock { get; set; }

        int countlongbreak = 0;

        public MainWindow b;
        public HomeController(MainWindow a)
        {
            home = new HomeContent()
            {
                Visibility = Visibility.Hidden
            };
            b = a;
            NotiSound = new MediaPlayer();

            notificationManager = new NotificationManager();

            timerforusing = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            timerforusing.Tick += ShowTimeUsing;

            timerforshortbreak = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 1000) };
            timerforshortbreak.Tick += TimeShortbreakLock;

            home.btnConfirm.Click += (sender, e) =>
            {
                Timeusing = int.Parse(home.txtSetupHour.Text);
                Timeshutdown = int.Parse(home.txtSetupShutdown.Text);
                Timeshortbreak = int.Parse(home.txtSetupShortBreakTime.Text);
                Timeshortbreaklock = int.Parse(home.txtSetupShortBreakLockTime.Text);
                Timelongbreak = int.Parse(home.txtSetupLongBreak.Text);
                Timelongbreaklock = int.Parse(home.txtSetupLongBreakLockTime.Text);

                using (var db = new AccountTEDBContext())
                {
                    Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                    account.timeusing = Timeusing;
                    account.timeshutdown = Timeshutdown;
                    account.shortbreaktime = Timeshortbreak;
                    account.shortbreaklock = Timeshortbreaklock;
                    account.longbreaktimes = Timelongbreak;
                    account.longbreaklock = Timelongbreaklock;

                    EnableNotification = account.enablenoti;
                    TimeNotification = account.notitime;
                    DefaultNotification = account.defaultnoti;
                    CustomNotification = account.customnoti;

                    NotiSoundDefault = account.notificationsounddefault;
                    if (NotiSoundDefault == true)
                        NameNotiSoundDefault = account.namenotificationsounddefault;
                    NotiSoundCustom = account.notificationsoundcustom;
                    if (NotiSoundCustom == true)
                        LinkNotiSoundCustom = GetLink(account.linknotificationsoundcustom);

                    db.SaveChanges();
                }

                TimeUsing();
                TimeShortbreak();

                b.showTimeUsing.Text = (Timeusing / 3600).ToString() + " Giờ";
                b.showTimeLock.Text = (Timeshortbreak / 60).ToString() + " Phút";
            };

            home.btnReset.Click += (sender, e) =>
            {
                home.txtSetupHour.Text = "1";
                home.txtSetupShutdown.Text = "1";
                home.txtSetupShortBreakTime.Text = "1";
                home.txtSetupShortBreakLockTime.Text = "1";
                home.txtSetupLongBreak.Text = "1";
                home.txtSetupLongBreakLockTime.Text = "1";

                b.showTimeUsing.Text = "0" + " Giờ";
                b.showTimeLock.Text = "0" + " Phút";

                timerforusing.Stop();
                timerforshortbreak.Stop();

                b.showTimeLeft.Text = "00:00";
                b.showTimeLockLeft.Text = "00:00";
            };
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

        #region Time using computer
        private void TimeUsing()
        {
            Timeusing = int.Parse(home.txtSetupHour.Text.ToString()) * 3600;
            timerforusing.Start();
        }

        private void ShowTimeUsing(object sender, EventArgs e)
        {
            Timeusing -= 1;
            if (Timeusing == 0)
            {
                LockScreen lockScreen = new LockScreen(this, "shutdown");
                timerforusing.Stop();
                if (flagIsLocked == false)
                {
                    flagIsLocked = true;
                    lockScreen.ShowDialog();
                }
                flagIsLocked = false;
                Timeusing = int.Parse(home.txtSetupHour.Text.ToString()) * 3600;
                timerforusing.Start();
            }
            else
            {
                MainWindow aa = b;
                TimeSpan c = TimeSpan.FromSeconds(Timeusing);
                aa.showTimeLeft.Text = c.ToString(@"hh\:mm\:ss");
            }
        }

        #endregion

        #region Short break
        private void TimeShortbreak()
        {
            Timeshortbreak = int.Parse(home.txtSetupShortBreakTime.Text.ToString()) * 60;
            timerforshortbreak.Start();
        }
        private void TimeShortbreakLock(object sender, EventArgs e)
        {
            Timeshortbreak -= 1;
            if (Timeshortbreak == 0)
            {
                timerforshortbreak.Stop();
                countlongbreak++;
                if (countlongbreak == Timelongbreak)
                    TimeLongbreak();
                else
                {
                    LockScreen lockScreen = new LockScreen(this, "shortbreak");
                    if (flagIsLocked == false)
                    {
                        flagIsLocked = true;
                        lockScreen.ShowDialog();
                    }
                    flagIsLocked = false;
                    Timeshortbreak = int.Parse(home.txtSetupShortBreakTime.Text.ToString()) * 60;
                    timerforshortbreak.Start();
                }
            }
            else
            {
                MainWindow aa = b;
                TimeSpan c = TimeSpan.FromSeconds(Timeshortbreak);
                aa.showTimeLockLeft.Text = c.ToString(@"mm\:ss");
            }
            if (Timeshortbreak == TimeNotification && EnableNotification == true && DefaultNotification == true)
            {
                notificationManager.Show(new NotificationContent
                {
                    Title = "Lưu ý !",
                    Message = "Máy của bạn sắp bị khóa, hãy hoàn thành nốt công việc đi nào.",
                    Type = NotificationType.Information
                });

                if (NotiSoundDefault == true)
                {
                    switch (NameNotiSoundDefault)
                    {
                        case "Nhạc số 1":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound1.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 2":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound2.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 3":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound3.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 4":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound4.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 5":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound5.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                    }
                }
                else if (NotiSoundCustom == true)
                {
                    NotiSound.Open(new Uri(LinkNotiSoundCustom));
                    NotiSound.Play();
                }
            }
            else if (Timeshortbreak == TimeNotification && EnableNotification == true && CustomNotification == true)
            {
                CustomNotification customNotification = new CustomNotification();
                customNotification.Show();

                if (NotiSoundDefault == true)
                {
                    switch (NameNotiSoundDefault)
                    {
                        case "Nhạc số 1":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound1.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 2":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound2.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 3":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound3.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 4":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound4.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                        case "Nhạc số 5":
                            NotiSound.Open(new Uri("Sound/NotiSound/Sound5.mp3", UriKind.Relative));
                            NotiSound.Play();
                            break;
                    }
                }
                else if (NotiSoundCustom == true)
                {
                    NotiSound.Open(new Uri(LinkNotiSoundCustom));
                    NotiSound.Play();
                }
            }
        }
        #endregion

        #region Long break
        private void TimeLongbreak()
        {
            LockScreen lockScreen = new LockScreen(this, "longbreak");
            if (flagIsLocked == false)
            {
                flagIsLocked = true;
                lockScreen.ShowDialog();
            }
            flagIsLocked = false;
            countlongbreak = 0;
            Timeshortbreak = int.Parse(home.txtSetupShortBreakTime.Text.ToString()) * 60;
            timerforshortbreak.Start();
        }
        #endregion
    }
}
