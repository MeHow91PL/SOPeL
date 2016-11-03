using SOPeL.DAL;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    public class PrzychodniaController : Controller
    {
        SopelContext db = new SopelContext();
        // GET: /Przychodnia/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logowanie(Uzytkownik userModel)
        {
            if (db.Uzytkownicy.Any(u => u.Login == userModel.Login) && (db.Uzytkownicy.Single(u => u.Login == userModel.Login) as Uzytkownik).Haslo == userModel.Haslo)
            {
                return RedirectToAction("Index");
            }
            else
                return View(userModel);
        }

       


    }
}