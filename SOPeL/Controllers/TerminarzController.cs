using SOPeL.DAL;
using SOPeL.Infrastructure;
using SOPeL.Models;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static SOPeL.Infrastructure.Enums;

namespace SOPeL.Controllers
{
    [Authorize]

    [System.Runtime.InteropServices.Guid("932276C1-039A-41C2-81C8-C84BD5147EA9")]
    public class TerminarzController : Controller
    {
        SopelContext db = new SopelContext();

        // GET: Rejestracja
        public ActionResult Index()
        {
            var model = pobierzTerminarzViewModel(DateTime.Today.ToString("yyyy-MM-dd"));

            ViewBag.GodzOd = model.opcje.Single(o => o.Nazwa == "term_godz_od").Wartosc;
            ViewBag.GodzDo = model.opcje.Single(o => o.Nazwa == "term_godz_do").Wartosc;
            ViewBag.CzasWiz = model.opcje.Single(o => o.Nazwa == "term_czas_wiz").Wartosc;

            return View(model);
        }

        public ActionResult pobierzTerminarz(string wybranaData)
        {
            var model = pobierzTerminarzViewModel(wybranaData);

            ViewBag.GodzOd = model.opcje.Single(o => o.Nazwa == "term_godz_od").Wartosc;
            ViewBag.GodzDo = model.opcje.Single(o => o.Nazwa == "term_godz_do").Wartosc;
            ViewBag.CzasWiz = model.opcje.Single(o => o.Nazwa == "term_czas_wiz").Wartosc;

            return View("SiatkaTerminarza", model);
        }

        private TerminarzViewModel pobierzTerminarzViewModel(string wybranaData = null, int pracownikId = 0)
        {
            List<Opcja> opcje = null;
            List<Pracownik> prac = null;
            List<Rezerwacja> rez = null;

            if (wybranaData != null)
            {
                wybranaData = String.Format("{0:yyyy-MM-dd}", wybranaData);
                DateTime data = DateTime.Parse(wybranaData);
                rez = db.Rezerwacje.Where(r => r.DataRezerwacji == data && r.Aktw == Aktywny.Tak).ToList();
            }
            else rez = db.Rezerwacje.ToList();

            if (pracownikId > 0) prac = db.Pracownicy.Where(p => p.ID == pracownikId).ToList();
            else prac = db.Pracownicy.ToList();

            opcje = db.Opcje.ToList();
            var model = new TerminarzViewModel { opcje = opcje, pracownicy = prac, rezerwacje = rez };

            return model;
        }



        //--------------------------------- Opcje terminarza -----------------------------------------------------------------------
        public JsonResult PobierzOpcjeTerminarza()
        {
            var opcjeTemrminarza = db.Opcje.Where(o => o.Nazwa.StartsWith("term")).ToDictionary(o => o.Nazwa);


            return Json(opcjeTemrminarza);
        }

        [HttpPost]
        public string ZapiszOpcjeTerminarza(Dictionary<string, string> opcj)
        {
            try
            {
                foreach (var opcja in opcj)
                {
                    var opcjaDb = db.Opcje.Single(o => o.Nazwa == opcja.Key);
                    opcjaDb.Wartosc = opcja.Value;
                    db.Entry(opcjaDb).State = EntityState.Modified;
                }
                db.SaveChanges();

                return "zapisano";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        //--------------------------------- Rezerwacja wizyt -----------------------------------------------------------------------
        [HttpPost]
        public PartialViewResult pokazOknoRezerwacji(string dataRez, int idLek, string godzRez, bool edycjaWizyty = false)
        {
            ViewBag.edycjaWizyty = edycjaWizyty;
            var model = new Rezerwacja { DataRezerwacji = DateTime.Parse(dataRez), godzOd = godzRez, PracownikID = idLek };

            return PartialView("_KartaRezerwacjiWizyty", model);
        }


        public JsonResult PacjentAutocomplete(string Prefix)
        {
            try
            {
                List<Pacjent> pacjenci = PacjenciManager.SzukajPacjentow(Prefix);
                return Json(pacjenci, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public JsonResult ZapiszRezerwacje(Rezerwacja model)
        {
            Pacjent pac = db.Pacjenci.Find(model.PacjentID);
            Pracownik prac = db.Pracownicy.Find(model.PracownikID);
            try
            {
                if (db.Rezerwacje.Any(r => r.PacjentID == model.PacjentID && r.DataRezerwacji == model.DataRezerwacji && r.Stat == Status.Rezerwacja && r.Aktw == Aktywny.Tak))
                {
                    throw new Exception("Dnia " + model.DataRezerwacji.ToShortDateString() + " istnieje już inna rezerwacja dla pacjenta " + pac.Imie + " " + pac.Nazwisko);
                }

                if (model.Id == 0) //Model == 0 dla nowej rezeracji
                {

                    if (db.Rezerwacje.Any(r => r.DataRezerwacji == model.DataRezerwacji && r.godzOd == model.godzOd && r.Stat == Status.Rezerwacja && r.Aktw == Aktywny.Tak))
                    {
                        throw new Exception("Istnieje już inna rezerwacja w podanym terminie");
                    }

                    db.Rezerwacje.Add(new Rezerwacja
                    {
                        DataRezerwacji = model.DataRezerwacji,
                        godzOd = model.godzOd,
                        PacjentID = model.PacjentID,
                        PracownikID = model.PracownikID,
                        DataModyfikacji = DateTime.Now,
                        Stat = model.Stat
                    });
                }
                else // Gdy rezerwacja jest edytowana
                {
                    Rezerwacja rez = db.Rezerwacje.Find(model.Id);
                    rez.Pacjent = db.Pacjenci.Find(model.PacjentID);
                    rez.Pracownik = db.Pracownicy.Find(model.PracownikID);
                    rez.DataModyfikacji = DateTime.Now;
                    rez.Stat = model.Stat;
                    rez.Pacjent = db.Pacjenci.Find(model.PacjentID);
                    db.Entry(rez).State = EntityState.Modified;
                }

                db.SaveChanges();

                return Json(new AjaxResult() { ZakończonoPomyślnie = true, Komunikat = "Rezerwacja zapisana pomyślnie" });
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult() { ZakończonoPomyślnie = false, Komunikat = ex.Message });
            }

        }

        public PartialViewResult EdytujRezerwacje(int idRez)
        {
            try
            {
                Rezerwacja rez = db.Rezerwacje.Find(idRez);
                ViewBag.PacjentValue = rez.Pacjent.Pesel + " | " + rez.Pacjent.Imie + " " + rez.Pacjent.Nazwisko;
                return PartialView(Ścieżki.KartaRezerwacjiWizyty, rez);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UsunRezerwacje(int idRez)
        {
            try
            {
                Rezerwacja rez = db.Rezerwacje.Find(idRez);
                rez.Aktw = Aktywny.Nie;
                db.Entry(rez).State = EntityState.Modified;

                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}