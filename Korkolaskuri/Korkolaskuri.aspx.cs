// <summary>
// Laskee korkolaskujen puuttuvia muuttujia.
// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Korkolaskuri
{
    public partial class Korkolaskuri : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        // <summary>
        // Buttonin event handler, jossa tarkastellaan jokaisen syötetyn kentän arvoa ja
        // jos kaikki paitsi yksi muuttuja on syötetty, laskee puuttuvan muuttujan arvon
        // ja sijoittaa sen oikeaan kenttään.
        // Toistaiseksi laskee niin, että vuodessa on 360 kalenteripäivää.
        // </summary>
        protected void btnLaske_Click(object sender, EventArgs e)
        {
            // Puretaan ensin kenttien arvot muuttujien arvoiksi
            double? alkupaaoma = null, korkoprosentti = null, korko = null;
            
            try
            {
                alkupaaoma = Double.Parse(txtAlkupaaoma.Text);
            } catch (Exception)
            {
                // Ei tarvitse tehdä mitään vielä
            }

            // Jos ensimmäinen parse epäonnistuu, ei se ole vielä syy keskeyttää parsimista,
            // siksi korkoprosentti on toisen tryn sisällä.
            try
            {
                korkoprosentti = Double.Parse(txtKorkoprosentti.Text);
            }
            catch (Exception)
            {
                // Ei tarvitse tehdä mitään vielä
            }

            try
            {
                korko = Double.Parse(txtKorkomaara.Text);
            }
            catch (Exception)
            {
                // Ei tarvitse tehdä mitään vielä
            }

            DateTime alkupaiva = txtAikaAlku.SelectedDate;
            DateTime loppupaiva = txtAikaLoppu.SelectedDate;
            TimeSpan kesto = loppupaiva - alkupaiva;

            // Tarkasta, onko liian moni kenttä tyhjä
            // Pitkä if tarkastaa jos enemmän kuin yksi kentistä on tyhjä
            if ( (alkupaaoma == null && ( korkoprosentti == null || kesto.Days == 0 || korko == null)) ||
                    (korkoprosentti == null && ( kesto.Days == 0 || korko == null)) ||
                    ( kesto.Days == 0 && korko == null)
                )
            {
                lblInfo.Text = "Liian moni kenttä on tyhjä.";
                return;
            }
            lblInfo.Text = "";

            // Nyt tiedetään, että yksi kenttä on tyhjä, niin lasketaan puuttuvan kentän arvo.
            // Siinä tapauksessa, että käyttäjä syöttää kaikkiin kenttiin arvon, lasketaan koron määrä.
            if (alkupaaoma == null)
            {
                LaskeAlkupaaoma(korkoprosentti, kesto, korko);
                return;
            }

            if (korkoprosentti == null)
            {
                LaskeKorkoprosentti(alkupaaoma, kesto, korko);
                return;
            }

            if (kesto.Days == 0)
            {
                LaskeKesto(alkupaaoma, korkoprosentti, korko, alkupaiva);
                return;
            }

            LaskeKorko(alkupaaoma, korkoprosentti, kesto);
        }

        private void LaskeKorko(double? alkupaaoma, double? korkoprosentti, TimeSpan kesto)
        {
            double korko;
            // r = k * i * t
            korko = PyoristaLahimpaanViiteenSenttiin(alkupaaoma.Value * (korkoprosentti.Value / 100.0) * (kesto.Days / 360.0));
            txtKorkomaara.Text = korko.ToString();
        }

        // Jos alkupäivämäärää ei ole valittu, käytetään tätä päivää aloituspäivämääränä
        private void LaskeKesto(double? alkupaaoma, double? korkoprosentti, double? korko, DateTime alkupaiva)
        {
            if (alkupaiva == DateTime.MinValue)
            {
                alkupaiva = DateTime.Today;
            }

            DateTime loppupaiva = alkupaiva;

            // t = r / (k * i)
            try
            {
                double daysToGo = (360.0 * korko.Value) / (alkupaaoma.Value * (korkoprosentti.Value / 100.0));
                daysToGo = Math.Ceiling(daysToGo);
                loppupaiva = loppupaiva.AddDays(daysToGo);
            }
            catch (DivideByZeroException)
            {
                lblInfo.Text = "Yritettiin jakaa nollalla. Tarkasta arvot!";
                return;
            }

            txtAikaAlku.SelectedDate = alkupaiva;
            txtAikaLoppu.SelectedDate = loppupaiva;
            txtAikaAlku.VisibleDate = alkupaiva;
            txtAikaLoppu.VisibleDate = loppupaiva;
        }

        private void LaskeKorkoprosentti(double? alkupaaoma, TimeSpan kesto, double? korko)
        {
            double korkoprosentti = 0;
            // i = r / (k * t)
            try
            {
                korkoprosentti = korko.Value / (alkupaaoma.Value * (kesto.Days / 360.0));
            }
            catch (DivideByZeroException)
            {
                lblInfo.Text = "Yritettiin jakaa nollalla. Tarkasta arvot!";
                return;
            }

            korkoprosentti = PyoristaLahimpaanViiteenSenttiin(korkoprosentti);
            txtKorkoprosentti.Text = korkoprosentti.ToString();
        }

        private void LaskeAlkupaaoma(double? korkoprosentti, TimeSpan kesto, double? korko)
        {
            double alkupaaoma = 0;
            // k = r / (i*t)
            try
            {
                alkupaaoma = korko.Value / ((korkoprosentti.Value/100.0) * (kesto.Days/360.0));
            }
            catch (DivideByZeroException)
            {
                lblInfo.Text = "Yritettiin jakaa nollalla. Tarkasta arvot!";
                return;
            }

            alkupaaoma = PyoristaLahimpaanViiteenSenttiin(alkupaaoma);
            txtAlkupaaoma.Text = alkupaaoma.ToString();
        }

        private double PyoristaLahimpaanViiteenSenttiin(double rahamaara)
        {
            return Math.Ceiling(rahamaara / 0.5) * 0.05;
        }
    }
}