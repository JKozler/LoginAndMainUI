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
using System.Text.RegularExpressions;
using Microsoft.Win32;
using System.Net.Http;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interaction logic for SettGloraSystem.xaml
    /// </summary>
    public partial class SettGloraSystem : UserControl
    {
        MainUI MUI = new MainUI();
        JObject user = new JObject();
        public SettGloraSystem()
        {
            InitializeComponent();
            CBNastaveni.SelectedItem = Default;
            tbAdd.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbAdd.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbRemove.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbRemove.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbChange.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbChange.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbCreate.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbCreate.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbAddEmail.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbAddEmail.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbChangeName.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbChangeName.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbAddChangePassword.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbAddChangePassword.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbEmail.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbEmail.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbTask.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbTask.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
            tbDescription.GotKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_GotKeyboardFocus);
            tbDescription.LostKeyboardFocus += new KeyboardFocusChangedEventHandler(TB_LostKeyboardFocus);
        }

        readonly string[] Fraze = new string[] { "Email uživatele", "Nový název týmu", "Název týmu", "Email uživatele", "Nové uživatelské jméno",
                                        "Nové heslo", "Nový email", "Nový název úkolu", "Nový popis úkolu" };
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
        private void TB_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
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
        private void TB_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
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
        async void Vzhled(int Poradnik)
        {
            switch (Poradnik)
            {
                case 0: //Manage team
                    labelAdd.Visibility = Visibility.Visible;
                    labelChange.Visibility = Visibility.Visible;
                    labelRemove.Visibility = Visibility.Visible;
                    lbTasks.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Visible;
                    btnChange.Visibility = Visibility.Visible;
                    btnRemove.Visibility = Visibility.Visible;

                    borderAdd.Visibility = Visibility.Visible;
                    borderChange.Visibility = Visibility.Visible;
                    borderRemove.Visibility = Visibility.Visible;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

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

                    labelAppearance.Visibility = Visibility.Hidden;

                    btnAppearance.Visibility = Visibility.Hidden;
                    break;
                case 1: //Create team
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;
                    lbTasks.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Visible;
                    labelAddEmail.Visibility = Visibility.Visible;

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

                    labelAppearance.Visibility = Visibility.Hidden;

                    btnAppearance.Visibility = Visibility.Hidden;
                    break;
                case 2: //User
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;
                    lbTasks.Visibility = Visibility.Visible;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

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

                    labelAppearance.Visibility = Visibility.Hidden;

                    btnAppearance.Visibility = Visibility.Hidden;
                    break;
                case 3: //Task
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;
                    lbTasks.Visibility = Visibility.Visible;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

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

                    labelAppearance.Visibility = Visibility.Hidden;

                    btnAppearance.Visibility = Visibility.Hidden;
                    break;
                case 4: //Appearance
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;
                    lbTasks.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

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

                    labelAppearance.Visibility = Visibility.Visible;

                    btnAppearance.Visibility = Visibility.Visible;
                    break;
                case 5: //Commands
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;
                    lbTasks.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

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

                    labelAppearance.Visibility = Visibility.Hidden;

                    btnAppearance.Visibility = Visibility.Hidden;
                    break;
                case 6: //File
                    labelAdd.Visibility = Visibility.Hidden;
                    labelChange.Visibility = Visibility.Hidden;
                    labelRemove.Visibility = Visibility.Hidden;
                    lbTasks.Visibility = Visibility.Hidden;

                    btnAdd.Visibility = Visibility.Hidden;
                    btnChange.Visibility = Visibility.Hidden;
                    btnRemove.Visibility = Visibility.Hidden;

                    borderAdd.Visibility = Visibility.Hidden;
                    borderChange.Visibility = Visibility.Hidden;
                    borderRemove.Visibility = Visibility.Hidden;

                    labelCreate.Visibility = Visibility.Hidden;
                    labelAddEmail.Visibility = Visibility.Hidden;

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

                    labelAppearance.Visibility = Visibility.Hidden;

                    btnAppearance.Visibility = Visibility.Hidden;
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
        string Email = string.Empty;
        readonly Regex TestEmail = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$"); //Zjišťuje, zda je email v pořádku
        bool Leave = false;
        OpenFileDialog OFD = new OpenFileDialog();
        private void btn_Click(object sender, RoutedEventArgs e)
        {
            string jmeno = ((Button)sender).Name;
            switch (jmeno)
            {
                case "btnAdd":
                    Informace[0] = tbAdd.Text;
                    Email = Informace[0];
                    if (!TestEmail.IsMatch(Email))
                    {
                        MessageBox.Show("Nesprávný formát emailu!", "CHYBA");
                        Email = "";
                    }
                    else MessageBox.Show("Uživatel byl úspěšně přidán!", "Povedlo se");
                    tbAdd.Text = Fraze[0];
                    tbAdd.Foreground = Brushes.Gray;
                    break;
                case "btnRemove":
                    Informace[1] = tbRemove.Text;

                    Email = Informace[1];
                    if (!TestEmail.IsMatch(Email))
                    {
                        MessageBox.Show("Nesprávný formát emailu!", "CHYBA");
                        Email = "";
                    }
                    else MessageBox.Show("Uživatel byl úspěšně odebrán!", "Povedlo se");

                    tbRemove.Text = Fraze[0];
                    tbRemove.Foreground = Brushes.Gray;
                    break;
                case "btnChange":
                    if (!tbChange.Text.Equals(Fraze[1]) || (!tbChange.Text.Equals("") && !tbChange.Text.Equals(Fraze[1])))
                    {
                        Informace[2] = tbChange.Text;
                        MessageBox.Show("Tým byl úspěšně přejmenován!", "Povedlo se");
                    }
                    else MessageBox.Show("Prázdné pole jména týmu!", "CHYBA");
                    tbChange.Text = Fraze[1];
                    tbChange.Foreground = Brushes.Gray;
                    break;
                case "btnAddEmail":
                    if (!tbCreate.Text.Equals(Fraze[2]) || (!tbCreate.Text.Equals("") && !tbCreate.Text.Equals(Fraze[2])))
                    {
                        if (!tbAddEmail.Text.Equals(Fraze[3]) || (!tbAddEmail.Text.Equals("") && !tbAddEmail.Text.Equals(Fraze[3])))
                        {
                            if (TestEmail.IsMatch(tbAddEmail.Text))
                            {
                                Informace[3] = tbCreate.Text;
                                Informace[4] = tbAddEmail.Text;
                                MessageBox.Show("Tým byl úspěšně vytvořen!", "Povedlo se");
                            }
                            else MessageBox.Show("Nesprávný formát emailu!", "CHYBA");
                        }
                        else MessageBox.Show("Musíte zadat email prvního člena!", "CHYBA");
                    }
                    else MessageBox.Show("Musíte zadat název týmu!", "CHYBA");
                    tbCreate.Text = Fraze[2];
                    tbCreate.Foreground = Brushes.Gray;

                    tbAddEmail.Text = Fraze[3];
                    tbAddEmail.Foreground = Brushes.Gray;
                    break;
                case "btnChangeName":
                    if (!tbChangeName.Text.Equals(Fraze[4]) || (!tbChangeName.Text.Equals("") && !tbChangeName.Text.Equals(Fraze[4])))
                    {
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
                        MessageBox.Show("Jméno úspěšně změněno!", "Povedlo se");
                    }
                    else MessageBox.Show("Musíte zadat nové uživatelské jméno!", "CHYBA");
                    break;
                case "btnChangePassword":
                    if (!tbAddChangePassword.Text.Equals(Fraze[5]) || (!tbAddChangePassword.Text.Equals("") && !tbAddChangePassword.Text.Equals(Fraze[5])))
                    {
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
                        MessageBox.Show("Jméno úspěšně změněno!", "Povedlo se");
                    }
                    else MessageBox.Show("Musíte zadat nové heslo!", "CHYBA");
                    break;
                case "btnEmail":
                    if (TestEmail.IsMatch(tbEmail.Text))
                    {
                        Informace[7] = tbEmail.Text;
                        MessageBox.Show("Úspěšně nastavený email!", "Povedlo se");
                    }
                    else MessageBox.Show("Chybně zadaný email!", "CHYBA");
                    tbEmail.Text = Fraze[6];
                    tbEmail.Foreground = Brushes.Gray;
                    break;
                case "btnLeave":
                    Leave = true; //NEZAPOMENOUT POTOM NASTAVIT NA FALSE PO API
                    break;
                case "btnTask":
                    if (!tbTask.Text.Equals(Fraze[7]) || (!tbTask.Text.Equals("") && !tbTask.Text.Equals(Fraze[7])))
                    {
                        Informace[8] = tbTask.Text;
                        MessageBox.Show("Úspěšně změněný název úkolu!", "Povedlo se");
                    }
                    else MessageBox.Show("Prázdné pole názvu úkolu!", "CHYBA");
                    tbTask.Text = Fraze[7];
                    tbTask.Foreground = Brushes.Gray;
                    break;
                case "btnDescription":
                    if (!tbDescription.Text.Equals(Fraze[8]) || (!tbDescription.Text.Equals("") && !tbDescription.Text.Equals(Fraze[8])))
                    {
                        Informace[9] = tbTask.Text;
                        MessageBox.Show("Úspěšně změněný popis úkolu!", "Povedlo se");
                    }
                    else MessageBox.Show("Prázdné pole popisku úkolu!", "CHYBA");
                    tbDescription.Text = Fraze[8];
                    tbDescription.Foreground = Brushes.Gray;
                    break;
                case "btnAppearance": //Funguje detekování fotky a imagebrush, jen se nedokážu dostat na ten mainborder
                    OFD.Filter = "JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|PNG files (*.png)|*.png|Bitmap files (*.bmp)|*.bmp|TIFF files (*.tiff)|*.jpg|TIF files (*.tif)|*.tif";
                    if (OFD.ShowDialog() == true)
                    {
                        ImageBrush IB = new ImageBrush(new BitmapImage(new Uri(OFD.FileName)));
                        MUI.mainBorder.Background = IB;
                    }
                    //else MessageBox.Show("Musíte si vybrat nějaký soubor!", "CHYBA");
                    break;
            }
        }
    }
}
