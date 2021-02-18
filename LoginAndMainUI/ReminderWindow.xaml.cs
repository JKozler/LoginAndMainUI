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

        public void Kontrola()
        {
            //Kontrola formatu
        }
        public void KonecTimeLimitu()
        {
            foreach (Udalost item in MojeUdalosti)
            {
                int year = NOW.Year;
                int month = NOW.Month;
                int day = NOW.Day;
                int hour = NOW.Hour;
                int minute = NOW.Minute;

                try {
                    if (item.CasUdalosti.Year == year && item.CasUdalosti.Month == month && item.CasUdalosti.Day == day && item.CasUdalosti.Hour == hour) //Pridat minuty
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
        public ReminderWindow()
        {
            InitializeComponent();
            dtVelky.DisplayDate = NOW;

        }
        List<Udalost> MojeUdalosti = new List<Udalost>();

        public void AddEvent_Click(object sender, RoutedEventArgs e)
        {

            // logika na add úkolu do seznamu 

            DateTime MyDate;
            string[] stringMaleDatum = dtMaly.Text.Split(':');

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


            
            Udalost u = new Udalost(tbNazev.Text,MyDate, tbPoznamka.Text);
            MojeUdalosti.Add(u);
            u.MSGbox();


        }

        private void Tester_Click(object sender, RoutedEventArgs e)
        {
            KonecTimeLimitu();
        }
    }
}
