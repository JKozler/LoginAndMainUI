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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        int notificationCounter = 0;
        bool menuGloraIsEnabled = false;
        bool infoGloraIsEnable = false;
        bool taskBarIsShowen = true;
        public MainUI()
        {
            InitializeComponent();
        }

        private void gloraHome_Click(object sender, RoutedEventArgs e)
        {
            if (menuGloraIsEnabled)
            {
                DoubleAnimation widthProp = new DoubleAnimation(248, 0, new Duration(TimeSpan.FromSeconds(0.2)));
                menuGloraIsEnabled = false;
                menuGlora.BeginAnimation(WidthProperty, widthProp);
            }
            else
            {
                DoubleAnimation widthProp = new DoubleAnimation(0, 248, new Duration(TimeSpan.FromSeconds(0.2)));
                menuGlora.BeginAnimation(WidthProperty, widthProp);
                menuGloraIsEnabled = true;
            }
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

        private void informationBar_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation blurEnable = new DoubleAnimation(0, 0.9, new Duration(TimeSpan.FromSeconds(0.5)));
            informationProperities.BeginAnimation(OpacityProperty, blurEnable);
            infoGloraIsEnable = true;
        }

        private void informationBarZero_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = new SolidColorBrush(Colors.Gray);
        }

        private void settingHome_MouseLeave(object sender, MouseEventArgs e)
        {
            Button btn = (Button)sender;
            btn.Background = mainElementPanel.Background;
        }

        private void closeNoti_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation blurEnable = new DoubleAnimation(0.9, 0, new Duration(TimeSpan.FromSeconds(0.5)));
            informationProperities.BeginAnimation(OpacityProperty, blurEnable);
            infoGloraIsEnable = false;
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.G)
                gloraHome_Click(sender, e);
            else if (e.Key == Key.I)
                informationBar_Click(sender, e);
            else if (e.Key == Key.Escape)
            {
                if (infoGloraIsEnable)
                    closeNoti_Click(sender, e);
                if (menuGloraIsEnabled)
                    gloraHome_Click(sender, e);
            }
        }

        private void moreInfoTask_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void moreInfoTask_MouseEnter(object sender, MouseEventArgs e)
        {
            moreInfoTask.Cursor = Cursors.Hand;
            moreInfoTask.Foreground = new SolidColorBrush(Color.FromRgb(21,67,96));
        }

        private void moreInfoTask_MouseLeave(object sender, MouseEventArgs e)
        {
            moreInfoTask.Cursor = Cursors.Arrow;
            moreInfoTask.Foreground = new SolidColorBrush(Color.FromRgb(46, 134, 193));
        }

        private void doneTaskLb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void progressTaskLb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createTaskLb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void failedTaskLb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minimzeTask_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
            taskBarWholeInfo.BeginAnimation(OpacityProperty, blurEnable);
            taskBarIsShowen = false;
            showTaskBar.Visibility = Visibility.Visible;
        }

        private void showTaskBar_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            taskBarWholeInfo.BeginAnimation(OpacityProperty, blurEnable);
            taskBarIsShowen = true;
            showTaskBar.Visibility = Visibility.Hidden;
        }
    }
}
