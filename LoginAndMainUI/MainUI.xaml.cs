﻿using System;
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
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        DateTime timeStart = new DateTime();
        DateTime timeStop = new DateTime();
        TimeSpan totalTime = new TimeSpan();
        int notificationCounter = 0;
        byte usingWorkingApps = 0;
        bool menuGloraIsEnabled = false;
        bool infoGloraIsEnable = false;
        bool taskBarIsShowen;
        bool gloraTextIsShowen = false;
        bool gWebIsShowen = false;
        bool infomrationCenterIsShowen = false;
        bool createNewTaskIsShowen = false;
        bool settingsGloraIsShowen = false;
        public MainUI()
        {
            InitializeComponent();
            taskCreateStats.Visibility = Visibility.Hidden;
            taskBarWholeInfo.Opacity = 0;
            taskCreateStats.Opacity = 0;
            selectedWork.Items.Add("Your own work.");
            selectedWork.SelectedItem = "Your own work.";
            taskBarIsShowen = false;
            doneTaskLb.IsEnabled = false;
            progressTaskLb.IsEnabled = false;
            createTaskLb.IsEnabled = false;
            failedTaskLb.IsEnabled = false;
            CheckIfWorkHasMoreThenOneStations();
            CheckForExistingWorkingApps();
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
            if (!settingsGloraIsShowen)
            {
                CloseEverythingAndShowThatWin("settings");
                GloraTextAssistentProp.Visibility = Visibility.Hidden;
                SettingsGlora.Visibility = Visibility.Visible;
                DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
                SettingsGlora.BeginAnimation(OpacityProperty, blurEnable);
                SettingsGlora.IsEnabled = true;
                settingsGloraIsShowen = true;
            }
            else
            {
                DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
                SettingsGlora.BeginAnimation(OpacityProperty, blurEnable);
                SettingsGlora.IsEnabled = false;
                settingsGloraIsShowen = false;
            }
        }

        private void startWorkTime_Click(object sender, RoutedEventArgs e)
        {
            startTime.Visibility = Visibility.Hidden;
            stopTime.Visibility = Visibility.Visible;
            timeStart = DateTime.Now;
            DoubleAnimation widthProp = new DoubleAnimation(0, 10, new Duration(TimeSpan.FromSeconds(0.2)));
            DoubleAnimation heightProp = new DoubleAnimation(0, 10, new Duration(TimeSpan.FromSeconds(0.2)));
            elRec.BeginAnimation(WidthProperty, widthProp);
            elRec.BeginAnimation(HeightProperty, heightProp);
        }

        private void stopWorkingTime_Click(object sender, RoutedEventArgs e)
        {
            stopTime.Visibility = Visibility.Hidden;
            startTime.Visibility = Visibility.Visible;
            timeStop = DateTime.Now;
            totalTime = (timeStop - timeStart) + totalTime;
            countOfTime.Content = totalTime.Hours + ":" + totalTime.Minutes + ":" + totalTime.Seconds;
            DoubleAnimation widthProp = new DoubleAnimation(10, 0, new Duration(TimeSpan.FromSeconds(0.2)));
            DoubleAnimation heightProp = new DoubleAnimation(10, 0, new Duration(TimeSpan.FromSeconds(0.2)));
            elRec.BeginAnimation(WidthProperty, widthProp);
            elRec.BeginAnimation(HeightProperty, heightProp);
        }

        private void informationBar_Click(object sender, RoutedEventArgs e)
        {
            if (!infomrationCenterIsShowen)
            {
                DoubleAnimation widthProp = new DoubleAnimation(0, 250, new Duration(TimeSpan.FromSeconds(0.2)));
                informationCenterBar.BeginAnimation(WidthProperty, widthProp);
                infomrationCenterIsShowen = true;
            }
            else
            {
                DoubleAnimation widthProp = new DoubleAnimation(250, 0, new Duration(TimeSpan.FromSeconds(0.2)));
                informationCenterBar.BeginAnimation(WidthProperty, widthProp);
                infomrationCenterIsShowen = false;
            }
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
            if (!infoGloraIsEnable)
            {
                DoubleAnimation blurEnable = new DoubleAnimation(0, 0.9, new Duration(TimeSpan.FromSeconds(0.5)));
                informationProperities.BeginAnimation(OpacityProperty, blurEnable);
                infoGloraIsEnable = true;
            }
            else
            {
                DoubleAnimation blurEnable = new DoubleAnimation(0.9, 0, new Duration(TimeSpan.FromSeconds(0.5)));
                informationProperities.BeginAnimation(OpacityProperty, blurEnable);
                infoGloraIsEnable = false;
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.G && Keyboard.Modifiers == ModifierKeys.Control)
                gloraHome_Click(sender, e);
            else if (e.Key == Key.I && Keyboard.Modifiers == ModifierKeys.Control)
                closeNoti_Click(sender, e);
            else if (e.Key == Key.T && Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (!taskBarIsShowen)
                    showTaskBar_Click(sender, e);
                else if (taskBarIsShowen)
                    minimzeTask_Click(sender, e);
            }
            else if (e.Key == Key.Escape)
            {
                if (infoGloraIsEnable)
                    closeNoti_Click(sender, e);
                if (menuGloraIsEnabled)
                    gloraHome_Click(sender, e);
                if (taskBarIsShowen)
                    minimzeTask_Click(sender, e);
                if (createNewTaskIsShowen)
                    createTaskLb_Click(sender, e);
                if (infomrationCenterIsShowen)
                    informationBar_Click(sender, e);
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
            if (!createNewTaskIsShowen)
            {
                taskCreateStats.Visibility = Visibility.Visible;
                DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
                taskCreateStats.BeginAnimation(OpacityProperty, blurEnable);
                taskCreateStats.IsEnabled = true;
                createNewTaskIsShowen = true;
            }
            else
            {
                DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
                taskCreateStats.BeginAnimation(OpacityProperty, blurEnable);
                taskCreateStats.IsEnabled = false;
                createNewTaskIsShowen = false;
                taskCreateStats.Visibility = Visibility.Hidden;
            }
        }

        private void failedTaskLb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void minimzeTask_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
            taskBarWholeInfo.BeginAnimation(OpacityProperty, blurEnable);
            taskBarIsShowen = false;
            doneTaskLb.IsEnabled = false;
            progressTaskLb.IsEnabled = false;
            createTaskLb.IsEnabled = false;
            failedTaskLb.IsEnabled = false;
            showTaskBar.Visibility = Visibility.Visible;
        }

        private void showTaskBar_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
            taskBarWholeInfo.BeginAnimation(OpacityProperty, blurEnable);
            taskBarIsShowen = true;
            doneTaskLb.IsEnabled = true;
            progressTaskLb.IsEnabled = true;
            createTaskLb.IsEnabled = true;
            failedTaskLb.IsEnabled = true;
            showTaskBar.Visibility = Visibility.Hidden;
        }

        private void gWeb_Click(object sender, RoutedEventArgs e)
        {
            if (!gWebIsShowen)
            {
                gWebIsShowen = true;
                btnMainPanel1.IsEnabled = true;
                DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.8)));
                btnMainPanel1.BeginAnimation(OpacityProperty, blurEnable);
                gloraHome_Click(sender, e);
            }
        }

        private void gReminder_Click(object sender, RoutedEventArgs e)
        {

        }

        private void gloraTextAsistent_Click(object sender, RoutedEventArgs e)
        {
            if (!gloraTextIsShowen)
            {
                CloseEverythingAndShowThatWin("gloraText");
                SettingsGlora.Visibility = Visibility.Hidden;
                GloraTextAssistentProp.Visibility = Visibility.Visible;
                DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
                GloraTextAssistentProp.BeginAnimation(OpacityProperty, blurEnable);
                GloraTextAssistentProp.IsEnabled = true;
                gloraTextIsShowen = true;
            }
            else
            {
                DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
                GloraTextAssistentProp.BeginAnimation(OpacityProperty, blurEnable);
                GloraTextAssistentProp.IsEnabled = false;
                gloraTextIsShowen = false;
            }
        }

        private void createNewTask_Click(object sender, RoutedEventArgs e)
        {

        }

        public void CheckIfWorkHasMoreThenOneStations()
        {
            if (selectedWork.Items.Count == 1 || selectedWork.Items.Count == 0)
            {
                selectedWork.IsEnabled = false;
                applyOrg.IsEnabled = false;
            }
        }

        private void applyOrg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void connectToOrg_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createNewProject_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addWorkingApp_Click(object sender, RoutedEventArgs e)
        {
            if (usingWorkingApps <= 11)
            {
                OpenFileDialog opf = new OpenFileDialog();
                if (opf.ShowDialog() == true)
                {
                    switch (++usingWorkingApps)
                    {
                        case 1:
                            appWork1.Visibility = Visibility.Visible;
                            appWork1.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork1.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork1.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 2:
                            appWork2.Visibility = Visibility.Visible;
                            appWork2.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork2.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork2.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 3:
                            appWork3.Visibility = Visibility.Visible;
                            appWork3.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork3.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork3.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 4:
                            appWork4.Visibility = Visibility.Visible;
                            appWork4.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork4.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork4.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 5:
                            appWork5.Visibility = Visibility.Visible;
                            appWork5.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork5.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork5.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 6:
                            appWork6.Visibility = Visibility.Visible;
                            appWork6.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork6.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork6.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 7:
                            appWork7.Visibility = Visibility.Visible;
                            appWork7.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork7.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork7.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 8:
                            appWork8.Visibility = Visibility.Visible;
                            appWork8.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork8.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork8.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 9:
                            appWork9.Visibility = Visibility.Visible;
                            appWork9.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork9.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork9.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 10:
                            appWork10.Visibility = Visibility.Visible;
                            appWork10.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork10.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork10.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 11:
                            appWork11.Visibility = Visibility.Visible;
                            appWork11.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork11.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork11.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        case 12:
                            appWork12.Visibility = Visibility.Visible;
                            appWork12.Content = System.IO.Path.GetFileName(opf.FileName);
                            appWork12.ToolTip = System.IO.Path.GetFileName(opf.FileName);
                            WriteAboutWorkApps("appWork12.pos", System.IO.Path.GetFileName(opf.FileName), opf.FileName);
                            break;
                        default:
                            MessageBox.Show("Error during loading working apps", "Error Working app", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
            }
            else
                MessageBox.Show("You allready use max. of apps. Please upgrade.");

            DoubleAnimation widthProp = new DoubleAnimation(0, 248, new Duration(TimeSpan.FromSeconds(0.2)));
            menuGlora.BeginAnimation(WidthProperty, widthProp);
            menuGloraIsEnabled = true;
        }

        private void appWorks_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            using (StreamReader sr = new StreamReader(btn.Name + ".pos"))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i == 1)
                        Process.Start(line);
                    i++;
                }
            }
        }

        private void btnMainPanel1_Click_1(object sender, RoutedEventArgs e)
        {
            gWeb_Click(sender, e);
        }

        public void CheckForExistingWorkingApps()
        {
            string baseName = "appWork";
            for (int i = 1; i <= 12; i++)
            {
                if (File.Exists(baseName + i + ".pos"))
                {
                    switch (i)
                    {
                        case 1:
                            appWork1.Visibility = Visibility.Visible;
                            usingWorkingApps = 1;
                            ReadAboutWorkApps("appWork1.pos", appWork1);
                            break;
                        case 2:
                            appWork2.Visibility = Visibility.Visible;
                            usingWorkingApps = 2;
                            ReadAboutWorkApps("appWork2.pos", appWork2);
                            break;
                        case 3:
                            appWork3.Visibility = Visibility.Visible;
                            usingWorkingApps = 3;
                            ReadAboutWorkApps("appWork3.pos", appWork3);
                            break;
                        case 4:
                            appWork4.Visibility = Visibility.Visible;
                            usingWorkingApps = 4;
                            ReadAboutWorkApps("appWork4.pos", appWork4);
                            break;
                        case 5:
                            appWork5.Visibility = Visibility.Visible;
                            usingWorkingApps = 5;
                            ReadAboutWorkApps("appWork5.pos", appWork5);
                            break;
                        case 6:
                            appWork6.Visibility = Visibility.Visible;
                            usingWorkingApps = 6;
                            ReadAboutWorkApps("appWork6.pos", appWork6);
                            break;
                        case 7:
                            appWork7.Visibility = Visibility.Visible;
                            usingWorkingApps = 7;
                            ReadAboutWorkApps("appWork7.pos", appWork7);
                            break;
                        case 8:
                            appWork8.Visibility = Visibility.Visible;
                            usingWorkingApps = 8;
                            ReadAboutWorkApps("appWork8.pos", appWork8);
                            break;
                        case 9:
                            appWork9.Visibility = Visibility.Visible;
                            usingWorkingApps = 9;
                            ReadAboutWorkApps("appWork9.pos", appWork8);
                            break;
                        case 10:
                            appWork10.Visibility = Visibility.Visible;
                            usingWorkingApps = 10;
                            ReadAboutWorkApps("appWork10.pos", appWork10);
                            break;
                        case 11:
                            appWork11.Visibility = Visibility.Visible;
                            usingWorkingApps = 11;
                            ReadAboutWorkApps("appWork11.pos", appWork11);
                            break;
                        case 12:
                            appWork12.Visibility = Visibility.Visible;
                            usingWorkingApps = 12;
                            ReadAboutWorkApps("appWork12.pos", appWork12);
                            break;
                        default:
                            MessageBox.Show("Error during loading working apps", "Error Working app", MessageBoxButton.OK, MessageBoxImage.Error);
                            break;
                    }
                }
                else { }
            }
        }
        public void WriteAboutWorkApps(string file, string title, string app)
        {
            Stream stream = new FileStream(file, FileMode.Create);
            using (StreamWriter sw = new StreamWriter(stream))
            {
                sw.WriteLine(title);
                sw.WriteLine(app);
            }
        }
        public void ReadAboutWorkApps(string file, Button btn)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i == 0) {
                        btn.Content = line;
                        btn.ToolTip = line;
                    }
                    i++;
                }
            }
        }
        public void CloseEverythingAndShowThatWin(string what)
        {
            if (what == "settings" && gloraTextIsShowen)
            {
                taskCreateStats.Visibility = Visibility.Hidden;
                createNewTaskIsShowen = false;
                taskCreateStats.IsEnabled = false;
                DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
                GloraTextAssistentProp.BeginAnimation(OpacityProperty, blurEnable);
                GloraTextAssistentProp.IsEnabled = false;
                gloraTextIsShowen = false;
            }
            else if (what == "gloraText" && settingsGloraIsShowen)
            {
                taskCreateStats.Visibility = Visibility.Hidden;
                createNewTaskIsShowen = false;
                taskCreateStats.IsEnabled = false;
                DoubleAnimation blurEnable = new DoubleAnimation(1, 0, new Duration(TimeSpan.FromSeconds(0.3)));
                SettingsGlora.BeginAnimation(OpacityProperty, blurEnable);
                SettingsGlora.IsEnabled = false;
                settingsGloraIsShowen = false;
            }
        }

        public void HideDisabledItem()
        {
            switch (usingWorkingApps)
            {
                case 1:
                    appWork1.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    appWork2.Visibility = Visibility.Hidden;
                    break;
                case 3:
                    appWork3.Visibility = Visibility.Hidden;
                    break;
                case 4:
                    appWork4.Visibility = Visibility.Hidden;
                    break;
                case 5:
                    appWork5.Visibility = Visibility.Hidden;
                    break;
                case 6:
                    appWork6.Visibility = Visibility.Hidden;
                    break;
                case 7:
                    appWork7.Visibility = Visibility.Hidden;
                    break;
                case 8:
                    appWork8.Visibility = Visibility.Hidden;
                    break;
                case 9:
                    appWork9.Visibility = Visibility.Hidden;
                    break;
                case 10:
                    appWork10.Visibility = Visibility.Hidden;
                    break;
                case 11:
                    appWork11.Visibility = Visibility.Hidden;
                    break;
                case 12:
                    appWork12.Visibility = Visibility.Hidden;
                    break;
                default:
                    MessageBox.Show("Error during loading working apps", "Error Working app", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void appWork1_KeyDown(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show("Are you sure to delete this app from pOS?", "Warning", MessageBoxButton.YesNo);
            if (mr == MessageBoxResult.Yes)
            {
                Button btn = (Button)sender;
                int index = Convert.ToInt32(btn.Name.ToString().Replace("appWork", ""));
                if (usingWorkingApps > index)
                {
                    for (int i = index; i < usingWorkingApps; i++)
                    {
                        Stream stream = new FileStream("appWork" + i + ".pos", FileMode.Create);
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            using (StreamReader sr = new StreamReader("appWork" + (i + 1).ToString() + ".pos"))
                            {
                                string line;
                                int y = 0;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    if (y == 0)
                                        sw.WriteLine(line);
                                    else
                                        sw.WriteLine(line);
                                    y++;
                                }
                            }
                        }
                    }
                    HideDisabledItem();
                    File.Delete("appWork" + usingWorkingApps + ".pos");
                    CheckForExistingWorkingApps();
                    Console.WriteLine(usingWorkingApps);
                }
                else
                {
                    usingWorkingApps--;
                    btn.Visibility = Visibility.Hidden;
                    File.Delete(btn.Name.ToString() + ".pos");
                }
            }
        }
    }
}
