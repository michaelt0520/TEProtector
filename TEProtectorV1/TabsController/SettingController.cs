using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEProtectorV1.UserControlContent;
using System.Windows;
using TEProtectorV1.TEProtectorDB;
using System.Windows.Controls;
using Microsoft.Win32;

namespace TEProtectorV1.TabsController
{
    public class SettingController
    {
        public SettingContent setting;

        public SettingController()
        {
            setting = new SettingContent
            {
                Visibility = Visibility.Hidden
            };

            setting.cbLockSoundDefault.SelectedIndex = 0;
            setting.cbNotificationSoundDefault.SelectedIndex = 0;

            setting.btnConfirm.Click += (sender, e) =>
            {
                ComboBoxItem getnamelocksounddefault = (ComboBoxItem)setting.cbLockSoundDefault.SelectedItem;
                ComboBoxItem getnamenotisounddefault = (ComboBoxItem)setting.cbNotificationSoundDefault.SelectedItem;
                using (var db = new AccountTEDBContext())
                {
                    Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                    account.manhinhmo = (setting.chkbTransparentLockScreen.IsChecked == true) ? true : false;
                    account.manhinhdonsac = (setting.chkbColorLockScreen.IsChecked == true) ? true : false;
                    account.mamau = setting.txtHexColor.Text;
                    account.manhinhhinhanh = (setting.chkbImageLockScreen.IsChecked == true) ? true : false;
                    account.linkanh = setting.txtImageLockScreen.Text;

                    account.locksounddefault = (setting.chkbLockAudio.IsChecked == true) ? true : false;
                    account.namelocksounddefault = getnamelocksounddefault.Content.ToString();
                    account.locksoundcustom = (setting.chkbCustomLockAudio.IsChecked == true) ? true : false;
                    account.linksoundcustom = setting.txtLockSoundCustom.Text;

                    account.notificationsounddefault = (setting.chkbDefaultNotiAudio.IsChecked == true) ? true : false;
                    account.namenotificationsounddefault = getnamenotisounddefault.Content.ToString();
                    account.customnoti = (setting.chkbCustomNotiAudio.IsChecked == true) ? true : false;
                    account.linknotificationsoundcustom = setting.txtNotiSoundCustom.Text;

                    db.SaveChanges();
                }
                
            };

            setting.btnReset.Click += (sender, e) =>
            {
                setting.txtImageLockScreen.Text = "";
                setting.txtLockSoundCustom.Text = "";
                setting.txtNotiSoundCustom.Text = "";
                setting.chkbTransparentLockScreen.IsChecked = true;
                setting.chkbLockAudio.IsChecked = true;
                setting.chkbDefaultNotiAudio.IsChecked = true;
            };

            setting.btnBrowse.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                { Filter = "JPG Files (*.jpg)|*.jpg|All Files (*.*)|*.*" };
                if (openFileDialog.ShowDialog() == true)
                    setting.txtImageLockScreen.Text = openFileDialog.FileName;
            };

            setting.btnBrowseLockAudio.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                { Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*" };
                if (openFileDialog.ShowDialog() == true)
                    setting.txtLockSoundCustom.Text = openFileDialog.FileName;
            };

            setting.btnBrowseNotiSound.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog()
                { Filter = "MP3 Files (*.mp3)|*.mp3|All Files (*.*)|*.*" };
                if (openFileDialog.ShowDialog() == true)
                    setting.txtNotiSoundCustom.Text = openFileDialog.FileName;
            };
        }
    }
}
