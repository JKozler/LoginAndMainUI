using Newtonsoft.Json.Linq;
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
    /// Interaction logic for TeamViewer.xaml
    /// </summary>
    public partial class TeamViewer : Window
    {
        public TeamViewer(JObject admin, JObject users, JObject team)
        {
            if (admin["admin"].ToString() == "no")
            {
                mainInfo.Content = "You are not a Admin at team with name " + team["name"].ToString() + ".";
                description.Content = "You have not a desrciption, because you are not a admin.";
            }
            else
            {
                mainInfo.Content = "You are a Admin at team with code " + team["code"].ToString() + ".";
                description.Content = "Your description is " + admin["description"].ToString() + ".";
            }
            InitializeComponent();
        }
    }
}
