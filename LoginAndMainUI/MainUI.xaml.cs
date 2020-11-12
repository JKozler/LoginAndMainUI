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

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        public MainUI()
        {
            InitializeComponent();
        }

        private void gloraHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void settingHome_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void startWorkTime_Click(object sender, RoutedEventArgs e)
        {
            startTime.Visibility = Visibility.Hidden;
            stopTime.Visibility = Visibility.Visible;
        }

        private void stopWorkingTime_Click(object sender, RoutedEventArgs e)
        {
            stopTime.Visibility = Visibility.Hidden;
            startTime.Visibility = Visibility.Visible;
        }
    }
}
