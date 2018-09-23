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
    /// Interaction logic for HomeContent.xaml
    /// </summary>
    public partial class HomeContent : UserControl
    {
        public HomeContent()
        {
            InitializeComponent();
            using (var db = new AccountTEDBContext())
            {
                Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                txtSetupHour.Text = account.timeusing.ToString();
                txtSetupShutdown.Text = account.timeshutdown.ToString();
                txtSetupShortBreakTime.Text = account.shortbreaktime.ToString();
                txtSetupShortBreakLockTime.Text = account.shortbreaklock.ToString();
                txtSetupLongBreak.Text = account.longbreaktimes.ToString();
                txtSetupLongBreakLockTime.Text = account.longbreaklock.ToString();
            }
        }
    }
}
