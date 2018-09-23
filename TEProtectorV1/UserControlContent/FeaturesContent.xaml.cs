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
    /// Interaction logic for FeaturesContent.xaml
    /// </summary>
    public partial class FeaturesContent : UserControl
    {
        public FeaturesContent()
        {
            InitializeComponent();
            using (var db = new AccountTEDBContext())
            {
                Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                chkbDisablePass.IsChecked = account.enablepass;
                chkbEnableNotification.IsChecked = account.enablenoti;
                txtSetupTimeNotification.Text = account.notitime.ToString();
                chkbDefaultNotification.IsChecked = account.defaultnoti;
                chkbCustomNotification.IsChecked = account.customnoti;
            }
        }
        
    }
}
