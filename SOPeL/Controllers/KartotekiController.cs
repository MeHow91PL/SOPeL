using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SOPeL.DAL;
using SOPeL.Models;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SOPeL.Infrastructure;

namespace SOPeL.Controllers
{
    public class KartotekiController : Controller
    {
        private SopelContext db = new SopelContext();

        // GET: Kartoteki
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult PokazListePacjentow(bool WybierzPacjenta = false, string searchString = "*")
        {
            var model = PacjenciManager.SzukajPacjentow(searchString);
            ViewBag.WybierzPacjenta = WybierzPacjenta;

            return PartialView("_ListaPacjentow", model);
        }



        public PartialViewResult SzukajPacjentow(bool WybierzPacjenta = false, string searchString = "*")
        {
            var pacjenci = PacjenciManager.SzukajPacjentow(searchString, 30);
            ViewBag.WybierzPacjenta = WybierzPacjenta;

            return PartialView("_ListaPacjentowTabela", pacjenci);
        }

        /// <summary>
        /// Akcja zwracająca widok karty pacjenta do wyświtlenia w oknie.
        /// </summary>
        /// <returns></returns>
        public PartialViewResult DodajPacjenta()
        {
            Pacjent pacjent = new Pacjent();
            return PartialView("_KartaPacjenta", pacjent);
        }

        /// <summary>
        /// Akcja zapisująca dane pacjenta w bazie.
        /// </summary>
        /// <param name="pacjent"></param>
        /// <param name="adres"></param>
        /// <returns></returns>
        public ActionResult ZapiszPacjenta([Bind(Include = "Imie,Nazwisko,Pesel,DataUrodzenia,KodPocztowy,Miasto,Ulica,NrDomu,NrLokalu,Telefon,Email,Plec,Aktw,ID")] Pacjent pacjent, Adres adres)
        {
            pacjent.Adres = adres;
            Rezultat zapiszResult = PacjenciManager.ZapiszPacjenta(pacjent);

            if (zapiszResult.RezultatPozytywny == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception(zapiszResult.Komunikat);
            }




        }



        public PartialViewResult PokazListePracownikow()
        {
            var pacjenci = PracownicyManager.SzukajPracownikow("*");
            return PartialView("~/Views/Kartoteki/_ListaPracownikow.cshtml", pacjenci);
        }

        public PartialViewResult SzukajPracownikow(bool WybierzPracownika = false, string searchString = "*", bool PokazUsuniete = false)
        {
            var pracownicy = PracownicyManager.SzukajPracownikow(searchString, 20, PokazUsuniete);
            return PartialView("_ListaPracownikowTabela", pracownicy);
        }

        public PartialViewResult UsunPracownika(int Id, bool WybierzPracownika = false, bool PokazUsuniete = false)
        {
            Pracownik prac = db.Pracownicy.Find(Id);
            prac.Aktw = Enums.Aktywny.Nie;
            db.Entry(prac).State = EntityState.Modified;
            db.SaveChanges();

            IEnumerable<Pracownik> pracownicy;
            if (PokazUsuniete)
            {
                pracownicy = db.Pracownicy;
            }
            else
            {
                pracownicy = db.Pracownicy.Where(p => p.Aktw == Enums.Aktywny.Tak);
            }

            ViewBag.WybierzPracownika = WybierzPracownika;

            return PartialView("_ListaPracownikowTabela", pracownicy);
        }




        // ------------------------------------------------------- KOD WYGENEROWANY PRZEZ VISUAL ----------------------------------------------------








        // GET: Kartoteki/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacjent pacjent = db.Pacjenci.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // GET: Kartoteki/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Kartoteki/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DataUrodzenia,Plec,DokumentTozsamosci,Imie,Nazwisko,Pesel,Telefon,Email,Aktw")] Pacjent pacjent)
        {
            if (ModelState.IsValid)
            {
                db.Pacjenci.Add(pacjent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pacjent);
        }

        // GET: Kartoteki/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacjent pacjent = db.Pacjenci.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // POST: Kartoteki/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DataUrodzenia,Plec,DokumentTozsamosci,Imie,Nazwisko,Pesel,Telefon,Email,Aktw")] Pacjent pacjent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pacjent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pacjent);
        }

        // GET: Kartoteki/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pacjent pacjent = db.Pacjenci.Find(id);
            if (pacjent == null)
            {
                return HttpNotFound();
            }
            return View(pacjent);
        }

        // POST: Kartoteki/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pacjent pacjent = db.Pacjenci.Find(id);
            db.Pacjenci.Remove(pacjent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
