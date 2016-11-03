using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    public class PoczekalniaController : Controller
    {
        // GET: Poczekalnia
        public ActionResult Index()
        {
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");
            //ViewBag.Pracownicy = GetListaPracownikow("Select * from pracownicy");
            //ViewBag.Rezerwacje = new List<Rezerwacja>();
            //Database.zamknijPolaczenie();
            return View("~/Views/Przychodnia/Poczekalnia/Index.cshtml");
        }


        internal List<Pracownik> GetListaPracownikow(string zapytanie)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            //while (dr.Read())
            //{
            //    pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
            //}

            //dr.Close();

            return pracownicy;
        }

        public PartialViewResult pobierzRezerwacje(string pracID)
        {
            List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select * from v_rezerwacjePacjentow where rez_data=current_date and prac_id = " + pracID);

            //while (dr.Read())
            //{
            //   rezerwacje.Add(new Rezerwacja()
            //    {
            //        Pacjent = new Pacjent()
            //        {
            //            Id = Convert.ToInt32(dr["pac_id"].ToString()),
            //            Imie = dr["pac_imie"].ToString(),
            //            Nazwisko = dr["pac_nazwisko"].ToString(),
            //            Pesel = dr["pac_pesel"].ToString()
            //        },
            //        godzOd = dr["rez_godz_pocz"].ToString()
            //    }
            //        );
            //}

            //dr.Close();

            //Database.zamknijPolaczenie();


            //ViewBag.Rezerwacje = rezerwacje;

            return PartialView("~/Views/Przychodnia/Poczekalnia/PoczekalniaPrzychodnia.cshtml");
        }
    }
}