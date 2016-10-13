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
    public class PoczekalniaController : Controller
    {
        // GET: Poczekalnia
        public ActionResult Index()
        {
            Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");
            ViewBag.Pracownicy = GetListaPracownikow("Select * from pracownicy");
            return View("~/Views/Przychodnia/Poczekalnia/Index.cshtml");
            Database.zamknijPolaczenie();
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
    }
}