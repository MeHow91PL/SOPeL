using SOPeL.DAL;
using SOPeL.Infrastructure;
using SOPeL.Models;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Rotativa;

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
            var toDay = DateTime.Now.ToShortDateString();

            //var rez = (from n in db.Rezerwacje.OrderByDescending(n => n.DataRezerwacji) where  (n.DataRezerwacji.Date() = yesterDayshort ) /*&& (n.DataRezerwacji < nextDay)*/ select n).ToList();

            //var dzis = DateTime.Now.ToString("yyyy-MM-dd");
            //var rez = db.Rezerwacje.Where(r => r.DataRezerwacji.ToString() == dzis).ToList();



            var prac = db.Pracownicy.ToList();
            var wiz = db.Wizyty.ToList();
            var rez = db.Rezerwacje.Where(r => r.Stat != "W").ToList();
            //var rezToday = db.Rezerwacje.Where(g => g.DataRezerwacji.TruncateTime() == toDay).ToList();
            var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
            return View(model);

        }
        public ActionResult WyswietlanieLekarzy(int? idlekarza)
        {



            if ((idlekarza == null) || (idlekarza == 0))
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r => r.Stat != "W").ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia", model);

            }
            else
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r => r.PracownikID == idlekarza && r.Stat != "W").ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia", model);

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

        public JsonResult ICD10Autocomplete(string term)
        {
            var icd10 = db.KodyICD10.Select(i => i.Kod).Where(i => i.StartsWith(term)).ToList();
            // string[] icd10= new string[] { "one", "two", "free", "baba", "patrzy" };

            return Json(icd10, JsonRequestBehavior.AllowGet);
        }


        public PartialViewResult WyswietlHistorieWizyt(int idPac)
        {
            var Model = db.Wizyty.Where(w => w.PacjentID == idPac).ToList();
            return PartialView("ListaWizyt", Model);
        }

        public PartialViewResult pokazHistorie(int idwizy)
        {
            Wizyta wizyta = db.Wizyty.Find(idwizy);
            return PartialView("_KartaWizytyHistoria", wizyta);
        }


        public ActionResult DrukujWywiad(int idPac, string wyw)
        {
            Pacjent pac = db.Pacjenci.Find(idPac);
            WydrukWywiaduViewModel vm = new WydrukWywiaduViewModel
            {
                Pacjent = pac,
                Wywiad = wyw
            };

            vm.Wywiad.Replace("\n", "<br />");
            return PartialView("Wydruk-Wywiad",vm);
        }




        public ActionResult ZapiszDodajWizyte([Bind(Include = "Id, Zalecenia,Skierowanie ,DataWizyty,DataModyfikacji,PacjentID,Pacjent,PracownikID,Pracownik,RezerwacjaId,Rozpoznanie,Wywiad,Badanie,Leki")] Wizyta wizyta, string submit)
        {
            if (submit == "wywiad")
            {
                return RedirectToAction("DrukujWywiad",wizyta);
            }
            else
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
}