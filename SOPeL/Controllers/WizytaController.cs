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
using static SOPeL.Infrastructure.Enums;

namespace SOPeL.Controllers
{
    [Authorize]
    public class WizytaController : Controller
    {
        SopelContext db = new SopelContext();
        //GET: Poczekalnia
        public ActionResult Index()
        {
            var data = DateTime.Today.ToString("yyyy-MM-dd");
            var model = PrzygotujWizytaViewModel(data);

            return View(model);

        }
        public ActionResult WyswietlanieLekarzy(int? idlekarza)
        {
            if ((idlekarza == null) || (idlekarza == 0))
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r => r.Stat == Status.Rezerwacja && r.Aktw == Aktywny.Tak).ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia", model);
            }
            else
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r => r.PracownikID == idlekarza && r.Stat == Status.Rezerwacja && r.Aktw == Aktywny.Tak).ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia", model);
            }
        }

        public PartialViewResult PobierzListeRezerwacji(string data, Status status = Status.Rezerwacja, int idlekarza = 0)
        {
            var model = PrzygotujWizytaViewModel(data,status,idlekarza);
            return PartialView("WizytaPrzychodnia", model);
        }

        private WizytaViewModel PrzygotujWizytaViewModel(string data, Status status = Status.Rezerwacja, int idlekarza = 0)
        {
            var prac = db.Pracownicy.ToList();
            var wiz = db.Wizyty.ToList();
            List<Rezerwacja> rez;
            DateTime dataRez = DateTime.Parse(data);

            if ( idlekarza == 0)
            {
                rez = db.Rezerwacje.Where(r => r.DataRezerwacji == dataRez && r.Aktw == Aktywny.Tak && r.Stat == status).OrderBy(r => r.DataRezerwacji).ThenBy(r => r.godzOd).ToList();
            }
            else
            {
                rez = db.Rezerwacje.Where(r => r.DataRezerwacji == dataRez && r.PracownikID == idlekarza && r.Aktw == Aktywny.Tak && r.Stat == status).OrderBy(r => r.DataRezerwacji).ThenBy(r => r.godzOd).ToList();
            }

            return new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
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
            var icd10 = db.KodyICD10.Select(i => i.Kod + " - " + i.Nazwa).Where(i => i.StartsWith(term)).ToList();

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
            return PartialView("Wydruk-Wywiad", vm);
        }

        public ActionResult DrukujRecepte()
        {
            return PartialView("_WydrukRecepta");
        }



        public ActionResult ZapiszDodajWizyte([Bind(Include = "Id, Zalecenia,Skierowanie ,DataWizyty,DataModyfikacji,PacjentID,Pacjent,PracownikID,Pracownik,RezerwacjaId,Rozpoznanie,Wywiad,Badanie,Leki")] Wizyta wizyta, string submit)
        {
            if (submit == "wywiad")
            {
                return RedirectToAction("DrukujWywiad", wizyta);
            }
            else
            {
                db.Wizyty.Add(wizyta);
                var idrez = wizyta.RezerwacjaId;
                Rezerwacja rezerwacja = db.Rezerwacje.Find(idrez);
                rezerwacja.Stat = Status.Wykonany;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }



    }
}