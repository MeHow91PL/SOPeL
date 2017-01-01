using SOPeL.DAL;
using SOPeL.Infrastructure;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        SopelContext db = new SopelContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public PartialViewResult PokazOpcjeGlowne()
        {
            OpcjeGlowneViewModel model = new OpcjeGlowneViewModel() { ogol_podz_imie_nazw =  db.Opcje.Single(o => o.Nazwa == "ogol_podz_imie_nazw").Wartosc};
            return PartialView("_OknoOpcjiGlownych", model);

        }

        [HttpPost]
        public ActionResult ZapiszOpcjeGlowne(OpcjeGlowneViewModel model)
        {
            var opcja = db.Opcje.Single(o => o.Nazwa == "ogol_podz_imie_nazw");
            opcja.Wartosc = model.ogol_podz_imie_nazw;
            db.Entry(opcja).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}