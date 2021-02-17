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

        public void MSGbox(Udalost udalost)
        {
            MessageBox.Show(udalost.Nazev + Convert.ToString(udalost.CasUdalosti), "test", MessageBoxButton.OK);
        }
    }

    public partial class ReminderWindow : Window
    {
        public void Kontrola()
        {
            //Kontrola formatu
        }
        public void KonecTimeLimitu()
        {
            //Metoda vyhodi msg box kdyz se bude cas na PC rovnat casu zadani
        }
        public ReminderWindow()
        {
            InitializeComponent();
        }

        private void AddEvent_Click(object sender, RoutedEventArgs e)
        {
            // logika na add úkolu do seznamu 
            Console.WriteLine(dtMaly.DataContext);
            DateTime MyDate;
            if (dtVelky.SelectedDate != dtVelky.DisplayDate)
            {
                MyDate = Convert.ToDateTime(dtVelky.SelectedDate);
            }
            else
            {
                return;
            }
                


            Udalost u = new Udalost(tbNazev.Text,MyDate, tbPoznamka.Text);
            u.MSGbox(u);

        }
    }
}
