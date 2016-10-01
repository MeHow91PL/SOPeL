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
            return View();
        }

        public ActionResult pobierzModulAdmin(string wybranyModul)
        {
            string modul = wybranyModul;
            return RedirectToAction("Index", modul);
        }

        public string zapiszOpcjeTerminarza(string term_czas_wiz)
        {
            try
            {
                Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

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
            Database.otworzPolaczenie("localhost", "postgres", "postgres", "SOPeL");

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