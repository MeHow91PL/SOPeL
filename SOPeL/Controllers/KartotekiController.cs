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

namespace SOPeL.Controllers
{
    public class KartotekiController : Controller
    {
        private SopelContext db = new SopelContext();

        // GET: Kartoteki
        public ActionResult Index()
        {
            return View(db.Pacjenci.ToList());
        }

        public PartialViewResult PokazListePacjentow()
        {
            var model = db.Pacjenci.ToList();

            return PartialView("_ListaPacjentow", model);
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
