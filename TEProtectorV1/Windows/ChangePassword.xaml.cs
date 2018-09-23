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
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AccountTEDBContext())
            {
                Account account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                if (OldPassword.Password == account.Password && NewPassword.Password == ConFirmNewPassword.Password)
                {
                    account.Password = NewPassword.Password;
                    db.SaveChanges();
                    MessageBox.Show("Đổi mật khẩu thành công!", "Chú ý", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                else
                    MessageBox.Show("Vui lòng kiểm tra lại thông tin!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

    }
}
