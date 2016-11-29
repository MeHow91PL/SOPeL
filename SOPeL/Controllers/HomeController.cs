using SOPeL.DAL;
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
        private SopelContext db = new SopelContext();

        public ActionResult Index()
        {
            
            //funckja tylko po to by zalaozcy baze przy urucghomieniu za pomoca inicjalizera
            //var uzytkownicyList = db.Uzytkownicy.ToList();
            return View();
        }

        [HttpPost] // z widoku _LogowanieParatial
        public ActionResult Logowanie(Uzytkownik uzytkownik)
        {
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");
            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT haslo from uzytkownicy where login = '" + uzytkownik.Login + "'");
            //try
            //{
            //    dr.Read();
            //    if (dr[0].ToString() == uzytkownik.Haslo)  // sprawdza czy podane przez uzytkownika haslo odpowiada temu z bazy
            //    {
            //        dr.Close();
            //        return RedirectToAction("Index", "Przychodnia");
            //    }
            //    else //gdy uzytkownik istniej w bazie ale hasło sie nie zgadza
            //    {
            //        dr.Close();
            //        return RedirectToAction("Index", "Home");
            //    }
            //}
            //catch (Exception)  //wyłapuje sytuacje, gdy użytkownik poda login nieistniejacy w bazie, bo wtedy dr.Read() nie ma wartosci
            //{

            //    return RedirectToAction("Index", "Home");
            //}
            var a = db.Pracownicy.ToArray();
            return RedirectToAction("Index", "Home");
        }
    }
}