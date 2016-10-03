﻿using SOPeL.Models;
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
            Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");
            
            ViewBag.Pracownicy = GetListaPracownikow("Select * from pracownicy");
            ViewBag.Rezerwacje = GetListaRezerwacji("Select * from v_rezerwacjepacjentow where date_trunc('day',rez_data) ='"
                + DateTime.Now.Year + "-" + string.Format("{0:00}", DateTime.Now.Month) + "-" + string.Format("{0:00}", DateTime.Now.Day) + "'");
            ViewBag.Opcje = PobierzOpcje();    

            Database.zamknijPolaczenie();
            return View("~/Views/Przychodnia/Terminarz/Index.cshtml");
        }

        private dynamic PobierzOpcje()
        {
            Dictionary<string,int> opcje = new Dictionary<string,int>();

            NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select nazwa, wartosc from opcje");

            while (dr.Read())
            {
                opcje.Add(dr["nazwa"].ToString(), Convert.ToInt32(dr["wartosc"]));
            }

            dr.Close();

            return opcje;
        }

        public ActionResult Admin()
        {
            return View("~/Views/Przychodnia/Admin/Index.cshtml");
        }

        private List<Rezerwacja> GetListaRezerwacji(string zapytanie)
        {
            List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

            NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            while (dr.Read())
            {
                rezerwacje.Add(new Rezerwacja() {
                    Pacjent = new Pacjent() { Imie = dr["pac_imie"].ToString(), Nazwisko = dr["pac_nazwisko"].ToString(), Pesel = dr["pac_pesel"].ToString() },
                    Pracownik = new Pracownik() { Imie = dr["prac_imie"].ToString(), Nazwisko = dr["prac_nazwisko"].ToString() },
                    DataRezerwacji = Convert.ToDateTime(dr["rez_data"].ToString()), godzOd = TimeSpan.FromSeconds(Convert.ToDouble(dr["rez_godz_pocz"].ToString())), godzDo = TimeSpan.FromSeconds(Convert.ToDouble(dr["rez_godz_konc"].ToString()))
                });
            }

            dr.Close();

            return rezerwacje;
        }



        internal List<Pracownik> GetListaPracownikow(string zapytanie)
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
            Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            ViewBag.Pracownicy = GetListaPracownikow("SELECT * FROM pracownicy");
            ViewBag.Rezerwacje = GetListaRezerwacji("SELECT * FROM v_RezerwacjePacjentow where date(rez_data) ='" + wybranaData + "'");
            ViewBag.Opcje = PobierzOpcje();
            Database.zamknijPolaczenie();

            return PartialView("~/Views/Przychodnia/Terminarz/TerminarzPrzychodnia.cshtml");
        }

        [HttpPost]
        public ActionResult zatwierdzenieRezerwacji(Rezerwacja rez,string submitButton)
        {
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            //if(submitButton == "Usuń")
            //Database.wykonajZapytanieDML("DELETE FROM rezerwacje WHERE id=" + rez.Id);
            ////else zapisz

            //Database.zamknijPolaczenie();

            return RedirectToAction("Index");
        }

    }
}