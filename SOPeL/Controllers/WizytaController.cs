using SOPeL.DAL;
using SOPeL.Infrastructure;
using SOPeL.Models;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    [Authorize]
    public class WizytaController : Controller
    {
        SopelContext db = new SopelContext();
        //GET: Poczekalnia
        public ActionResult Index()
        {
            //var yesterDay = DateTime.Now.AddDays(-1);
            //var yesterDayshort = yesterDay.ToShortDateString();
            //var nextDay = DateTime.Now.AddDays(+1);
            //var toDay = DateTime.Now.ToShortDateString();

            //var rez = (from n in db.Rezerwacje.OrderByDescending(n => n.DataRezerwacji) where  (n.DataRezerwacji.Date() = yesterDayshort ) /*&& (n.DataRezerwacji < nextDay)*/ select n).ToList();

            //var dzis = DateTime.Now.ToString("yyyy-MM-dd");
            //var rez = db.Rezerwacje.Where(r => r.DataRezerwacji.ToString() == dzis).ToList();



            var prac = db.Pracownicy.ToList();
            var wiz = db.Wizyty.ToList();
            var rez = db.Rezerwacje.Where(r => r.Stat != "W").ToList();
            var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
            return View(model);

        }
        public ActionResult WyswietlanieLekarzy(int? idlekarza)
        {



            if ((idlekarza == null)|| (idlekarza==0))
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r=> r.Stat != "W").ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia", model);

            }
            else
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r => r.PracownikID == idlekarza && r.Stat != "W").ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia",model);

            }
        }

        public PartialViewResult dodajWizyte(int idrez)
        {

            Wizyta wizyta = new Wizyta();
            Rezerwacja rezerwacja = db.Rezerwacje.Find(idrez);
            wizyta.Pacjent = rezerwacja.Pacjent;
            wizyta.RezerwacjaId = rezerwacja.Id;
            wizyta.DataWizyty = DateTime.Now;
            wizyta.DataModyfikacji = DateTime.Now;
            wizyta.PacjentID = rezerwacja.PacjentID;
            wizyta.PracownikID = rezerwacja.PracownikID;

            return PartialView("_KartaWizyty", wizyta);
        }

        public ActionResult ZapiszDodajWizyte([Bind(Include = "Id, Zalecenia,Skierowanie ,DataWizyty,DataModyfikacji,PacjentID,PracownikID,RezerwacjaId,Rozpoznanie,Wywiad,Badanie")] Wizyta wizyta)
        {
            db.Wizyty.Add(wizyta);
            var idrez = wizyta.RezerwacjaId;
            Rezerwacja rezerwacja = db.Rezerwacje.Find(idrez);
            rezerwacja.Stat = "W";
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}