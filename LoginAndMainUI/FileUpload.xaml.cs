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
    /// Interaction logic for FileUpload.xaml
    /// </summary>
    public partial class FileUpload : Window
    {
        #region Properities
        private string info;

        public string Info
        {
            get { return info; }
            set { info = value; }
        }

        #endregion
        JObject userF = new JObject();
        JObject userT = new JObject();
        System.Windows.Forms.OpenFileDialog opf = new System.Windows.Forms.OpenFileDialog();
        public FileUpload(JObject userFrom, JObject userTo)
        {
            userF = userFrom;
            userT = userTo;
            Info = "Posting direct message to user called " + userT["user"]["name"];
            DataContext = this;
            InitializeComponent();
        }

        private async void upload_Click(object sender, RoutedEventArgs e)
        {
            HttpClient http = new HttpClient();
            if (nameTxt.Text != null && descriptionTxt.Text != null)
            {
                Console.WriteLine(opf);
                string url = "http://www.g-pos.8u.cz/api/post-mess/{\"name\":\"" + nameTxt.Text +"\",\"description\":\"" + descriptionTxt.Text + "\",\"team\":\"" + userF["user"]["team"] + "\",\"userFrom\":\"" + userF["user"]["id"] + "\",\"userTo\":\"" + userT["user"]["id"] + "\"}";
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
            }
        }
    }
}
