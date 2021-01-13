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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Security.Cryptography;
using System.Net.Http;
using Newtonsoft.Json.Linq;

using System.Net;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Adresa = string.Empty;
        string[] PrihlasovaciUdaje;
        public MainWindow()
        {
            WebRequest.DefaultWebProxy = null;
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            InitializeComponent();
            Adresa = "username.gte";
            tbName.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbName.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbPasswordReally.Visibility = Visibility.Collapsed;
            if (!File.Exists(Adresa)) File.WriteAllText(Adresa, string.Empty);

            PSBLabel.Visibility = Visibility.Hidden;
            PSBAcceptLabel.Visibility = Visibility.Hidden;

            if (File.ReadAllText(Adresa) != "")
            {
                if (File.ReadAllText(Adresa).Last() == '1')
                {
                    PrihlasovaciUdaje = File.ReadAllLines(Adresa);
                    tbName.Text = PrihlasovaciUdaje[PrihlasovaciUdaje.Length - 1].Split(' ')[0];
                    tbPassword.Password = PrihlasovaciUdaje[PrihlasovaciUdaje.Length - 1].Split(' ')[1];
                    CBAutomaticLoad.IsChecked = true;
                    tbName.Foreground = Brushes.Black;
                }
            }
        }
        private void tb_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Foreground == Brushes.Gray)
                {
                    ((TextBox)sender).Text = "";
                    ((TextBox)sender).Foreground = Brushes.White;
                }
            }
        }
        string PHFraze = "Název účtu";
        private void tb_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Foreground = Brushes.Gray;
                    ((TextBox)sender).Text = PHFraze;
                }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
        public static byte[] GetHash(string input)
        {
            using (HashAlgorithm Algoritmus = SHA256.Create()) return Algoritmus.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
        public static string GetHashString(string input)
        {
            StringBuilder SB = new StringBuilder();
            foreach (byte B in GetHash(input)) SB.Append(B.ToString("X2"));
            return SB.ToString();
        }
        public async Task CheckUser() {
            HttpClient http = new HttpClient();
            try
            {
                string password = tbPassword.Password.ToString();
                string url = "http://www.g-pos.8u.cz/api/get-user-detail/{\"name\":\"" + tbName.Text + "\",\"password\":\"" + password + "\"}";
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                if (jo["user"]["name"] != null)
                {
                    MainUI mainUI = new MainUI(jo);
                    mainUI.Show();
                }
                string Jmeno = tbName.Text;
                string Heslo = tbPassword.Password;
                string Check = File.ReadAllText(Adresa);
                if (Check == "") File.WriteAllText(Adresa, $"{Check}\n{Jmeno} {Heslo} {(CBAutomaticLoad.IsChecked == true ? "1" : "0")}");
                else if (Check.Last() == '0' && CBAutomaticLoad.IsChecked == true) File.WriteAllText(Adresa, $"{Check}\n{Jmeno} {Heslo} 1");
                else if (Check.Last() == '1' && CBAutomaticLoad.IsChecked == false) File.WriteAllText(Adresa, $"{Check}\n{Jmeno} {Heslo} 0");
                //POŘÁD JE POTŘEBA PODMÍNKA, ABY SE JMÉNO A HESLO ZAPSALO POUZE V PŘÍPADĚ, ŽE JSOU V POŘÁDKU
            }
            catch (Exception)
            {
                btnAccept.Content = "Bad name/password";
            }
        }

        public async Task RegisterUser()
        {
            HttpClient http = new HttpClient();
            try
            {
                string password = tbPassword.Password.ToString();
                string url = "http://www.g-pos.8u.cz/api/post-user/{\"name\":\"" + tbName.Text + "\",\"email\":\"null\",\"teamId\":0,\"password\":\"" + tbPassword.Password + "\",\"time\":0}";
                HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                string res = await response.Content.ReadAsStringAsync();
                JObject jo = JObject.Parse(res);
                if (jo["user"]["name"] != null)
                {
                    MainUI mainUI = new MainUI(jo);
                    mainUI.Show();
                }
            }
            catch (Exception)
            {
                btnAccept.Content = "Bad name/password";
            }
        }
        private async void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            PHFraze = "Název účtu";
            if (tbName.Text != null && tbPasswordReally.Password != null && !IsRegistration)
            {
                await CheckUser();
            }
            else if(tbName.Text != null && tbPasswordReally.Password != null && tbPassword.Password !=null && tbPassword.Password == tbPasswordReally.Password && IsRegistration)
            {
                await RegisterUser();
            }
            else
                MessageBox.Show("Neúspěšné přihlášení!", "Nepovedlo se!");
        }
        bool IsRegistration = false;
        void ClickBorder() 
        {
            tbName.Foreground = Brushes.Gray;
            tbPassword.Focus();

            if (IsRegistration == false)
            {
                tbPasswordReally.Visibility = Visibility.Visible;
                PHFraze = "Registrační jméno";
                tbName.Text = PHFraze;
                tbPassword.Password = "";
                tbPasswordReally.Password = tbPassword.Password;

                Grid.SetRow(borderTBName, Grid.GetRow(borderTBName) - 1);
                Grid.SetRowSpan(borderTBName, Grid.GetRowSpan(borderTBName) + 1);

                Grid.SetRow(borderTBPassword, Grid.GetRow(borderTBPassword) - 1);
                Grid.SetRowSpan(borderTBPassword, Grid.GetRowSpan(borderTBPassword) + 1);

                Grid.SetRowSpan(borderTBPasswordReally, Grid.GetRowSpan(borderTBPasswordReally) + 1);

                Grid.SetRowSpan(btnAccept, Grid.GetRowSpan(btnAccept) + 1);

                btnAccept.Content = "Registrovat";
                IsRegistration = true;
                borderRegister.ToolTip = "Přihlásit se";

                PSBLabel.Visibility = Visibility.Visible;
                PSBAcceptLabel.Visibility = Visibility.Visible;

                tbName.Focus();//////////////////////////////////////////////////////////////////////////////////////////////////////
            }
            else
            {
                PHFraze = "Název účtu";
                tbPasswordReally.Visibility = Visibility.Collapsed;
                tbName.Text = PHFraze;
                tbPassword.Password = "";
                tbPasswordReally.Password = tbPassword.Password;

                Grid.SetRow(borderTBName, Grid.GetRow(borderTBName) + 1);
                Grid.SetRowSpan(borderTBName, Grid.GetRowSpan(borderTBName) - 1);

                Grid.SetRow(borderTBPassword, Grid.GetRow(borderTBPassword) + 1);
                Grid.SetRowSpan(borderTBPassword, Grid.GetRowSpan(borderTBPassword) - 1);

                Grid.SetRowSpan(borderTBPasswordReally, Grid.GetRowSpan(borderTBPasswordReally) - 1);

                Grid.SetRowSpan(btnAccept, Grid.GetRowSpan(btnAccept) - 1);

                btnAccept.Content = "Přihlásit se";
                IsRegistration = false;
                borderRegister.ToolTip = "Registrovat";

                PSBLabel.Visibility = Visibility.Hidden;
                PSBAcceptLabel.Visibility = Visibility.Hidden;
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickBorder();
        }

        private void tbPasswordReally_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnAccept_Click(sender, e);
            }
        }

        private void PSBLabel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (((Label)sender).Name)
            {
                case "PSBLabel":
                    PSBLabel.Visibility = Visibility.Hidden;
                    PSBAcceptLabel.Visibility = Visibility.Visible;
                    tbPassword.Focus();
                    break;
                case "PSBAcceptLabel":
                    PSBAcceptLabel.Visibility = Visibility.Hidden;
                    PSBLabel.Visibility = Visibility.Visible;
                    tbPasswordReally.Focus();
                    break;
            }
        }
    }
}
