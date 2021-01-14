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
using Newtonsoft.Json.Linq;
using System.IO;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for SettGloraSystem.xaml
    /// </summary>
    public partial class SettGloraSystem : UserControl
    {
        MainUI MUI = new MainUI();
        public SettGloraSystem()
        {
            InitializeComponent();
            CBNastaveni.SelectedItem = Default;
            tbAdd.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbAdd.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbRemove.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbRemove.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbChange.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbChange.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbCreate.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbCreate.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbAddEmail.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbAddEmail.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbChangeName.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbChangeName.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbAddChangePassword.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbAddChangePassword.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbEmail.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbEmail.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbTask.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbTask.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
            tbDescription.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_GotKeyboardFocus);
            tbDescription.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(tb_LostKeyboardFocus);
        }
        string[] Fraze = new string[] { "Název účtu", "Nový název týmu", "Název týmu", "Email uživatele", "Nové uživatelské jméno",
                                        "Nové heslo", "Nový email", "Nový název úkolu", "Nový popis úkolu" };
        ListBoxItem Zachyt;
        private void CBNastaveni_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int Jmeno = ((ComboBox)sender).SelectedIndex;

            tbAdd.Text = Fraze[0];
            tbRemove.Text = Fraze[0];
            tbChange.Text = Fraze[1];
            tbCreate.Text = Fraze[2];
            tbAddEmail.Text = Fraze[3];
            tbChangeName.Text = Fraze[4];
            tbAddChangePassword.Text = Fraze[5];
            tbEmail.Text = Fraze[6];
            tbTask.Text = Fraze[7];
            tbDescription.Text = Fraze[8];

            tbAdd.Foreground = Brushes.Gray;
            tbRemove.Foreground = Brushes.Gray;
            tbChange.Foreground = Brushes.Gray;
            tbCreate.Foreground = Brushes.Gray;
            tbAddEmail.Foreground = Brushes.Gray;
            tbChangeName.Foreground = Brushes.Gray;
            tbAddChangePassword.Foreground = Brushes.Gray;
            tbEmail.Foreground = Brushes.Gray;
            tbTask.Foreground = Brushes.Gray;
            tbDescription.Foreground = Brushes.Gray;

            
            if (((ComboBox)sender).SelectedIndex == 3)
            {
                /*foreach (ListBoxItem Item in MUI.lbTaskProgress.ItemsSource)//Potřebuju nějak dostat jeho tasky do mejch tasku, s tím MUI je problém, napadlo mě to přes APInu stahovat
                {
                    Zachyt = Item;
                }*/
            }

            Vzhled(Jmeno);
        }
        private void tb_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Foreground == Brushes.Gray)
                {
                    ((TextBox)sender).Text = "";
                    ((TextBox)sender).Foreground = Brushes.Black;
                }
            }
        }
        private void tb_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                if (((TextBox)sender).Text.Trim().Equals(""))
                {
                    ((TextBox)sender).Foreground = Brushes.Gray;
                    switch (((TextBox)sender).Name)
                    {
                        case "tbAdd":
                            ((TextBox)sender).Text = Fraze[0];
                            break;
                        case "tbRemove":
                            ((TextBox)sender).Text = Fraze[0];
                            break;
                        case "tbChange":
                            ((TextBox)sender).Text = Fraze[1];
                            break;
                        case "tbCreate":
                            ((TextBox)sender).Text = Fraze[2];
                            break;
                        case "tbAddEmail":
                            ((TextBox)sender).Text = Fraze[3];
                            break;
                        case "tbChangeName":
                            ((TextBox)sender).Text = Fraze[4];
                            break;
                        case "tbAddChangePassword":
                            ((TextBox)sender).Text = Fraze[5];
                            break;
                        case "tbEmail":
                            ((TextBox)sender).Text = Fraze[6];
                            break;
                        case "tbTask":
                            ((TextBox)sender).Text = Fraze[7];
                            break;
                        case "tbDescription":
                            ((TextBox)sender).Text = Fraze[8];
                            break;
                    }
                }
            }
        }
        void Vzhled(int Poradnik)
        {
            switch (Poradnik)
            {
                case 0: //Manage team
                    labelAdd.Visibility = Visibility.Visible;
                    labelChange.Visibility = Visibility.Visible;
                    labelRemove.Visibility = Visibility.Visible;

                    btnAdd.Visibility = Visibility.Visible;
                    btnChange.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Visible;

                    borderAdd.Visibility = Visibility.Visible;
                    borderChange.Visibility = Visibility.Visible;
                    borderRemove.Visibility = Visibility.Visible;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

                    btnCreate.Visibility = Visibility.Hidden;
                    btnAddEmail.Visibility = Visibility.Hidden;

                    borderCreate.Visibility = Visibility.Hidden;
                    borderAddEmail.Visibility = Visibility.Hidden;

                    labelChangeName.Visibility = Visibility.Hidden;
                    labelChangePassword.Visibility = Visibility.Hidden;
                    labelEmail.Visibility = Visibility.Hidden;
                    labelLeave.Visibility = Visibility.Hidden;

                    btnChangeName.Visibility = Visibility.Hidden;
                    btnChangePassword.Visibility = Visibility.Hidden;
                    btnEmail.Visibility = Visibility.Hidden;
                    btnLeave.Visibility = Visibility.Hidden;

                    borderChangeName.Visibility = Visibility.Hidden;
                    borderChangePassword.Visibility = Visibility.Hidden;
                    borderEmail.Visibility = Visibility.Hidden;

                    labelTask.Visibility = Visibility.Hidden;
                    labelDescription.Visibility = Visibility.Hidden;

                    btnTask.Visibility = Visibility.Hidden;
                    btnDescription.Visibility = Visibility.Hidden;

                    borderTask.Visibility = Visibility.Hidden;
                    borderDescription.Visibility = Visibility.Hidden;
                    break;
                case 1: //Create team
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Visible;
                    labelAddEmail.Visibility = Visibility.Visible;

                    btnCreate.Visibility = Visibility.Visible;
                    btnAddEmail.Visibility = Visibility.Visible;

                    borderCreate.Visibility = Visibility.Visible;
                    borderAddEmail.Visibility = Visibility.Visible;

                    labelChangeName.Visibility = Visibility.Hidden;
                    labelChangePassword.Visibility = Visibility.Hidden;
                    labelEmail.Visibility = Visibility.Hidden;
                    labelLeave.Visibility = Visibility.Hidden;

                    btnChangeName.Visibility = Visibility.Hidden;
                    btnChangePassword.Visibility = Visibility.Hidden;
                    btnEmail.Visibility = Visibility.Hidden;
                    btnLeave.Visibility = Visibility.Hidden;

                    borderChangeName.Visibility = Visibility.Hidden;
                    borderChangePassword.Visibility = Visibility.Hidden;
                    borderEmail.Visibility = Visibility.Hidden;

                    labelTask.Visibility = Visibility.Hidden;
                    labelDescription.Visibility = Visibility.Hidden;

                    btnTask.Visibility = Visibility.Hidden;
                    btnDescription.Visibility = Visibility.Hidden;

                    borderTask.Visibility = Visibility.Hidden;
                    borderDescription.Visibility = Visibility.Hidden;
                    break;
                case 2: //User
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

                    btnCreate.Visibility = Visibility.Hidden;
                    btnAddEmail.Visibility = Visibility.Hidden;

                    borderCreate.Visibility = Visibility.Hidden;
                    borderAddEmail.Visibility = Visibility.Hidden;

                    labelChangeName.Visibility = Visibility.Visible;
                    labelChangePassword.Visibility = Visibility.Visible;
                    labelEmail.Visibility = Visibility.Visible;
                    labelLeave.Visibility = Visibility.Visible;

                    btnChangeName.Visibility = Visibility.Visible;
                    btnChangePassword.Visibility = Visibility.Visible;
                    btnEmail.Visibility = Visibility.Visible;
                    btnLeave.Visibility = Visibility.Visible;

                    borderChangeName.Visibility = Visibility.Visible;
                    borderChangePassword.Visibility = Visibility.Visible;
                    borderEmail.Visibility = Visibility.Visible;

                    labelTask.Visibility = Visibility.Hidden;
                    labelDescription.Visibility = Visibility.Hidden;

                    btnTask.Visibility = Visibility.Hidden;
                    btnDescription.Visibility = Visibility.Hidden;

                    borderTask.Visibility = Visibility.Hidden;
                    borderDescription.Visibility = Visibility.Hidden;
                    break;
                case 3: //Task
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

                    btnCreate.Visibility = Visibility.Hidden;
                    btnAddEmail.Visibility = Visibility.Hidden;

                    borderCreate.Visibility = Visibility.Hidden;
                    borderAddEmail.Visibility = Visibility.Hidden;

                    labelChangeName.Visibility = Visibility.Hidden;
                    labelChangePassword.Visibility = Visibility.Hidden;
                    labelEmail.Visibility = Visibility.Hidden;
                    labelLeave.Visibility = Visibility.Hidden;

                    btnChangeName.Visibility = Visibility.Hidden;
                    btnChangePassword.Visibility = Visibility.Hidden;
                    btnEmail.Visibility = Visibility.Hidden;
                    btnLeave.Visibility = Visibility.Hidden;

                    borderChangeName.Visibility = Visibility.Hidden;
                    borderChangePassword.Visibility = Visibility.Hidden;
                    borderEmail.Visibility = Visibility.Hidden;

                    labelTask.Visibility = Visibility.Visible;
                    labelDescription.Visibility = Visibility.Visible;

                    btnTask.Visibility = Visibility.Visible;
                    btnDescription.Visibility = Visibility.Visible;

                    borderTask.Visibility = Visibility.Visible;
                    borderDescription.Visibility = Visibility.Visible;
                    break;
                case 4: //Appearance
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

                    btnCreate.Visibility = Visibility.Hidden;
                    btnAddEmail.Visibility = Visibility.Hidden;

                    borderCreate.Visibility = Visibility.Hidden;
                    borderAddEmail.Visibility = Visibility.Hidden;

                    labelChangeName.Visibility = Visibility.Hidden;
                    labelChangePassword.Visibility = Visibility.Hidden;
                    labelEmail.Visibility = Visibility.Hidden;
                    labelLeave.Visibility = Visibility.Hidden;

                    btnChangeName.Visibility = Visibility.Hidden;
                    btnChangePassword.Visibility = Visibility.Hidden;
                    btnEmail.Visibility = Visibility.Hidden;
                    btnLeave.Visibility = Visibility.Hidden;

                    borderChangeName.Visibility = Visibility.Hidden;
                    borderChangePassword.Visibility = Visibility.Hidden;
                    borderEmail.Visibility = Visibility.Hidden;

                    labelTask.Visibility = Visibility.Hidden;
                    labelDescription.Visibility = Visibility.Hidden;

                    btnTask.Visibility = Visibility.Hidden;
                    btnDescription.Visibility = Visibility.Hidden;

                    borderTask.Visibility = Visibility.Hidden;
                    borderDescription.Visibility = Visibility.Hidden;
                    break;
                case 5: //Commands
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

                    btnCreate.Visibility = Visibility.Hidden;
                    btnAddEmail.Visibility = Visibility.Hidden;

                    borderCreate.Visibility = Visibility.Hidden;
                    borderAddEmail.Visibility = Visibility.Hidden;

                    labelChangeName.Visibility = Visibility.Hidden;
                    labelChangePassword.Visibility = Visibility.Hidden;
                    labelEmail.Visibility = Visibility.Hidden;
                    labelLeave.Visibility = Visibility.Hidden;

                    btnChangeName.Visibility = Visibility.Hidden;
                    btnChangePassword.Visibility = Visibility.Hidden;
                    btnEmail.Visibility = Visibility.Hidden;
                    btnLeave.Visibility = Visibility.Hidden;

                    borderChangeName.Visibility = Visibility.Hidden;
                    borderChangePassword.Visibility = Visibility.Hidden;
                    borderEmail.Visibility = Visibility.Hidden;

                    labelTask.Visibility = Visibility.Hidden;
                    labelDescription.Visibility = Visibility.Hidden;

                    btnTask.Visibility = Visibility.Hidden;
                    btnDescription.Visibility = Visibility.Hidden;

                    borderTask.Visibility = Visibility.Hidden;
                    borderDescription.Visibility = Visibility.Hidden;
                    break;
                case 6: //File
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

                    btnCreate.Visibility = Visibility.Hidden;
                    btnAddEmail.Visibility = Visibility.Hidden;

                    borderCreate.Visibility = Visibility.Hidden;
                    borderAddEmail.Visibility = Visibility.Hidden;

                    labelChangeName.Visibility = Visibility.Hidden;
                    labelChangePassword.Visibility = Visibility.Hidden;
                    labelEmail.Visibility = Visibility.Hidden;
                    labelLeave.Visibility = Visibility.Hidden;

                    btnChangeName.Visibility = Visibility.Hidden;
                    btnChangePassword.Visibility = Visibility.Hidden;
                    btnEmail.Visibility = Visibility.Hidden;
                    btnLeave.Visibility = Visibility.Hidden;

                    borderChangeName.Visibility = Visibility.Hidden;
                    borderChangePassword.Visibility = Visibility.Hidden;
                    borderEmail.Visibility = Visibility.Hidden;

                    labelTask.Visibility = Visibility.Hidden;
                    labelDescription.Visibility = Visibility.Hidden;

                    btnTask.Visibility = Visibility.Hidden;
                    btnDescription.Visibility = Visibility.Hidden;

                    borderTask.Visibility = Visibility.Hidden;
                    borderDescription.Visibility = Visibility.Hidden;
                    break;
            }
        }
        public string[] Informace = new string[10];
        // 0 = jméno na přidání
        // 1 = jméno na odebrání
        // 2 = změnit název týmu
        // 3 = změna jména
        // 4 = změna hesla
        // 5 = zadat email
        // 6 = vytvořit nový tým
        // 7 = email prvního člena
        // 8 = změnit název úkolu
        // 9 = změnit popis úkolu
        string[] PrihlasovaciUdaje;
        string[] PrihlasovaciUdajeUpraveno;
        string[] NamePassword;
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string jmeno = ((Button)sender).Name;
            switch (jmeno)
            {
                case "btnAdd":
                    Informace[0] = tbAdd.Text;
                    tbAdd.Text = Fraze[0];
                    tbAdd.Foreground = Brushes.Gray;
                    break;
                case "btnRemove":
                    Informace[1] = tbRemove.Text;
                    tbRemove.Text = Fraze[0];
                    tbRemove.Foreground = Brushes.Gray;
                    break;
                case "btnChange":
                    Informace[2] = tbChange.Text;
                    tbChange.Text = Fraze[1];
                    tbChange.Foreground = Brushes.Gray;
                    break;
                case "btnCreate":
                    Informace[3] = tbCreate.Text;
                    tbCreate.Text = Fraze[2];
                    tbCreate.Foreground = Brushes.Gray;
                    break;
                case "btnAddEmail":
                    Informace[4] = tbAddEmail.Text;
                    tbEmail.Text = Fraze[3];
                    tbEmail.Foreground = Brushes.Gray;
                    break;
                case "btnChangeName":
                    Informace[5] = tbChangeName.Text;
                    tbChangeName.Text = Fraze[4];
                    tbChangeName.Foreground = Brushes.Gray;

                    PrihlasovaciUdaje = File.ReadAllLines("username.gte");
                    PrihlasovaciUdajeUpraveno = new string[PrihlasovaciUdaje.Length];
                    NamePassword = PrihlasovaciUdaje[PrihlasovaciUdaje.Length - 1].Split(' ');
                    NamePassword[0] = Informace[5];
                    for (int i = 0; i < PrihlasovaciUdajeUpraveno.Length; i++)
                    {
                        PrihlasovaciUdajeUpraveno[i] = PrihlasovaciUdaje[i];
                        if (i == PrihlasovaciUdajeUpraveno.Length - 1) PrihlasovaciUdajeUpraveno[i] = $"{NamePassword[0]} {NamePassword[1]} {NamePassword[2]}";
                    }
                    File.WriteAllLines("username.gte", PrihlasovaciUdajeUpraveno);
                    break;
                case "btnChangePassword":
                    Informace[6] = tbAddChangePassword.Text;
                    tbAddChangePassword.Text = Fraze[5];
                    tbAddChangePassword.Foreground = Brushes.Gray;

                    PrihlasovaciUdaje = File.ReadAllLines("username.gte");
                    PrihlasovaciUdajeUpraveno = new string[PrihlasovaciUdaje.Length];
                    NamePassword = PrihlasovaciUdaje[PrihlasovaciUdaje.Length - 1].Split(' ');
                    NamePassword[1] = Informace[6];
                    for (int i = 0; i < PrihlasovaciUdajeUpraveno.Length; i++)
                    {
                        PrihlasovaciUdajeUpraveno[i] = PrihlasovaciUdaje[i];
                        if (i == PrihlasovaciUdajeUpraveno.Length - 1) PrihlasovaciUdajeUpraveno[i] = $"{NamePassword[0]} {NamePassword[1]} {NamePassword[2]}";
                    }
                    File.WriteAllLines("username.gte", PrihlasovaciUdajeUpraveno);
                    break;
                case "btnEmail":
                    Informace[7] = tbEmail.Text;
                    tbEmail.Text = Fraze[6];
                    tbEmail.Foreground = Brushes.Gray;
                    break;
                case "btnLeave":
                    break;
                case "btnTask":
                    Informace[8] = tbTask.Text;
                    tbTask.Text = Fraze[7];
                    tbTask.Foreground = Brushes.Gray;
                    break;
                case "btnDescription":
                    Informace[9] = tbDescription.Text;
                    tbDescription.Text = Fraze[8];
                    tbDescription.Foreground = Brushes.Gray;
                    break;
            }
        }
    }
}
