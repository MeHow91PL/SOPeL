using SOPeL.DAL;
using SOPeL.Infrastructure;
using SOPeL.Models;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        [HttpPost]
        public PartialViewResult pokazOknoRezerwacji(string dataRez, int idLek, string godzRez, bool edycjaWizyty = false)
        {
            ViewBag.edycjaWizyty = edycjaWizyty;
            var model = new Rezerwacja { DataRezerwacji = DateTime.Parse(dataRez), godzOd = godzRez, PracownikID = idLek };

            return PartialView("_KartaRezerwacjiWizyty", model);
        }

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

    }
}