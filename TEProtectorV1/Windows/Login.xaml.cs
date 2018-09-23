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
using TEProtectorV1.UserControlContent;
using System.Net;
using System.Net.Mail;


namespace TEProtectorV1.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public LoginContent login;
        public SignUpContent signup;
        public ForgotPasswordContent forgotpass;
        
        public Login()
        {
            InitializeComponent();

            var background = new SolidColorBrush(Colors.DarkGreen);
            gridLogin.Background = background;

            login = new LoginContent();
            signup = new SignUpContent();
            forgotpass = new ForgotPasswordContent();
            

            gridAccount.Children.Add(login);
            gridAccount.Children.Add(signup);
            gridAccount.Children.Add(forgotpass);

            gridAccount.Children[0].Visibility = Visibility.Visible;
            gridAccount.Children[1].Visibility = Visibility.Hidden;
            gridAccount.Children[2].Visibility = Visibility.Hidden;


            login.btnLogin.Click += (sender, e) =>
            {
                using (var db = new AccountTEDBContext())
                {
                    Account Account = db.Accounts.Where(p => p.Email == login.EmailBox.Text && p.Password == login.PasswordBox.Password).SingleOrDefault();
                    if (Account != null)
                    {
                        Properties.Settings.Default.isLogin = true;
                        Properties.Settings.Default.idaccess = login.EmailBox.Text;
                        Properties.Settings.Default.Save();
                        MainWindow main = new MainWindow();
                        main.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu của bạn không chính xác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            };


            signup.btnSignUp.Click += (sender, e) =>
            {
                if (IsValidEmail(signup.txtEmail.Text) == true)
                {
                    if (signup.txtPassword.Password.Length >= 8)
                    {
                        if (signup.txtPassword.Password == signup.txtConfirmPassword.Password)
                        {
                            ComboBoxItem typeitem = (ComboBoxItem)signup.cbQuestion.SelectedItem;
                            string question = typeitem.Content.ToString();
                            using (var db = new AccountTEDBContext())
                            {
                                Account a = db.Accounts.Where(p => p.Email == signup.txtEmail.Text).SingleOrDefault();
                                if (a == null)
                                {
                                    db.Accounts.Add(new Account { Email = signup.txtEmail.Text, Password = signup.txtPassword.Password, Question = question, Answer = signup.txtAnswer.Text });
                                    db.SaveChanges();
                                    MessageBox.Show("Tạo tài khoản thành công", "Successful", MessageBoxButton.OK);
                                    signup.txtEmail.Text = signup.txtPassword.Password = signup.txtConfirmPassword.Password = signup.txtAnswer.Text = "";
                                }
                                else
                                    MessageBox.Show("Email đã trùng, vui lòng nhập email khác", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                            MessageBox.Show("Mật khẩu không trùng khớp, vui lòng kiểm tra lại!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                        MessageBox.Show("Mật khẩu phải lớn hơn hoặc bằng 8 kí tự!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                    MessageBox.Show("Email không hợp lệ hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            };

            forgotpass.btnSend.Click += (sender, e) =>
            {
                ComboBoxItem typeitem = (ComboBoxItem)forgotpass.cbQuestion.SelectedItem;
                string question = typeitem.Content.ToString();
                using (var db = new AccountTEDBContext())
                {
                    Account Account = db.Accounts.Where(p => p.Email == forgotpass.txtEmail.Text && p.Question == question && p.Answer == forgotpass.txtAnswer.Text).SingleOrDefault();
                    if (Account != null)
                    {
                        string getpass = GetPassowrd();
                        try
                        {
                            var fromAddress = new MailAddress("teprotector@gmail.com", "TEProtector");
                            var toAddress = new MailAddress(forgotpass.txtEmail.Text);
                            string fromPassword = "Teprotector123";
                            string subject = "Khôi phục mật khẩu !";
                            string body = "Mật khẩu của bạn đã được đổi thành: " + getpass;

                            var smtp = new SmtpClient
                            {
                                Host = "smtp.gmail.com",
                                Port = 587,
                                EnableSsl = true,
                                DeliveryMethod = SmtpDeliveryMethod.Network,
                                UseDefaultCredentials = false,
                                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                            };
                            using (var message = new MailMessage(fromAddress, toAddress)
                            {
                                Subject = subject,
                                Body = body
                            })
                            {
                                smtp.Send(message);
                            }
                            MessageBox.Show("Mất khẩu của bạn đã được gửi đến email của bạn, vui lòng kiểm tra email, mọi vấn đề xin vui lòng liên lạc chúng tôi ở phần hỗ trợ.", "Thành công", MessageBoxButton.OK);
                            Account.Password = getpass;
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Kết nối mạng pleaseee !!!!!", "Error", MessageBoxButton.OK);
                        }
                    }
                    else
                        MessageBox.Show("Email bạn không đúng hoặc sai câu trả lời.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtnLogin_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            gridAccount.Children[0].Visibility = Visibility.Visible;
            gridAccount.Children[1].Visibility = Visibility.Hidden;
            gridAccount.Children[2].Visibility = Visibility.Hidden;

            var background = new SolidColorBrush(Colors.DarkGreen);
            gridLogin.Background = background;
            gridSignup.Background = Brushes.Transparent;
            gridForgotPassowrd.Background = Brushes.Transparent;
        }

        private void BtnSignup_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            gridAccount.Children[1].Visibility = Visibility.Visible;
            gridAccount.Children[0].Visibility = Visibility.Hidden;
            gridAccount.Children[2].Visibility = Visibility.Hidden;

            var background = new SolidColorBrush(Colors.DarkGreen);
            gridSignup.Background = background;
            gridLogin.Background = Brushes.Transparent;
            gridForgotPassowrd.Background = Brushes.Transparent;
        }

        private void BtnForgot_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            gridAccount.Children[2].Visibility = Visibility.Visible;
            gridAccount.Children[0].Visibility = Visibility.Hidden;
            gridAccount.Children[1].Visibility = Visibility.Hidden;

            var background = new SolidColorBrush(Colors.DarkGreen);
            gridForgotPassowrd.Background = background;
            gridSignup.Background = Brushes.Transparent;
            gridLogin.Background = Brushes.Transparent;
        }

        private string GetPassowrd()
        {
            Random pass = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyz0123456789";
            var chars = Enumerable.Range(0, 12)
                .Select(x => pool[pass.Next(0, pool.Length)]);
            return new string(chars.ToArray());
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
