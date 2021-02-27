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
using LoginAndMainUI;
using Newtonsoft.Json.Linq;

namespace LoginAndMainUI
{
    /// <summary>
    /// Interakční logika pro ReminderWindow.xaml
    /// </summary>
    /// 

    class Udalost
    {

        public DateTime CasUdalosti { get; set; }
        public string Nazev { get; set; }
        // public string Uzivatel { get; set; } Dodelat s databází
        public string Poznamka { get; set; }

        public Udalost(string nazev, DateTime casUdalosti, string poznamka)
        {
             Nazev = nazev;
             CasUdalosti = casUdalosti;
             Poznamka = poznamka;
        }

        public void MSGbox()
        {
            MessageBox.Show(Nazev + Convert.ToString(CasUdalosti), "test", MessageBoxButton.OK);
        }

    }

    public partial class ReminderWindow : Window
    {
        DateTime NOW =  DateTime.Now;
        List<Udalost> MojeUdalosti = new List<Udalost>();
        bool OkName = false;
        bool OkDate = false;
        JObject user = new JObject();

        public void KonecTimeLimitu()
        {
            foreach (Udalost item in MojeUdalosti)
            {
                try {
                    if (item.CasUdalosti.Year == NOW.Year && item.CasUdalosti.Month == NOW.Month && item.CasUdalosti.Day == NOW.Day && item.CasUdalosti.Hour == NOW.Hour) //Pridat minuty, zatim nefunguji 
                    {
                        MessageBox.Show(item.Nazev, "Začátek události", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Fail");
                    }
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message,"Error",MessageBoxButton.OK);
                }


            }
            //Metoda vyhodi msg box kdyz se bude cas na PC rovnat casu zadani

        }
        public ReminderWindow(JObject jo)
        {
            user = jo;
            InitializeComponent();
            dtVelky.DisplayDate = NOW;

        }
       
        public bool Kontrola()
        {
            OkName = false;
            OkDate = false;

            //Kontrola formatu
            if (tbNazev.Text != null || tbNazev.Text.Length > 2)
            {
                OkName = true;
            }

            if (dtVelky.SelectedDate.HasValue)
            {
                if (dtVelky.SelectedDate.Value >= DateTime.Now)
                {
                    OkDate = true;
                }
            }
            return OkDate;

        }

        public async void AddEvent_Click(object sender, RoutedEventArgs e)
        {

            // logika na add úkolu do seznamu 

            DateTime MyDate;
            string[] stringMaleDatum = dtMaly.Text.Split(':');
            HttpClient http = new HttpClient();

            if (dtVelky.SelectedDate != dtVelky.DisplayDate)
            {
                MyDate = Convert.ToDateTime(dtVelky.SelectedDate);
                MyDate = MyDate.AddHours(Convert.ToDouble(stringMaleDatum[0]));
                MyDate = MyDate.AddMinutes(Convert.ToDouble(stringMaleDatum[1]));
            }
            else
            {
                return;
            }

            Kontrola();
            if (OkDate == true && OkName == true)
            {
                Udalost u = new Udalost(tbNazev.Text, MyDate, tbPoznamka.Text);
                MojeUdalosti.Add(u);
                u.MSGbox();

                try
                {
                    if (chBoxIsPublic.IsChecked == true)
                    {
                        string url = "http://www.g-pos.8u.cz/api/post-event/{\"nazev\":\"" + tbNazev.Text + "\",\"description\":\"" + tbPoznamka.Text + "\",\"od_kdy\":\"" + MyDate.ToString("yyyy-MM-dd") + "\",\"team\":" + user["user"]["team"] + ",\"public\":\"" + 1 +"\",\"user\":\"" + user["user"]["id"] + "\"}";
                        HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                        string res2 = await response.Content.ReadAsStringAsync();
                        JObject jo2 = JObject.Parse(res2);
                    }
                    else
                    {
                        string url = "http://www.g-pos.8u.cz/api/post-event/{\"nazev\":\"" + tbNazev.Text + "\",\"description\":\"" + tbPoznamka.Text + "\",\"od_kdy\":\"" + MyDate.ToString("yyyy-MM-dd") + "\",\"team\":" + user["user"]["team"] + ",\"public\":\"" + 0 + "\",\"user\":\"" + user["user"]["id"] + "\"}";
                        HttpResponseMessage response = await http.GetAsync(url, HttpCompletionOption.ResponseContentRead);
                        string res2 = await response.Content.ReadAsStringAsync();
                        JObject jo2 = JObject.Parse(res2);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Špatný formát", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
   


        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
