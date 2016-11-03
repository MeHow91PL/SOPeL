using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOPeL.Models;
using SOPeL.DAL;

namespace SOPeL.Controllers
{
    public class PacjenciController : Controller
    {
        SopelContext db = new SopelContext();
        // GET: Pacjenci
        public ActionResult Index()
        {
            //return View("~/Views/Przychodnia/Pacjenci/Index.cshtml");
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");
            //ViewBag.Pacjenci = GetListaPacjentow("Select * from pacjenci");
            //Database.zamknijPolaczenie();
            List<Pacjent> pacjenci = db.Pacjenci.ToList();
            return View("~/Views/Przychodnia/Pacjenci/Index.cshtml",pacjenci);
        }
        
        internal List<Pacjent> GetListaPacjentow(string zapytanie)
        {
            List<Pacjent> pacjenci = new List<Pacjent>();

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            //while (dr.Read())
            //{
            //    pacjenci.Add(new Pacjent() { Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Pesel = dr["pesel"].ToString() });
            //}

            //dr.Close();

            return pacjenci;
        }
    }
}