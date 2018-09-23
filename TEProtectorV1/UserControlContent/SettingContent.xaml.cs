using System;
using System.Collections.Generic;
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
using TEProtectorV1.TEProtectorDB;

namespace TEProtectorV1.UserControlContent
{
    /// <summary>
    /// Interaction logic for SettingContent.xaml
    /// </summary>
    public partial class SettingContent : UserControl
    {
        public SettingContent()
        {
            InitializeComponent();
            using (var db = new AccountTEDBContext())
            {
                Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                chkbTransparentLockScreen.IsChecked = account.manhinhmo;
                chkbColorLockScreen.IsChecked = account.manhinhdonsac;
                txtHexColor.Text = account.mamau.ToString();
                chkbImageLockScreen.IsChecked = account.manhinhhinhanh;
                txtImageLockScreen.Text = account.linkanh;

                chkbLockAudio.IsChecked = account.locksounddefault;
                cbLockSoundDefault.SelectedValuePath = account.namelocksounddefault;
                chkbCustomLockAudio.IsChecked = account.locksoundcustom;
                txtLockSoundCustom.Text = account.linksoundcustom;

                chkbDefaultNotiAudio.IsChecked = account.notificationsounddefault;
                cbNotificationSoundDefault.SelectedValuePath = account.namenotificationsounddefault;
                chkbCustomNotiAudio.IsChecked = account.customnoti;
                txtNotiSoundCustom.Text = account.linknotificationsoundcustom;
            }
        }
        private void ClrPcker_Background_SelectedColorChanged_1(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            txtHexColor.Text = ClrPcker_Background.SelectedColor.ToString();
        }
    }
}
