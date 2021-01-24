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
    /// Interaction logic for AllTasks.xaml
    /// </summary>
    public partial class AllTasks : Window
    {
        JObject allTasks = new JObject();
        int userId = 0;
        public AllTasks(JObject tasks, int id)
        {
            InitializeComponent();
            allTasks = tasks;
            userId = id;
            FillDoneTasks();
            FillFailedTasks();
            FillNewTasks();
            FillProgressTasks();
        }

        public void FillDoneTasks() 
        {
            JArray array = (JArray)allTasks["task"];
            lbDoneTask.Items.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                if (allTasks["task"][i]["state"].ToString() == "Done")
                    lbDoneTask.Items.Add(allTasks["task"][i]["name"]);
            }
        }
        public void FillFailedTasks()
        {
            JArray array = (JArray)allTasks["task"];
            lbFialedTask.Items.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                if (allTasks["task"][i]["state"].ToString() == "Failed")
                    lbFialedTask.Items.Add(allTasks["task"][i]["name"]);
            }
        }
        public void FillProgressTasks()
        {
            JArray array = (JArray)allTasks["task"];
            lbProgressTask.Items.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                if (allTasks["task"][i]["state"].ToString() == "Progress")
                    lbProgressTask.Items.Add(allTasks["task"][i]["name"]);
            }
        }
        public void FillNewTasks()
        {
            JArray array = (JArray)allTasks["task"];
            lbNewTask.Items.Clear();
            for (int i = 0; i < array.Count; i++)
            {
                if (allTasks["task"][i]["state"].ToString() == "New")
                    lbNewTask.Items.Add(allTasks["task"][i]["name"]);
            }
        }

        public async Task TasksUpdate() 
        {
            HttpClient http = new HttpClient();
            try
            {
                string url = "http://www.g-pos.8u.cz/api/get-task-detail/" + userId;
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                allTasks = jo;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK);
            }
            FillDoneTasks();
            FillFailedTasks();
            FillNewTasks();
            FillProgressTasks();
        }

        private async void lbDoneTask_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lbDoneTask.SelectedItem != null)
            {
                TaskEdit taskEdit = new TaskEdit(lbDoneTask.SelectedItem.ToString(), userId);
                taskEdit.ShowDialog();
                await TasksUpdate();
            }
            else if (lbFialedTask.SelectedItem != null)
            {
                TaskEdit taskEdit = new TaskEdit(lbFialedTask.SelectedItem.ToString(), userId);
                taskEdit.ShowDialog();
                await TasksUpdate();
            }
            else if (lbProgressTask.SelectedItem != null)
            {
                TaskEdit taskEdit = new TaskEdit(lbProgressTask.SelectedItem.ToString(), userId);
                taskEdit.ShowDialog();
                await TasksUpdate();
            }
            else if (lbNewTask.SelectedItem != null)
            {
                TaskEdit taskEdit = new TaskEdit(lbNewTask.SelectedItem.ToString(), userId);
                taskEdit.ShowDialog();
                await TasksUpdate();
            }
        }
    }
}
