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

namespace TEProtectorV1.Windows
{
    /// <summary>
    /// Interaction logic for FormClose.xaml
    /// </summary>
    public partial class FormClose : Window
    {
        public bool isExited { get; set; }
        public FormClose()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            btnCancel_Click(sender, e);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            isExited = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
