using Npgsql;
using PostgresObsuga;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SOPeL.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //dupa
            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost] // z widoku _LogowanieParatial 
        public ActionResult Logowanie(Uzytkownik uzytkownik)
        {
            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");
            NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT haslo from uzytkownicy where login = '" + uzytkownik.Login + "'");
            try
            {
                dr.Read();
                if (dr[0].ToString() == uzytkownik.Haslo)  // sprawdza czy podane przez uzytkownika haslo odpowiada temu z bazy 
                {
                    dr.Close();
                    return RedirectToAction("Index", "Przychodnia");
                }
                else //gdy uzytkownik istniej w bazie ale hasło sie nie zgadza 
                {
                    dr.Close();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)  //wyłapuje sytuacje, gdy użytkownik poda login nieistniejacy w bazie, bo wtedy dr.Read() nie ma wartosci 
            {
                dr.Close();
                return RedirectToAction("Index", "Home");
            }
        }
    }
}