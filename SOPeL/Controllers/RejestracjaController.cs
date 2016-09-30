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
    public class RejestracjaController : Controller
    {
        // GET: Rejestracja
        public ActionResult Index()
        {
            List<Pracownik> pracownicy = new List<Pracownik>();
            List<string> specjalizacje = new List<string>();
            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");
            NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT * FROM pracownicy");

            while (dr.Read())
            {
                pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
            }

            dr.Close();

            dr = Database.wykonajZapytanieDQL("SELECT DISTINCT specjalizacja FROM pracownicy");

            while (dr.Read())
            {
                specjalizacje.Add(dr[0].ToString());
            }

            dr.Close();

            ViewBag.Pracownicy = pracownicy;
            ViewBag.Specjalizacje = specjalizacje;

            Database.zamknijPolaczenie();
            return View("~/Views/Portal pacjenta/Rejestracja on-line/index.cshtml");
        }

        public JsonResult ListaPracownikow(string spec)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();

            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");
            NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT * FROM pracownicy where specjalizacja like '" + spec + "'");

            while (dr.Read())
            {
                pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
            }

            dr.Close();

            return Json(pracownicy);
        }

        [HttpPost]
        public ActionResult Index(Rezerwacja rezerwacja, string idrez)//idrez ma następujący format idLekarza-godzina-data
        {
            //Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

            //string[] rez = idrez.Split('-');

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT count(pesel) from pacjenci where pesel = '" + rezerwacja.Pacjent.Pesel + "'");
            //dr.Read();
            //if (dr[0].ToString() == "0")
            //{
            //    dr.Close();

            //    Database.wykonajZapytanieDML("INSERT INTO pacjenci (imie,nazwisko,pesel,dok_toz,telefon,email,plec)" +
            //        "VALUES('" + rezerwacja.Pacjent.Imie.ToUpper() + "', '" + rezerwacja.Pacjent.Nazwisko.ToUpper() + "', '" 
            //        + rezerwacja.Pacjent.Pesel + "', '" + rezerwacja.Pacjent.DokumentTozsamosci.ToUpper() 
            //        + "', '" + rezerwacja.Pacjent.Telefon.ToUpper() + "', '" 
            //        + rezerwacja.Pacjent.Email.ToUpper() + "', '" + rezerwacja.Pacjent.Plec.ToUpper() + "')");
            //}
            //dr.Close();
            //dr = Database.wykonajZapytanieDQL("Select id from pacjenci where pesel = '" + rezerwacja.Pacjent.Pesel + "'");

            //dr.Read();

            //string id = dr["Id"].ToString();

            //dr.Close();

            //Database.wykonajZapytanieDML("INSERT INTO REZERWACJE (ID_PRACOWNIKA,ID_PACJENTA,data)" +
            //    "VALUES(" + rez[0] + ", " + id + ", '" + rezerwacja.DataRezerwacji.ToString("yyyy-MM-dd") + " " + rezerwacja.GodzinaRezerwacji + "')");

            //Database.zamknijPolaczenie();
            return RedirectToAction("Index");
        }



        public ActionResult wczytaj()
        {
            Pacjent pac = new Pacjent();
            NpgsqlDataReader wynik = Database.wykonajZapytanieDQL("Select * from pacjenci");
            while (wynik.Read())
            {
                pac.Imie = wynik[2] as string;
                pac.Nazwisko = wynik[1] as string;
            }
            Database.zamknijPolaczenie();

            return View("Index", pac);
        }

        public JsonResult GetJsonPrac(string term)
        {
            NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select imie || ' ' || nazwisko || ' (' || specjalizacja || ')' from pracownicy");
            List<string> listaPracowników = new List<string>();
            while (dr.Read())
            {
                if((dr[0]as string).ToUpper().Contains(term.ToUpper()))
                listaPracowników.Add(dr[0].ToString());
            }

            Database.zamknijPolaczenie();

            return Json(listaPracowników ,JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult pobierzTerminarzWybranegoLekarza(int idi ,string wybranaData)
        {
            //List<Pracownik> pracownicy = new List<Pracownik>();
            //Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");
            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT * FROM pracownicy where id =" + idi);

            //while (dr.Read())
            //{
            //    pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
            //}
            //dr.Close();


            //List<Rezerwacja> rezerwacje = new List<Rezerwacja>();
            //Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");
            //dr = Database.wykonajZapytanieDQL("SELECT * FROM v_RezerwacjePacjentow where id_pracownika = " + idi + " and date(data) ='" + wybranaData + "'");
            //while (dr.Read())
            //{
            //    rezerwacje.Add(new Rezerwacja() { GodzinaRezerwacji = dr["data"].ToString().Substring(11, 5), DataRezerwacji = (DateTime)dr["data"], Pacjent = new Pacjent() { Nazwisko = dr["nazwisko_pacjenta"].ToString(), Imie = dr["imie_pacjenta"].ToString() }, Pracownik = new Pracownik() { Id = (int)dr["id_pracownika"] } });
            //}
            //dr.Close();

            //Database.zamknijPolaczenie();

            //ViewBag.Pracownicy = pracownicy;
            //ViewBag.Rezerwacje = rezerwacje;


            return PartialView("_Terminarz");
        }
            //public ActionResult pokazLekarza(Rezerwacja rezerwacja)
            //{
            //    List<Pracownik> wybranyLekarz = new List<Pracownik>();

            //    Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

            //    NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select * from prac"); /*from prac where nazw=" + rezerwacja.pracownik.nazwisko);*/

            //    while (dr.Read())
            //    {
            //        wybranyLekarz.Add(new Pracownik() { id = (int)dr["id"], imie = dr["imie"].ToString(), nazwisko = dr["nazw"].ToString(), specjalizacja = dr["spec"].ToString() });
            //    }

            //    ViewBag.pokazLekarza = wybranyLekarz;

            //    return View();
            //}
        }
}