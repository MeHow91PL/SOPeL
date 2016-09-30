using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostgresObsuga;

namespace SOPeL.Controllers
{
    public class PrzychodniaController : Controller
    {
        //
        // GET: /Przychodnia/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult pobierzModulAdmin(string wybranyModul)
        {
            string modul = wybranyModul;
            return RedirectToAction("Index", modul);
        }

        public bool zapiszOpcjeTerminarza()
        {
            try
            {
                Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

                Database.zamknijPolaczenie();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}