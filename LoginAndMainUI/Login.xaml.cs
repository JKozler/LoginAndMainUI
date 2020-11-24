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

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainUI mainUI = new MainUI();
            mainUI.Show();
            tbName.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbName.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbPasswordReally.Visibility = Visibility.Collapsed;
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

        private void btnAccept_Click(object sender, RoutedEventArgs e)
        {
            PHFraze = "Název účtu";
            Close();
        }
        bool IsRegistration = false;
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
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
    }
}
