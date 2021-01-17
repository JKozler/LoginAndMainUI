using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for TaskEdit.xaml
    /// </summary>
    public partial class TaskEdit : Window
    {
        string name = "";
        int teamId = 0;
        string userName = "";
        JObject task = new JObject();
        public TaskEdit(string taskName, int id)
        {
            InitializeComponent();
            name = taskName;
            teamId = id;
            GetAllUsers(teamId);
            taskState.Items.Add("New");
            taskState.Items.Add("Progress");
            taskState.Items.Add("Done");
            taskState.Items.Add("Failed");
            GetTask();
        }

        public async Task UpdateTask(string name, string description, string userName, DateTime dateFrom, DateTime dateTo, string state)
        {
            HttpClient http = new HttpClient();
            string urlUserId = "http://www.g-pos.8u.cz/api/user-id-by-name/" + userName;
            HttpResponseMessage responseUser = await http.GetAsync(urlUserId, HttpCompletionOption.ResponseContentRead);
            string res = await responseUser.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(res);
            try
            {
                string url = "http://www.g-pos.8u.cz/api/put-task/{\"id\":\"" + Convert.ToInt32(task["task"][0]["id"]) + "\",\"name\":\"" + name + "\",\"description\":\"" + description + "\",\"userId\":\"" + jo["id"].ToString() + "\",\"dateFrom\":\"" + dateFrom.ToString("yyyy-MM-dd") + "\",\"dateTo\":\"" + dateTo.ToString("yyyy-MM-dd") + "\",\"state\":\"" + state + "\"}";
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res2 = await response.Content.ReadAsStringAsync();
                JObject jo2 = JObject.Parse(res2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private async void save_Click(object sender, RoutedEventArgs e)
        {
            if (taskDateFrom.SelectedDate < taskDateTo.SelectedDate)
            {
                await UpdateTask(tbName.Text, tbDescription.Text, taskUser.SelectedItem.ToString(), Convert.ToDateTime(taskDateFrom.SelectedDate), Convert.ToDateTime(taskDateTo.SelectedDate), taskState.Text);
                MessageBox.Show("Changes was succeessfully saved", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("");
        }

        public async Task GetUserById()
        {
            int id = Convert.ToInt32(task["task"][0]["userId"]);
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-user/" + id;
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                userName = jo["name"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        public async Task GetAllUsers(int id)
        {
            HttpClient http = new HttpClient();
            taskUser.Items.Clear();
            string url = "http://www.g-pos.8u.cz/api/get-all-users-from-team/" + id;
            HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
            string res = await response.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(res);
            JArray array = (JArray)jo["users"];
            for (int i = 0; i < array.Count; i++)
            {
                taskUser.Items.Add(jo["users"][i]["name"]);
            }
        }

        public async Task GetTask()
        {
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-task-by-name/" + name;
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                task = jo;
                tbName.Text = task["task"][0]["name"].ToString();
                tbDescription.Text = task["task"][0]["description"].ToString();
                taskDateFrom.SelectedDate = Convert.ToDateTime(task["task"][0]["dateFrom"]);
                taskDateTo.SelectedDate = Convert.ToDateTime(task["task"][0]["dateTo"]);
                taskState.SelectedItem = task["task"][0]["state"].ToString();
                await GetUserById();
                taskUser.Text = userName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
        }
    }
}
