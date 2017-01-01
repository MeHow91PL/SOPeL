﻿using SOPeL.DAL;
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

                DateTime data = new DateTime(
                    Convert.ToInt32(wybranaData.Substring(0, 4)),
                    Convert.ToInt32(wybranaData.Substring(5, 2)),
                    Convert.ToInt32(wybranaData.Substring(8, 2))
                    );

                rez = db.Rezerwacje.Where(r => r.DataRezerwacji == data).ToList();
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


        public async Task<JsonResult> PacjentAutocomplete(string Prefix)
        {
            List<Pacjent> pacjenci = null;
            Regex pattern = new Regex(@"^[0-9]{1,11}$");

            if (pattern.IsMatch(Prefix)) // jeżeli wprowadzony ciąg jest liczbą to szukaj po peselu
            {
                pacjenci = await db.Pacjenci.Where(p => p.Pesel.StartsWith(Prefix)).Take(20).ToListAsync();
            }
            else
            {

                string znakPodziału = db.Opcje.Single(o => o.Nazwa == OpcjeManager.Ogólne.ZnakPodziałuImieniaINazwiska.nazwa).Wartosc;

                if (Prefix.Contains(znakPodziału))
                {
                    int pozycjaZnakuPodzialu = Prefix.IndexOf(znakPodziału);
                    string nazw = Prefix.Substring(0, pozycjaZnakuPodzialu);
                    string imie = Prefix.Substring(pozycjaZnakuPodzialu + 1); // pozycjaZnakuPodzialu + 1 to pierwsza litra wyszukiwanego imienia

                    pacjenci = await db.Pacjenci.Where(
                        p => p.Nazwisko.StartsWith(nazw) && //wyszukuje nazwisko w substringu od początku do wystąpienia znaku podziału
                        p.Imie.StartsWith(imie))//wyszukuje imię w substringu od wystąpienia znaku podziału
                        .Take(20).ToListAsync(); // pobiera 20 pierwszych wyników i konwertuje je do listy
                }
                else
                {
                    pacjenci = await db.Pacjenci.Where(p => p.Nazwisko.StartsWith(Prefix)).Take(20).ToListAsync();
                }

            }

            return Json(pacjenci, JsonRequestBehavior.AllowGet);
        }

        public bool ZapiszRezerwacje(Rezerwacja model)
        {
            try
            {
                db.Rezerwacje.Add(new Rezerwacja
                {
                    DataRezerwacji = model.DataRezerwacji,
                    godzOd = model.godzOd,
                    PacjentID = model.PacjentID,
                    PracownikID = model.PracownikID,
                    DataModyfikacji = DateTime.Now,
                    Stat = model.Stat
                });

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