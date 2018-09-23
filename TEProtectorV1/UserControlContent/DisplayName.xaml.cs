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

namespace TEProtectorV1.UserControlContent
{
    /// <summary>
    /// Interaction logic for DisplayName.xaml
    /// </summary>
    public partial class DisplayName : UserControl
    {
        public DisplayName()
        {
            InitializeComponent();
            idaccount.Content = Properties.Settings.Default.idaccess;
        }
    }
}
