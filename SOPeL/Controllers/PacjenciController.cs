using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOPeL.Models;
using SOPeL.DAL;
using SOPeL.Infrastructure;

namespace SOPeL.Controllers
{
    public class PacjenciController : PrzychodniaMasterController
    {
        SopelContext db = new SopelContext();
        // GET: Pacjenci
        public ActionResult Index()
        {
            
            List<Pacjent> pacjenci = db.Pacjenci.ToList();
            return View("~/Views/Przychodnia/Pacjenci/Index.cshtml",pacjenci);
        }
        
        internal List<Pacjent> GetListaPacjentow(string zapytanie)
        {
            List<Pacjent> pacjenci = new List<Pacjent>();

            return pacjenci;
        }

        public PartialViewResult dodajPacjenta()
        {
            Pacjent pacjent = new Pacjent();
            return PartialView("_KartaPacjenta",pacjent);
        }



        public ActionResult ZapiszDodajPacjenta([Bind(Include = "Imie,Nazwisko,Pesel,KodPocztowy,Miasto,Ulica,Telefon,Email,Plec")] Pacjent pacjent)
        {
           
                    db.Pacjenci.Add(pacjent);
                    db.SaveChanges();
                    return RedirectToAction("Index");
              

            
        }
    }
}