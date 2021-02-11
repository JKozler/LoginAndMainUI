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
using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window, INotifyPropertyChanged
    {
        DateTime timeStart = new DateTime();
        DateTime timeStop = new DateTime();
        TimeSpan totalTime = new TimeSpan();
        int notificationCounter = 0;
        int numberOfTask = 0;
        int numberOfTaskDone = 0;
        int numberOfTaskFailed = 0;
        int numberOfTaskProgress = 0;
        byte usingWorkingApps = 0;
        bool menuGloraIsEnabled = false;
        bool infoGloraIsEnable = false;
        bool firstRun = true;
        bool taskBarIsShowen;
        bool gloraTextIsShowen = false;
        bool gWebIsShowen = false;
        bool infomrationCenterIsShowen = false;
        bool createNewTaskIsShowen = false;
        bool settingsGloraIsShowen = false;
        public JObject user = new JObject();
        public JObject team = new JObject();
        public JObject task = new JObject();
        public JObject taskUpdate = new JObject();
        public JObject admin = new JObject();
        public JObject teamUsers = new JObject();
        public JObject mess = new JObject();
        private string taskN;

        #region properities
        public string TaskName
        {
            get { return taskN; }
            set { taskN = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskName")); }
        }

        private string taskP;

        public string TaskProperty
        {
            get { return taskP; }
            set { taskP = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskProperty")); }
        }
        private string taskE;

        public string TaskElse
        {
            get { return taskE; }
            set { taskE = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskElse")); }
        }

        private string connectORG;

        public string ConnectORG
        {
            get { return connectORG; }
            set { connectORG = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnectORG")); }
        }

        private string allUserCount;

        public string AllUserCount
        {
            get { return allUserCount; }
            set { allUserCount = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllUserCount")); }
        }

        private bool connectToORgIsEnabled;

        public bool ConnectToORgIsEnabled
        {
            get { return connectToORgIsEnabled; }
            set { connectToORgIsEnabled = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ConnectToORgIsEnabled")); }
        }

        private bool cbConnectToORgIsEnabled;

        public bool CBConnectToORgIsEnabled
        {
            get { return cbConnectToORgIsEnabled; }
            set { cbConnectToORgIsEnabled = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CBConnectToORgIsEnabled")); }
        }

        private string cbSelectedOrg;

        public string CBSelectedOrg
        {
            get { return cbSelectedOrg; }
            set { cbSelectedOrg = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CBSelectedOrg")); }
        }

        private List<string> arrayOfItems;

        public List<string> ArrayOfItems
        {
            get { return arrayOfItems; }
            set { arrayOfItems = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ArrayOfItems")); }
        }

        private ObservableCollection<string> lbNewTask;

        public ObservableCollection<string> LBNewTask
        {
            get { return lbNewTask; }
            set { lbNewTask = value;  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LBNewTask")); }
        }

        private ObservableCollection<string> lbProgressTask;

        public ObservableCollection<string> LBProgressTask
        {
            get { return lbProgressTask; }
            set { lbProgressTask = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LBProgressTask")); }
        }

        private ObservableCollection<string> infoItems;

        public ObservableCollection<string> InfoItems
        {
            get { return infoItems; }
            set { infoItems = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InfoItems")); }
        }

        private ObservableCollection<string> allMessage;

        public ObservableCollection<string> AllMessage
        {
            get { return allMessage; }
            set { allMessage = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllMessage")); }
        }

        private int numberFailed;

        public int NumberFailed
        {
            get { return numberFailed; }
            set { numberFailed = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberFailed")); }
        }

        private int numberProgress;

        public int NumberProgress
        {
            get { return numberProgress; }
            set { numberProgress = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberProgress")); }
        }

        private string numberEnable;

        public string NumberEnable
        {
            get { return numberEnable; }
            set { numberEnable = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("NumberEnable")); }
        }

        private string toolTipInfo;

        public string ToolTipInfo
        {
            get { return toolTipInfo; }
            set { toolTipInfo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ToolTipInfo")); }
        }

        private bool enableTeam;

        public bool EnableTeam
        {
            get { return enableTeam; }
            set { enableTeam = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnableTeam")); }
        }

        #endregion

        Timer timer;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainUI()
        {
        }
        public MainUI(JObject jo)
        {
            InitializeComponent();
            ArrayOfItems = new List<string>();
            InfoItems = new ObservableCollection<string>();
            AllMessage = new ObservableCollection<string>();
            user = jo;
            AllUserCount = "0";
            ToolTipInfo = "You have 0 notification.";
            NumberFailed = 0;
            NumberEnable = "0/0";
            NumberProgress = 0;
            taskCreateStats.Visibility = Visibility.Hidden;
            taskBarWholeInfo.Opacity = 0;
            taskCreateStats.Opacity = 0;
            ArrayOfItems.Add("Your own work.");
            CBSelectedOrg = "Your own work.";
            taskBarIsShowen = false;
            doneTaskLb.IsEnabled = false;
            progressTaskLb.IsEnabled = false;
            createTaskLb.IsEnabled = false;
            failedTaskLb.IsEnabled = false;
            LoadInfoHourSpend(Convert.ToInt32(jo["user"]["time"]));
            CheckInformationsAboutUser();
            CheckIfWorkHasMoreThenOneStations();
            CheckForExistingWorkingApps();
            DataContext = this;
            timer = new Timer(e => { UpdateTask(); }, null, 30000, 60000);
        }

        public async void UpdateTask()
        {
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-task-detail/" + user["user"]["id"];
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                taskUpdate = jo;
                JArray arrayUpdate1 = (JArray)taskUpdate["task"];
                JArray arrayUpdate2 = (JArray)task["task"];
                if (arrayUpdate1.Count != arrayUpdate2.Count) 
                {
                    if (arrayUpdate1.Count >= arrayUpdate2.Count)
                    {
                        TaskName = "Task create";
                        TaskProperty = taskUpdate["task"][arrayUpdate1.Count - 1]["name"].ToString();
                        TaskElse = "State - " + taskUpdate["task"][arrayUpdate1.Count - 1]["state"].ToString();
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            InfoItems.Add("Task - created (" + taskUpdate["task"][arrayUpdate1.Count - 1]["name"].ToString() + ")");
                        });
                    }
                    else
                    {
                        TaskName = "Task delete";
                        TaskProperty = task["task"][arrayUpdate2.Count - 1]["name"].ToString();
                        TaskElse = "State - " + task["task"][arrayUpdate2.Count - 1]["state"].ToString() + ", was deleted.";
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            InfoItems.Add("Task - deleted (" + taskUpdate["task"][arrayUpdate1.Count - 1]["name"].ToString() + ")");
                        });
                    }
                    App.Current.Dispatcher.Invoke((System.Action)delegate
                    {
                        InformationCenter();
                    });
                    notificationCounter++;
                    ToolTipInfo = "You have " + notificationCounter + " notification";
                }
                else
                {
                    for (int i = 0; i < arrayUpdate2.Count; i++)
                    {
                        if (task["task"][i] != taskUpdate["task"][i])
                        {
                            TaskName = "Task update";
                            TaskProperty = taskUpdate["task"][arrayUpdate1.Count - 1]["name"].ToString();
                            TaskElse = "State - " + taskUpdate["task"][arrayUpdate1.Count - 1]["state"].ToString();
                            App.Current.Dispatcher.Invoke((System.Action)delegate
                            {
                                InfoItems.Add("Task - update (" + taskUpdate["task"][arrayUpdate1.Count - 1]["name"].ToString() + ")");
                            });
                        }
                    }
                }
                PropertyChanged.Invoke(CheckInformationsAboutUser(), new PropertyChangedEventArgs("Check info."));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
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

        public async Task UpdateUser(string name, string email, string password, int team, int time, string role)
        {
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/put-user/{\"name\":\"" + name + "\",\"email\":\"" + email + "\",\"password\":\"" + password + "\",\"team\":\"" + team + "\",\"time\":\"" + time + "\",\"role\":\"" + role + "\"}";
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res2 = await response.Content.ReadAsStringAsync();
                JObject jo2 = JObject.Parse(res2);
                await CheckInformationsAboutUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        public async Task UpdateTask(string name, string description, DateTime dateFrom, DateTime dateTo, int userId, string state, int id)
        {
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/put-task/{\"name\":\"" + name + "\",\"description\":\"" + description + "\",\"userId\":\"" + userId + "\",\"dateFrom\":\"" + dateFrom.ToString("yyyy-MM-dd") + "\",\"dateTo\":\"" + dateTo.ToString("yyyy-MM-dd") + "\",\"state\":\"" + state + "\",\"id\":\"" + id + "\"}";
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res2 = await response.Content.ReadAsStringAsync();
                JObject jo2 = JObject.Parse(res2);
                await CheckInformationsAboutUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
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

        private async void stopWorkingTime_Click(object sender, RoutedEventArgs e)
        {
            stopTime.Visibility = Visibility.Hidden;
            startTime.Visibility = Visibility.Visible;
            timeStop = DateTime.Now;
            int hourBefore = Convert.ToInt32(countOfTime.Content.ToString().Remove(countOfTime.Content.ToString().IndexOf(":"), countOfTime.Content.ToString().Length - 1));
            int minBefore = Convert.ToInt32(countOfTime.Content.ToString().Remove(0, countOfTime.Content.ToString().IndexOf(":") + 1));
            totalTime = (timeStop - timeStart) + totalTime;
            if (totalTime.Seconds > 30)
            {
                if (totalTime.Minutes + 1 + minBefore >= 60 && (totalTime.Minutes + minBefore - 60).ToString().Length <= 1)
                    countOfTime.Content = (totalTime.Hours + hourBefore + 1) + ":0" + (totalTime.Minutes + minBefore - 60);
                else if (totalTime.Minutes + 1 + minBefore >= 60 && (totalTime.Minutes + minBefore).ToString().Length <= 1)
                    countOfTime.Content = (totalTime.Hours + hourBefore) + ":0" + (totalTime.Minutes + minBefore);
                else if (totalTime.Minutes + 1 + minBefore >= 60)
                    countOfTime.Content = (totalTime.Hours + hourBefore + 1) + ":" + (totalTime.Minutes + 1 + minBefore - 60);
                else
                    countOfTime.Content = (totalTime.Hours + hourBefore) + ":" + (totalTime.Minutes + 1 + minBefore);
            }
            else
            {
                if (totalTime.Minutes + minBefore >= 60 && (totalTime.Minutes + minBefore - 60).ToString().Length <= 1)
                    countOfTime.Content = (totalTime.Hours + hourBefore + 1) + ":0" + (totalTime.Minutes + minBefore - 60);
                else if (totalTime.Minutes + minBefore >= 60 && (totalTime.Minutes + minBefore).ToString().Length <= 1)
                    countOfTime.Content = (totalTime.Hours + hourBefore) + ":0" + (totalTime.Minutes + minBefore);
                else if (totalTime.Minutes + minBefore >= 60)
                    countOfTime.Content = (totalTime.Hours + hourBefore + 1) + ":" + (totalTime.Minutes + minBefore - 60);
                else
                    countOfTime.Content = (totalTime.Hours + hourBefore) + ":" + (totalTime.Minutes + minBefore);
            }
            int totalTimeForAPI = 60 * Convert.ToInt32(countOfTime.Content.ToString().Remove(countOfTime.Content.ToString().IndexOf(":"), countOfTime.Content.ToString().Length - 1)) + Convert.ToInt32(countOfTime.Content.ToString().Remove(0, countOfTime.Content.ToString().IndexOf(":") + 1));
            await UpdateUser(user["user"]["name"].ToString(), user["user"]["email"].ToString(), user["user"]["password"].ToString(), Convert.ToInt32(user["user"]["team"]), totalTimeForAPI, user["user"]["email"].ToString());
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

        public void InformationCenter() 
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

        private void closeNoti_Click(object sender, RoutedEventArgs e)
        {
            infoIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.InfoCircle;
            InformationCenter();
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

        private async void moreInfoTask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTasks allTasks = new AllTasks(task, Convert.ToInt32(user["user"]["id"]), true, Convert.ToInt32(user["user"]["team"]));
            allTasks.ShowDialog();
            await CheckInformationsAboutUser();
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

        private async void doneTaskLb_Click(object sender, RoutedEventArgs e)
        {
            if (lbTask.SelectedItem != null)
            {
                string item = lbTask.SelectedItem.ToString();
                JArray array = (JArray)task["task"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (task["task"][i]["name"].ToString() == item)
                    {
                        await UpdateTask(task["task"][i]["name"].ToString(), task["task"][i]["description"].ToString(), Convert.ToDateTime(task["task"][i]["dateFrom"]), Convert.ToDateTime(task["task"][i]["dateTo"]), Convert.ToInt32(task["task"][i]["userId"]), "Done", Convert.ToInt32(task["task"][i]["id"]));
                    }
                }
            }
            else if (lbTaskProgress.SelectedItem != null)
            {
                string item = lbTaskProgress.SelectedItem.ToString();
                JArray array = (JArray)task["task"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (task["task"][i]["name"].ToString() == item)
                    {
                        await UpdateTask(task["task"][i]["name"].ToString(), task["task"][i]["description"].ToString(), Convert.ToDateTime(task["task"][i]["dateFrom"]), Convert.ToDateTime(task["task"][i]["dateTo"]), Convert.ToInt32(task["task"][i]["userId"]), "Done", Convert.ToInt32(task["task"][i]["id"]));
                    }
                }
            }
            await CheckInformationsAboutUser();
        }

        private async void progressTaskLb_Click(object sender, RoutedEventArgs e)
        {
            if (lbTask.SelectedItem != null)
            {
                string item = lbTask.SelectedItem.ToString();
                JArray array = (JArray)task["task"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (task["task"][i]["name"].ToString() == item)
                    {
                        await UpdateTask(task["task"][i]["name"].ToString(), task["task"][i]["description"].ToString(), Convert.ToDateTime(task["task"][i]["dateFrom"]), Convert.ToDateTime(task["task"][i]["dateTo"]), Convert.ToInt32(task["task"][i]["userId"]), "Progress", Convert.ToInt32(task["task"][i]["id"]));
                    }
                }
            }
            else if (lbTaskProgress.SelectedItem != null)
            {
                string item = lbTaskProgress.SelectedItem.ToString();
                JArray array = (JArray)task["task"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (task["task"][i]["name"].ToString() == item)
                    {
                        await UpdateTask(task["task"][i]["name"].ToString(), task["task"][i]["description"].ToString(), Convert.ToDateTime(task["task"][i]["dateFrom"]), Convert.ToDateTime(task["task"][i]["dateTo"]), Convert.ToInt32(task["task"][i]["userId"]), "Done", Convert.ToInt32(task["task"][i]["id"]));
                    }
                }
            }
            await CheckInformationsAboutUser();
        }

        public async Task GetAllUsers(int id)
        {
            if (team == null)
            {
                userAssign.Items.Add("Me");
            }
            else
            {
                HttpClient http = new HttpClient();
                userAssign.Items.Clear();
                string url = "http://www.g-pos.8u.cz/api/get-all-users-from-team/" + id;
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                teamUsers = jo;
                JArray array = (JArray)jo["users"];
                for (int i = 0; i < array.Count; i++)
                {
                    userAssign.Items.Add(jo["users"][i]["name"]);
                }
            }
        }

        private async void createTaskLb_Click(object sender, RoutedEventArgs e)
        {
            if (!createNewTaskIsShowen)
            {
                taskCreateStats.Visibility = Visibility.Visible;
                DoubleAnimation blurEnable = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(0.3)));
                taskCreateStats.BeginAnimation(OpacityProperty, blurEnable);
                taskCreateStats.IsEnabled = true;
                createNewTaskIsShowen = true;
                try
                {
                    await GetAllUsers(Convert.ToInt32(user["user"]["team"]));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
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

        private async void failedTaskLb_Click(object sender, RoutedEventArgs e)
        {
            if (lbTask.SelectedItem != null)
            {
                string item = lbTask.SelectedItem.ToString();
                JArray array = (JArray)task["task"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (task["task"][i]["name"].ToString() == item)
                    {
                        await UpdateTask(task["task"][i]["name"].ToString(), task["task"][i]["description"].ToString(), Convert.ToDateTime(task["task"][i]["dateFrom"]), Convert.ToDateTime(task["task"][i]["dateTo"]), Convert.ToInt32(task["task"][i]["userId"]), "Failed", Convert.ToInt32(task["task"][i]["id"]));
                    }
                }
            }
            else if (lbTaskProgress.SelectedItem != null)
            {
                string item = lbTaskProgress.SelectedItem.ToString();
                JArray array = (JArray)task["task"];
                for (int i = 0; i < array.Count; i++)
                {
                    if (task["task"][i]["name"].ToString() == item)
                    {
                        await UpdateTask(task["task"][i]["name"].ToString(), task["task"][i]["description"].ToString(), Convert.ToDateTime(task["task"][i]["dateFrom"]), Convert.ToDateTime(task["task"][i]["dateTo"]), Convert.ToInt32(task["task"][i]["userId"]), "Done", Convert.ToInt32(task["task"][i]["id"]));
                    }
                }
            }
            await CheckInformationsAboutUser();
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
                GWebPlace.Visibility = Visibility.Visible;
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

        public async Task AssignTaskToUser()
        {
            HttpClient http = new HttpClient();
            string urlUserId = "http://www.g-pos.8u.cz/api/user-id-by-name/" + userAssign.SelectedItem.ToString();
            HttpResponseMessage responseUser = await http.GetAsync(urlUserId, HttpCompletionOption.ResponseContentRead);
            string res = await responseUser.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(res);
            if (team == null)
            {
                try
                {
                    string url = "http://www.g-pos.8u.cz/api/post-task/{\"teamCode\":\"" + user["user"]["id"].ToString() + "\",\"name\":\"" + taskName.Text + "\",\"description\":\"" + taskDescription.Text + "\",\"userId\":\"" + user["user"]["id"].ToString() + "\",\"dateFrom\":\"" + Convert.ToDateTime(taskDateFrom.SelectedDate).ToString("yyyy-MM-dd") + "\",\"dateTo\":\"" + Convert.ToDateTime(taskDateTo.SelectedDate).ToString("yyyy-MM-dd") + "\",\"state\":\"New\"}";
                    HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    string res2 = await response.Content.ReadAsStringAsync();
                    JObject jo2 = JObject.Parse(res2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                try
                {
                    string url = "http://www.g-pos.8u.cz/api/post-task/{\"teamCode\":\"" + team["code"].ToString() + "\",\"name\":\"" + taskName.Text + "\",\"description\":\"" + taskDescription.Text + "\",\"userId\":\"" + jo["id"].ToString() + "\",\"dateFrom\":\"" + Convert.ToDateTime(taskDateFrom.SelectedDate).ToString("yyyy-MM-dd") + "\",\"dateTo\":\"" + Convert.ToDateTime(taskDateTo.SelectedDate).ToString("yyyy-MM-dd") + "\",\"state\":\"New\"}";
                    HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    string res2 = await response.Content.ReadAsStringAsync();
                    JObject jo2 = JObject.Parse(res2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
        }

        private async void createNewTask_Click(object sender, RoutedEventArgs e)
        {
            if (taskDateFrom.SelectedDate > taskDateTo.SelectedDate)
                MessageBox.Show("Date from have to be smaller than date to.", "Error", MessageBoxButton.OK);
            else if (userAssign.SelectedItem.ToString() != null && taskName.Text != null && taskDescription.Text != null) 
            {
                await AssignTaskToUser();
                taskName.Text = "";
                userAssign.SelectedItem = "";
                taskDescription.Text = "";
                taskDateFrom.SelectedDate = null;
                taskDateTo.SelectedDate = null;
                MessageBox.Show("Task was successfully created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                await CheckInformationsAboutUser();
            }
            else
                MessageBox.Show("You must fill every box (instead of dates).", "Error", MessageBoxButton.OK);
        }

        public void CheckIfWorkHasMoreThenOneStations()
        {
            if (selectedWork.Items.Count == 1 || selectedWork.Items.Count == 0)
            {
                ConnectToORgIsEnabled = true;
                applyOrg.IsEnabled = true;
            }
            else
            {
                ConnectToORgIsEnabled = false;
                CBConnectToORgIsEnabled = true;
                connectToOrgBtn.IsEnabled = false;
            }
        }

        private void applyOrg_Click(object sender, RoutedEventArgs e)
        {

        }

        public async Task ConnectToTeam(string code)
        {
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-team-detail/" + code;
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                if (jo["name"] != null)
                {
                    string url2 = "http://www.g-pos.8u.cz/api/put-user/{\"name\":\"" + user["user"]["name"].ToString() + "\",\"email\":\"null\",\"teamId\":0,\"password\":\"" + user["user"]["password"].ToString() + "\",\"time\":" + user["user"]["time"].ToString() + ",\"email\":\"" + user["user"]["email"].ToString() + "\",\"team\":" + jo["id"].ToString() + "}";
                    HttpResponseMessage response2 = await http.GetAsync(url2, HttpCompletionOption.ResponseContentRead);
                    string res2 = await response2.Content.ReadAsStringAsync();
                    JObject jo2 = JObject.Parse(res2);
                    user["user"] = jo2;
                    MessageBox.Show("Successfully added to team.");
                }
                await CheckInformationsAboutUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private async void connectToOrg_Click(object sender, RoutedEventArgs e)
        {
            if (connectToOrg.Text != null)
            {
                await ConnectToTeam(connectToOrg.Text);
            }
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
        public void LoadInfoHourSpend(int time)
        {
            if (time >= 60)
            {
                int hours = time / 60;
                int minutes = time % 60;
                countOfTime.Content = hours + ":" + minutes;
            }
            else
                countOfTime.Content = "0:" + time;
        }

        private async void teamInfo_Click(object sender, RoutedEventArgs e)
        {
            await GetAllUsers(Convert.ToInt32(user["user"]["team"]));
            TeamViewer tv = new TeamViewer(admin, teamUsers, team, user);
            tv.ShowDialog();
        }

        public async Task CheckInformationsAboutUser()
        {
            var uiContext = SynchronizationContext.Current;
            HttpClient http = new HttpClient();
            if (Convert.ToInt32(user["user"]["team"]) != 0)
            {
                EnableTeam = true;
                try
                {
                    string url = "http://www.g-pos.8u.cz/api/get-team/" + user["user"]["team"];
                    HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    string res = await response.Content.ReadAsStringAsync();
                    JObject jo = JObject.Parse(res);
                    ConnectORG = jo["code"].ToString();
                    ConnectToORgIsEnabled = false;
                    CBSelectedOrg = jo["name"].ToString();
                    CBConnectToORgIsEnabled = true;
                    ArrayOfItems.Add(jo["name"].ToString());
                    team = jo;

                    string url2 = "http://www.g-pos.8u.cz/api/get-number-of-user/" + user["user"]["team"];
                    HttpResponseMessage response2 = await http.GetAsync(url2, HttpCompletionOption.ResponseContentRead);
                    string res2 = await response2.Content.ReadAsStringAsync();
                    JObject jo2 = JObject.Parse(res2);
                    if (AllUserCount != jo2["COUNT(*)"].ToString() && !firstRun)
                    {
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            TaskName = "New user added";
                            TaskProperty = "";
                            TaskElse = "";
                            InformationCenter();
                            InfoItems.Add("New user added");
                        });
                        notificationCounter++;
                        ToolTipInfo = "You have " + notificationCounter + " notification";
                    }
                    AllUserCount = jo2["COUNT(*)"].ToString();

                    string url3 = "http://www.g-pos.8u.cz/api/get-admin/" + user["user"]["id"];
                    HttpResponseMessage response3 = await http.GetAsync(url3, HttpCompletionOption.ResponseContentRead);
                    string res3 = await response3.Content.ReadAsStringAsync();
                    JObject jo3 = JObject.Parse(res3);
                    if (jo3["id"].ToString() != "no")
                    {
                        if (!firstRun && admin == jo3)
                        {
                            App.Current.Dispatcher.Invoke((System.Action)delegate
                            {
                                TaskName = "Admin set new values";
                                TaskProperty = "You was add to admin like " + jo3["description"].ToString();
                                TaskElse = "Team code " + jo3["teamCode"].ToString();
                                InformationCenter();
                                InfoItems.Add("New user added");
                            });
                            notificationCounter++;
                            ToolTipInfo = "You have " + notificationCounter + " notification";
                        }
                    }
                    admin = jo3;
                    string url4 = "http://www.g-pos.8u.cz/api/get-mess/" + user["user"]["id"];
                    HttpResponseMessage response4 = await http.GetAsync(url4, HttpCompletionOption.ResponseContentRead);
                    string res4 = await response4.Content.ReadAsStringAsync();
                    JObject jo4 = JObject.Parse(res4);
                    AllMessage = new ObservableCollection<string>();
                    JArray arrayMess = (JArray)jo4["mess"];
                    JArray arrayMessOld = (JArray)mess["mess"];
                    if (!firstRun && arrayMessOld.Count != arrayMess.Count)
                    {
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            TaskName = "New message from ";
                            TaskProperty = "Message " + jo4["mess"][arrayMess.Count - 1]["description"].ToString();
                            TaskElse = "Subject " + jo4["mess"][arrayMess.Count - 1]["name"].ToString();
                            InformationCenter();
                            InfoItems.Add("New direct massage");
                        });
                        notificationCounter++;
                        ToolTipInfo = "You have " + notificationCounter + " notification";
                    }
                    for (int i = 0; i < arrayMess.Count; i++)
                    {
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            AllMessage.Add(jo4["mess"][i]["name"].ToString());
                        });
                    }
                    mess = jo4;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
                }
            }
            else { team = null; EnableTeam = false; }
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-task-detail/" + user["user"]["id"];
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                task = jo;
                LBNewTask = new ObservableCollection<string>();
                LBProgressTask = new ObservableCollection<string>();
                JArray array = (JArray)jo["task"];
                numberOfTask = 0;
                numberOfTaskDone = 0;
                numberOfTaskFailed = 0;
                numberOfTaskProgress = 0;
                for (int i = 0; i < array.Count; i++)
                {
                    if (jo["task"][i]["state"].ToString() == "New") {
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            LBNewTask.Add(jo["task"][i]["name"].ToString());
                        });
                    }
                    if (jo["task"][i]["state"].ToString() == "Done")
                        numberOfTaskDone++;
                    if (jo["task"][i]["state"].ToString() == "Failed")
                        numberOfTaskFailed++;
                    if (jo["task"][i]["state"].ToString() == "Progress")
                    {
                        numberOfTaskProgress++;
                        App.Current.Dispatcher.Invoke((System.Action)delegate
                        {
                            LBProgressTask.Add(jo["task"][i]["name"].ToString());
                        });
                    }
                    numberOfTask++;
                }
                NumberProgress = numberOfTaskProgress;
                NumberFailed = numberOfTaskFailed;
                NumberEnable = numberOfTaskDone + "/" + numberOfTask;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            firstRun = false;
        }

        public async Task<string[]> ReturnAllMyTask()
        {
            string[] names;
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-task-detail/" + user["user"]["id"];
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                task = jo;
                JArray array = (JArray)jo["task"];
                names = new string[array.Count];
                for (int i = 0; i < array.Count; i++)
                {
                    names[i] = task["task"]["name"].ToString();
                }
                return names;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            return null;
        }

        private void closeGlora_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult mr = MessageBox.Show("Are you sure to shutdown pOS?", "Information", MessageBoxButton.YesNo);
            if (mr == MessageBoxResult.Yes)
                this.Close();
        }

        private async void lbTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbTask.SelectedItem != null)
            {
                TaskEdit taskEdit = new TaskEdit(lbTask.SelectedItem.ToString(), Convert.ToInt32(user["user"]["team"]));
                taskEdit.ShowDialog();
                await CheckInformationsAboutUser();
            }
            else if (lbTaskProgress.SelectedItem != null)
            {
                TaskEdit taskEdit = new TaskEdit(lbTaskProgress.SelectedItem.ToString(), Convert.ToInt32(user["user"]["team"]));
                taskEdit.ShowDialog();
                await CheckInformationsAboutUser();
            }
        }

        private void clearNoti_Click(object sender, RoutedEventArgs e)
        {
            infoIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.InfoCircleOutline;
            notificationCounter = 0;
            ToolTipInfo = "You have " + notificationCounter + " notification";
            InfoItems.Clear();
        }

        private void agreeWith_Click(object sender, RoutedEventArgs e)
        {

        }

        private void disagreeWith_Click(object sender, RoutedEventArgs e)
        {

        }

        private void allMess_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
