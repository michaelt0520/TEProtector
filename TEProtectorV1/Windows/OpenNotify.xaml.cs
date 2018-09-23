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
using System.Windows.Shapes;
using TEProtectorV1.TEProtectorDB;

namespace TEProtectorV1.Windows
{
    /// <summary>
    /// Interaction logic for OpenNotify.xaml
    /// </summary>
    public partial class OpenNotify : Window
    {
        public MainWindow main;
        public System.Windows.Forms.NotifyIcon mainnotify;
        public OpenNotify(MainWindow a, System.Windows.Forms.NotifyIcon aaa)
        {
            main = a;
            mainnotify = aaa;
            InitializeComponent();
            txtPass.Password = "";
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountTEDBContext())
            {
                Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                if (txtPass.Password == account.Password)
                {
                    main.ShowInTaskbar = true;
                    mainnotify.Visible = false;
                    main.WindowState = WindowState.Normal;
                    Hide();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
        
    }
}
