using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for TeamViewer.xaml
    /// </summary>
    public partial class TeamViewer : Window, INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;

        public TeamViewer(JObject admin, JObject users, JObject team)
        {
            UsersItems = new ObservableCollection<string>();
            if (admin["admin"].ToString() == "no")
            {
                Info = "You are not a Admin, so you have not got ability to change users settings.";
                ElseInfo = "You have not got a desrciption, because you are not a admin.";
            }
            else
            {
                Info = "You are a Admin at team (team name " + team["name"].ToString() + ").";
                ElseInfo = "Your description is " + admin["description"].ToString() + ".";
            }
            JArray array = (JArray)users["users"];
            for (int i = 0; i < array.Count; i++)
            {
                UsersItems.Add(users["users"][i]["name"].ToString());
            }
            DataContext = this;
            InitializeComponent();
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserSelect = "Detail for user - " + lbUsers.SelectedItem.ToString();
        }
    }
}
