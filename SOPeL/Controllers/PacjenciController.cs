using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOPeL.Models;
using SOPeL.DAL;
using SOPeL.Infrastructure;
using System.Net;

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

        public ActionResult ZapiszDodajPacjenta([Bind(Include = "Imie,Nazwisko,Pesel,KodPocztowy,Miasto,Ulica,Telefon,Email,Plec,Aktw,ID")] Pacjent pacjent)
        {
            if (db.Pacjenci.Any(p => p.ID == pacjent.ID))
            {

                db.Entry(pacjent);
                db.Entry(pacjent).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                db.Pacjenci.Add(pacjent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
                          
        }

        public PartialViewResult EdytujPacjenta(int id)
        {
           
            Pacjent pacjent = db.Pacjenci.Find(id);
            return PartialView("_KartaPacjenta", pacjent);
        }


        public PartialViewResult UsunPacjenta(int id)
        {

            Pacjent pacjent = db.Pacjenci.Find(id);
            pacjent.Aktw = "N";
            db.SaveChanges();
            return PartialView("PacjenciPrzychodnia");
        }


    }
}