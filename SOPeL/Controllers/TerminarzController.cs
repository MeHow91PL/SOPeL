using SOPeL.DAL;
using SOPeL.Models;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SOPeL.Controllers
{
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

            return View("~/Views/Przychodnia/Terminarz/Index.cshtml", model);
        }

        public ActionResult pobierzTerminarz(string wybranaData)
        {
            var model = pobierzTerminarzViewModel(wybranaData);

            ViewBag.GodzOd = model.opcje.Single(o => o.Nazwa == "term_godz_od").Wartosc;
            ViewBag.GodzDo = model.opcje.Single(o => o.Nazwa == "term_godz_do").Wartosc;
            ViewBag.CzasWiz = model.opcje.Single(o => o.Nazwa == "term_czas_wiz").Wartosc;

            return View("~/Views/Przychodnia/Terminarz/SiatkaTerminarza.cshtml",model);
        }

        private TerminarzViewModel pobierzTerminarzViewModel(string wybranaData)
        {
            wybranaData = String.Format("{0:yyyy-MM-dd}", wybranaData);

            DateTime data = new DateTime(
                Convert.ToInt32(wybranaData.Substring(0, 4)),
                Convert.ToInt32(wybranaData.Substring(5, 2)),
                Convert.ToInt32(wybranaData.Substring(8, 2))
                );

            var opcje = db.Opcje.ToList();
            var prac = db.Pracownicy.ToList();
            var rez = db.Rezerwacje.Where(r => r.DataRezerwacji == data).ToList();

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

        private dynamic PobierzOpcje()
        {
            Dictionary<string, string> opcje = new Dictionary<string, string>();

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select nazwa, wartosc from opcje");

            //while (dr.Read())
            //{
            //    opcje.Add(dr["nazwa"].ToString(), dr["wartosc"].ToString());
            //}

            //dr.Close();

            return opcje;
        }

        public ActionResult Admin()
        {
            return View("~/Views/Przychodnia/Admin/Index.cshtml");
        }

        private List<Rezerwacja> GetListaRezerwacji(string zapytanie)
        {
            List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            //while (dr.Read())
            //{
            //    rezerwacje.Add(new Rezerwacja()
            //    {
            //        Pacjent = new Pacjent() { Imie = dr["pac_imie"].ToString(), Nazwisko = dr["pac_nazwisko"].ToString(), Pesel = dr["pac_pesel"].ToString() },
            //        Pracownik = new Pracownik() { Imie = dr["prac_imie"].ToString(), Nazwisko = dr["prac_nazwisko"].ToString() },
            //        DataRezerwacji = Convert.ToDateTime(dr["rez_data"].ToString()),
            //        godzOd = dr["rez_godz_pocz"].ToString(),
            //        godzDo = dr["rez_godz_konc"].ToString()
            //    });
            //}

            //dr.Close();

            return rezerwacje;
        }



        internal List<Pracownik> GetListaPracownikow(string zapytanie)
        {
            List<Pracownik> pracownicy = new List<Pracownik>();

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

            //while (dr.Read())
            //{
            //    pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
            //}

            //dr.Close();

            return pracownicy;
        }



        [HttpPost]
        public ActionResult zatwierdzenieRezerwacji(Rezerwacja rez, string submitButton)
        {
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            //if(submitButton == "Usuń")
            //Database.wykonajZapytanieDML("DELETE FROM rezerwacje WHERE id=" + rez.Id);
            ////else zapisz

            //Database.zamknijPolaczenie();

            return RedirectToAction("Index");
        }



        public string zapiszOpcjeTerminarza(Dictionary<string,string> opcj)
        {
            try
            {
                //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

                //foreach (var opcja in opcj)
                //{
                //    Database.wykonajZapytanieDML("update opcje set wartosc = '" + opcja.Value + "' where nazwa = '" + opcja.Key + "'");
                //}

                //Database.zamknijPolaczenie();
                return "zapisano";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public JsonResult pobierzOpcjeTerminarza()
        {
            Dictionary<string, object> opcje = new Dictionary<string, object>();
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("SELECT nazwa, wartosc FROM opcje WHERE nazwa like 'term_%'");

            //while (dr.Read())
            //{
            //    opcje.Add(dr["nazwa"].ToString(), dr["wartosc"]);
            //}

            //Database.zamknijPolaczenie();

            var test1 = Json(opcje);



            return test1;
        }

    }
}