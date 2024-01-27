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

namespace felveteli
{
    /// <summary>
    /// Interaction logic for UjDiak.xaml
    /// </summary>
    public partial class UjDiak : Window
    {
        Kuldo adatok;

        public UjDiak()
        {
            InitializeComponent();
        }

        private void btnBezar_Click(object sender, RoutedEventArgs e)
        {
           Close();
        }

        public UjDiak(Kuldo ujdiak) : this()
        {
            this.adatok = ujdiak;
            txtOM.Text = ujdiak.OM_Azonosito;
            txtNev.Text = ujdiak.Neve;
            txtEmail.Text = ujdiak.Email;
            txtErtesites.Text = ujdiak.ErtesitesiCime;
            txtMagyar.Text = Convert.ToString(ujdiak.Magyar);
            txtMatek.Text = Convert.ToString(ujdiak.Matematika);
            dpSzuletesi.Text = Convert.ToString(ujdiak.SzuletesiDatum);
        }

        bool IsValidEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false;
            }
            else if (trimmedEmail.Contains(".") == false || trimmedEmail.Split('@')[1].Count(x => x == '.') > 1)
            {
                return false;
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }

  

        }

        private void btnBekuld_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOM.Text) || string.IsNullOrEmpty(txtNev.Text) || string.IsNullOrEmpty(txtMatek.Text) || string.IsNullOrEmpty(txtMagyar.Text) || string.IsNullOrEmpty(txtErtesites.Text) || string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(dpSzuletesi.Text))
            {
                MessageBox.Show("Nem adtál meg mindenhova adatot!");
                return;
            }
            int smt;
            if (txtOM.Text.Length == 11 || txtOM.Text[0] == 7)
            {
                bool result = Int32.TryParse(txtOM.Text, out smt);
                if (result == true)
                {
                    MessageBox.Show("Csak szám adat lehet az om azonosítóban!");
                    return;
                }
                else
                {
                    adatok.OM_Azonosito = txtOM.Text;
                }
            }
            else
            {
                MessageBox.Show("11 szám hosszú az OM azonosító és 7-el kezdődik!");
                return;
            }

            if (txtNev.Text.Split(' ').Length < 2)
            {
                MessageBox.Show("Vezetéknév és keresztnév is kell");
                txtNev.BorderBrush = System.Windows.Media.Brushes.Red;
                txtNev.Focus();
                return;
            }
            else
            {
                string nevString = "";
                var nevek = txtNev.Text.Split(' ');
                for (int i = 0; i < nevek.Length; i++)
                {
                    nevString += (nevek[i][0].ToString().ToUpper()) + nevek[i].Substring(1);
                    nevString += " ";
                }
                adatok.Neve = nevString.Trim();
                txtNev.BorderBrush = System.Windows.Media.Brushes.Black;
            }


            adatok.ErtesitesiCime = txtErtesites.Text;

            if(IsValidEmail(txtEmail.Text) == false)
            {
                MessageBox.Show("Hiba van az email címben");
                return;
            }
            else
            {
                adatok.Email = txtEmail.Text;
            }

            adatok.SzuletesiDatum = Convert.ToDateTime(dpSzuletesi.Text);


            try
            {
                if( Convert.ToInt32(txtMatek.Text) > 50 ||  Convert.ToInt32(txtMatek.Text) < 0)
                {
                    MessageBox.Show("0 és 50 között lehet csak a pontszám!");
                    return;
                }
                else { adatok.Matematika = int.Parse(txtMatek.Text); }
                
            }
            catch
            {
                MessageBox.Show("A matematika sorba nem számot írt!");
                return;
            }

            try
            {
                if (Convert.ToInt32(txtMagyar.Text) > 50 || Convert.ToInt32(txtMagyar.Text) < 0)
                {
                    MessageBox.Show("0 és 50 között lehet csak a pontszám!");
                    return;
                }
                else { adatok.Magyar = int.Parse(txtMagyar.Text); }

            }
            catch
            {
                MessageBox.Show("A magyar sorba nem számot írt!");
                return;
            }

            Close();
            
        }

    }
}
