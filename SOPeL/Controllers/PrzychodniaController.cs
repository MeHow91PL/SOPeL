using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostgresObsuga;
using Npgsql;
using Newtonsoft.Json;

namespace SOPeL.Controllers
{
    public class PrzychodniaController : Controller
    {
        //
        // GET: /Przychodnia/
        public ActionResult Index()
        {
            //try
            //{
            //    Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            //    NpgsqlDataReader dr = Database.wykonajZapytanieDQL();

            //    Database.zamknijPolaczenie();
                
            //}
            //catch (Exception ex)
            //{ 
                
            //}

            return View();
        }

        public ActionResult wczytajModul(string wybranyModul)
        {
            string modul = wybranyModul;
            return RedirectToAction("Index", modul);
        }

       
    }
}