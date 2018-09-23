using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEProtectorV1.UserControlContent;
using System.Windows;
using System.Diagnostics;
using TEProtectorV1.TEProtectorDB;

namespace TEProtectorV1.TabsController
{
    public partial class FeaturesController
    {
        public FeaturesContent features;
        private bool EnablePassword { get; set; }
        private bool EnableNotification { get; set; }
        private int TimeNotification { get; set; }
        private bool DefaultNotification { get; set; }
        private bool CustomNotification { get; set; }

        public FeaturesController()
        {
            features = new FeaturesContent
            {
                Visibility = Visibility.Hidden
            };


            features.btnConfirm.Click += (sender, e) =>
            {
                EnablePassword = (features.chkbDisablePass.IsChecked == true) ? true : false;
                EnableNotification = (features.chkbEnableNotification.IsChecked == true) ? true : false;
                TimeNotification = int.Parse(features.txtSetupTimeNotification.Text);
                DefaultNotification = (features.chkbDefaultNotification.IsChecked == true) ? true : false;
                CustomNotification = (features.chkbCustomNotification.IsChecked == true) ? true : false;

                using (var db = new AccountTEDBContext())
                {
                    Account Account = db.Accounts.Where(p => p.Email == Properties.Settings.Default.idaccess).SingleOrDefault();
                    Account.enablepass = EnablePassword;
                    Account.enablenoti = EnableNotification;
                    if (EnableNotification == true)
                    {
                        Account.notitime = TimeNotification;
                        Account.defaultnoti = DefaultNotification;
                        Account.customnoti = CustomNotification;
                    }
                    db.SaveChanges();
                }
            };

            features.btnReset.Click += (sender, e) =>
            {
                features.chkbDisablePass.IsChecked = false;
                features.chkbEnableNotification.IsChecked = false;
                features.sliderSetupNotification.Value = 30;
                features.chkbDefaultNotification.IsChecked = true;
            };
            
            features.btnNightLight.Click += (sender, e) =>
            {
                try
                {
                    Process.Start("ms-settings:nightlight");
                }
                catch
                {
                    MessageBox.Show("Bạn phải nâng cấp máy lên Windows 8 trở lên mới có thể sử dụng được tính năng này !", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            };
        }
    }
}
