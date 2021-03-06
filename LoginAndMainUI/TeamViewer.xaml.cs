using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for TeamViewer.xaml
    /// </summary>
    public partial class TeamViewer : Window, INotifyPropertyChanged
    {
        #region Properities 
        private string info;

        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        private string elseInfo;

        public string ElseInfo
        {
            get { return elseInfo; }
            set { elseInfo = value; }
        }

        private ObservableCollection<string> usersItems;

        public ObservableCollection<string> UsersItems
        {
            get { return usersItems; }
            set { usersItems = value; }
        }

        private string userSelect;

        public string UserSelect
        {
            get { return userSelect; }
            set { userSelect = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UserSelect")); }
        }

        private bool enableContent;

        public bool EnableContent
        {
            get { return enableContent; }
            set { enableContent = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("EnableContent")); }
        }

        private List<string> itemsCB;

        public List<string> ItemsCB
        {
            get { return itemsCB; }
            set { itemsCB = value; }
        }

        private string role;

        public string RoleTxt
        {
            get { return role; }
            set { role = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoleTxt")); }
        }

        private List<string> roleItems;

        public List<string> RoleItems
        {
            get { return roleItems; }
            set { roleItems = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RoleItems")); }
        }


        private string admin;

        public string Admin
        {
            get { return admin; }
            set { admin = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Admin")); }
        }

        private string taskStates;

        public string TaskStates
        {
            get { return taskStates; }
            set { taskStates = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("TaskStates")); }
        }

        private string descriptionTxta;

        public string DescriptionTxt
        {
            get { return descriptionTxta; }
            set { descriptionTxta = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("DescriptionTxt")); }
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        JObject user = new JObject();
        JObject team = new JObject();
        JObject task = new JObject();
        JObject adminInfo = new JObject();
        JObject logedUser = new JObject();
        int userID = 0;
        bool adminEdit = false;

        public TeamViewer(JObject admin, JObject users, JObject team, JObject logedUser)
        {
            this.team = team;
            this.logedUser = logedUser;
            UsersItems = new ObservableCollection<string>();
            RoleItems = new List<string>();
            ItemsCB = new List<string>();
            ItemsCB.Add("Yes");
            ItemsCB.Add("No");
            RoleItems.Add("Main admin"); RoleItems.Add("Manager"); RoleItems.Add("Programmer"); RoleItems.Add("Designer"); RoleItems.Add("Manager"); RoleItems.Add("Advertisment"); RoleItems.Add("");
            if (admin["id"].ToString() == "no")
            {
                Info = "You are not a Admin, so you have not got ability to change users settings.";
                ElseInfo = "You have not got a desrciption, because you are not a admin.";
                EnableContent = false;
            }
            else
            {
                Info = "You are a Admin at team (team name " + team["name"].ToString() + ").";
                ElseInfo = "Your description is " + admin["description"].ToString() + ".";
                adminEdit = true;
                EnableContent = true;
            }
            JArray array = (JArray)users["users"];
            for (int i = 0; i < array.Count; i++)
            {
                UsersItems.Add(users["users"][i]["name"].ToString());
            }
            DataContext = this;
            InitializeComponent();
        }

        public async Task GetUserDetail()
        {
            DescriptionTxt = "";
            HttpClient http = new HttpClient();
            try
            {
                string urlUserId = "http://www.g-pos.8u.cz/api/user-id-by-name/" + lbUsers.SelectedItem.ToString();
                HttpResponseMessage responseUser = await http.GetAsync(urlUserId, HttpCompletionOption.ResponseContentRead);
                string res = await responseUser.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                userID = Convert.ToInt32(jo["id"]);
                string url = "http://www.g-pos.8u.cz/api/get-user-detail-by-id/" + jo["id"].ToString();
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res2 = await response.Content.ReadAsStringAsync();
                JObject jo2 = JObject.Parse(res2);
                user = jo2;
                RoleTxt = jo2["user"]["role"].ToString();
                if (RoleTxt == logedUser["user"]["role"].ToString() || logedUser["user"]["role"].ToString() == "Main admin" || RoleTxt == "")
                {
                    adminEdit = true;
                    EnableContent = true;
                }
                else
                    EnableContent = false;

                string url3 = "http://www.g-pos.8u.cz/api/get-admin/" + userID;
                HttpResponseMessage response3 = await http.GetAsync(url3, HttpCompletionOption.ResponseContentRead);
                string res3 = await response3.Content.ReadAsStringAsync();
                JObject jo3 = JObject.Parse(res3);
                if (jo3["id"].ToString() == "no")
                    Admin = "No";
                else
                {
                    Admin = "Yes";
                    DescriptionTxt = jo3["description"].ToString();
                }
                adminInfo = jo3;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-task-detail/" + userID;
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                task = jo;
                JArray array = (JArray)jo["task"];
                int numberOfTask = 0;
                int numberOfTaskDone = 0;
                for (int i = 0; i < array.Count; i++)
                {
                    if (jo["task"][i]["state"].ToString() == "Done")
                        numberOfTaskDone++;
                    numberOfTask++;
                }
                TaskStates = numberOfTaskDone + "/" + numberOfTask;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private async void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserSelect = "Detail for user - " + lbUsers.SelectedItem.ToString();
            await GetUserDetail();
        }

        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient http = new HttpClient();
            bool deleting = false;
            try
            {
                if (adminYesNo.SelectedItem.ToString() == "Yes" && descriptionTxt.Text != null && adminInfo["id"].ToString() == "no")
                {
                    string url = "http://www.g-pos.8u.cz/api/post-admin/{\"teamCode\":\"" + team["code"].ToString() + "\",\"userId\":\"" + user["user"]["id"].ToString() + "\",\"description\":\"" + descriptionTxt.Text + "\"}";
                    HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    string res = await response.Content.ReadAsStringAsync();
                    JObject jo = JObject.Parse(res);
                }
                else if (adminYesNo.SelectedItem.ToString() == "No" && adminInfo["id"].ToString() != "no")
                {
                    string url = "http://www.g-pos.8u.cz/api/delete-admin/" + user["user"]["id"].ToString();
                    HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                    string res = await response.Content.ReadAsStringAsync();
                    JObject jo = JObject.Parse(res);
                    if (jo["done"].ToString() == "yes")
                    {
                        MessageBox.Show("Successfully deleted from admins.", "Success", MessageBoxButton.OK);
                        deleting = true;
                    }
                }

                if (roleTx.Text != user["user"]["role"].ToString() && !deleting)
                {
                    string url2 = "http://www.g-pos.8u.cz/api/put-user/{\"name\":\"" + user["user"]["name"].ToString() + "\",\"password\":\"" + user["user"]["password"].ToString() + "\",\"email\":\"" + user["user"]["email"].ToString() + "\",\"team\":\"" + user["user"]["team"].ToString() + "\",\"time\":\"" + user["user"]["time"].ToString() + "\",\"role\":\"" + roleTx.Text + "\"}";
                    HttpResponseMessage response2 = await http.GetAsync(url2, HttpCompletionOption.ResponseContentRead);
                    string res2 = await response2.Content.ReadAsStringAsync();
                    JObject jo2 = JObject.Parse(res2);
                }

                MessageBox.Show("Successfully saved.", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            deleting = false;
            await GetUserDetail();
        }

        private void taskDetail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            AllTasks allTasks = new AllTasks(task, userID, EnableContent, Convert.ToInt32(user["user"]["team"]));
            allTasks.ShowDialog();
        }

        private void postBtn_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem.ToString() != null)
            {
                FileUpload fileUpload = new FileUpload(logedUser, user);
                fileUpload.ShowDialog();
            }
            else
                MessageBox.Show("Choose user.", "Error", MessageBoxButton.OK);
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
