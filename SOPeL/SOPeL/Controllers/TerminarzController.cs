using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostgresObsuga;
using Npgsql;

namespace SOPeL.Controllers
{
    public class TerminarzController : Controller
    {
        // GET: Rejestracja
        public ActionResult Index()
        {
            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");
            
            ViewBag.Pracownicy = GetListaPracownikow("Select * from pracownicy");
            ViewBag.Rezerwacje = GetListaRezerwacji("Select * from v_rezerwacjepacjentow where date_trunc('day',data) ='"
                + DateTime.Now.Year + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day) + "'");
                
            Database.zamknijPolaczenie();
            return View();
        }

        private List<Rezerwacja> GetListaRezerwacji(string zapytanie)
        {
            List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

            NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            while (dr.Read())
            {
                rezerwacje.Add(new Rezerwacja() {Id = Convert.ToInt32(dr["id"]) , GodzinaRezerwacji = dr["data"].ToString().Substring(11, 5), DataRezerwacji = (DateTime)dr["data"], Pacjent = new Pacjent() { Nazwisko = dr["nazwisko_pacjenta"].ToString(), Imie = dr["imie_pacjenta"].ToString(), Pesel = dr["pesel_pacjenta"].ToString(), DokumentTozsamosci = dr["dok_toz"].ToString(), Email = dr["email_pacjenta"].ToString(), Telefon = dr["telefon_pacjenta"].ToString() }, Pracownik = new Pracownik() { Id = (int)dr["id_pracownika"] } });
            }

            dr.Close();

            return rezerwacje;
        }

        private List<Pracownik> GetListaPracownikow(string zapytanie)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();

            NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            while (dr.Read())
            {
                pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
            }

            dr.Close();

            return pracownicy;
        }

        public PartialViewResult pobierzTerminarz(string wybranaData, List<Pracownik> pracownicy)
        {
            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

            ViewBag.Pracownicy = GetListaPracownikow("SELECT * FROM pracownicy");
            ViewBag.Rezerwacje = GetListaRezerwacji("SELECT * FROM v_RezerwacjePacjentow where date(data) ='" + wybranaData + "'");

            Database.zamknijPolaczenie();

            return PartialView("TerminarzPrzychodnia");
        }

        [HttpPost]
        public ActionResult zatwierdzenieRezerwacji(Rezerwacja rez,string submitButton)
        {
            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

            if(submitButton == "Usuń")
            Database.wykonajZapytanieDML("DELETE FROM rezerwacje WHERE id=" + rez.Id);
            //else zapisz

            Database.zamknijPolaczenie();

            return RedirectToAction("Index");
        }

    }
}