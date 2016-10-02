using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PostgresObsuga;
using Npgsql;

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

        public string zapiszOpcjeTerminarza(string term_czas_wiz)
        {
            try
            {
                Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

                Database.wykonajZapytanieDML("update opcje set wartosc = " + term_czas_wiz + " where nazwa = 'term_czas_wiz'");

                Database.zamknijPolaczenie();
                return "zapisano";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int pobierzOpcjeTerminarza()
        {
            int result = 0 ;
            Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT wartosc FROM opcje WHERE nazwa = 'term_czas_wiz'");

            while (dr.Read())
            {
                result = Convert.ToInt32(dr["wartosc"]);
            }

            Database.zamknijPolaczenie();

            return result;
        }
    }
}