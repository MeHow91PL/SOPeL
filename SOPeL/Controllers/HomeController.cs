using SOPeL.DAL;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SOPeL.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private SopelContext db = new SopelContext();

        public ActionResult Index()
        {

            //funckja tylko po to by zalaozcy baze przy urucghomieniu za pomoca inicjalizera
            //var uzytkownicyList = db.Uzytkownicy.ToList();
            return View();
        }
    }
}