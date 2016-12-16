using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SOPeL.Models;
using SOPeL.DAL;
using SOPeL.Infrastructure;
using System.Net;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;

namespace SOPeL.Controllers
{
    [Authorize]
    public class PacjenciController : Controller
    {
        SopelContext db = new SopelContext();
        // GET: Pacjenci
        public ActionResult Index(string searchString)
        { 
            var pacjenci = from s in db.Pacjenci select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                pacjenci = pacjenci.Where(s => s.Nazwisko.Contains(searchString)
                                            || s.Pesel.Contains(searchString));
            }
            return View(pacjenci.ToList());
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

        public ActionResult ZapiszDodajPacjenta([Bind(Include = "Imie,Nazwisko,Pesel,DataUrodzenia,KodPocztowy,Miasto,Ulica,NrDomu,NrLokalu,Telefon,Email,Plec,Aktw,ID")] Pacjent pacjent, Adres adres)
        {
            pacjent.Adres = adres;

            if (db.Pacjenci.Any(p => p.ID == pacjent.ID))
            {
                db.Entry(pacjent).State = EntityState.Modified;
                db.Entry(pacjent.Adres).State = EntityState.Modified;
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
            var pacjenci = from s in db.Pacjenci select s;
            return PartialView("PacjenciPrzychodnia", pacjenci.ToList());
        }
    }
}