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

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Adresa = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            Adresa = "username.gte";
            MainUI mainUI = new MainUI();
            mainUI.Show();
            tbName.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbName.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbPasswordReally.Visibility = Visibility.Collapsed;
            if (!File.Exists(Adresa)) File.WriteAllText(Adresa, string.Empty);
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
        /*public static byte[] GetHash(string input)
        {
            using (HashAlgorithm Algoritmus = SHA256.Create()) return Algoritmus.ComputeHash(Encoding.UTF8.GetBytes(input));
        }
        public static string GetHashString(string input)
        {
            StringBuilder SB = new StringBuilder();
            foreach (byte B in GetHash(input)) SB.Append(B.ToString("X2"));
            return SB.ToString();
        }*/
        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            PHFraze = "Název účtu";
            string name = tbName.Text;
            string password = tbPassword.Password;
            string passwordReally = tbPasswordReally.Password;

            string nameHash = name.GetHashCode().ToString().Replace("-", String.Empty);
            string passwordHash = password.GetHashCode().ToString().Replace("-", String.Empty);
            bool Bad = false;
            bool Good = false;
            string[] nameHashText;
            string currentName = string.Empty;
            string currentPassword = string.Empty;
            switch (IsRegistration)
            {
                case false:
                    nameHashText = File.ReadAllLines(Adresa);
                    for (int i = 1; i < nameHashText.Length; i++)
                    {
                        currentName = nameHashText[i].Split(' ')[0];
                        currentPassword = nameHashText[i].Split(' ')[1];
                        if (currentName.Equals(nameHash))
                        {
                            if (currentPassword.Equals(passwordHash)) 
                            { 
                                MessageBox.Show("Úspěšné přihlášení! //nezapomenout smazat", "Povedlo se!"); Good = true;
                                Close(); 
                                break; 
                            }
                            else Bad = true;
                        }
                        else Bad = true;
                    }
                    break;
                case true:
                    if (!passwordReally.Equals(password)) MessageBox.Show("Nezadal jste identická hesla!", "Chybné heslo!");
                    else
                    {
                        File.WriteAllText(Adresa, $"{File.ReadAllText(Adresa)}\n{name.GetHashCode().ToString().Replace("-", String.Empty)} {password.GetHashCode().ToString().Replace("-", String.Empty)}");
                        ClickBorder();
                    }
                    break;
            }
            if (Bad && Good == false) MessageBox.Show("Neúspěšné přihlášení! //nezapomenout smazat", "Nepovedlo se!");
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
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ClickBorder();
        }
    }
}
