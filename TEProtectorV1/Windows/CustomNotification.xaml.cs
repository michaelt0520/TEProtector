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
using System.Windows.Threading;

namespace TEProtectorV1.Windows
{
    /// <summary>
    /// Interaction logic for CustomNotification.xaml
    /// </summary>
    public partial class CustomNotification : Window
    {
        DispatcherTimer dispatcherTimer;
        public CustomNotification()
        {
            InitializeComponent();
            dispatcherTimer = new DispatcherTimer() { Interval = new TimeSpan(0, 0, 0, 0, 3000) };
            dispatcherTimer.Tick += CloseNotification;
            dispatcherTimer.Start();
        }
        private void CloseNotification (object sender, EventArgs e)
        {
            Close();
        }
    }
}
