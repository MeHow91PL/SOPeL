using SOPeL.DAL;
using SOPeL.Infrastructure;
using SOPeL.Models;
using SOPeL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SOPeL.Controllers
{
    public class WizytaController : PrzychodniaMasterController
    {
        SopelContext db = new SopelContext();
        // GET: Poczekalnia
        public ActionResult Index()
        {
            var prac = db.Pracownicy.ToList();
            var rez = db.Rezerwacje.ToList();
            var wiz = db.Wizyty.ToList();

            var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty=wiz };
            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");
            //ViewBag.Pracownicy = GetListaPracownikow("Select * from pracownicy");
            //ViewBag.Rezerwacje = new List<Rezerwacja>();
            //Database.zamknijPolaczenie();
            return View("~/Views/Przychodnia/Poczekalnia/Index.cshtml", model);
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



        public PartialViewResult pobierzRezerwacje(string pracID)
        {
            List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

            //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

            //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select * from v_rezerwacjePacjentow where rez_data=current_date and prac_id = " + pracID);

            //while (dr.Read())
            //{
            //   rezerwacje.Add(new Rezerwacja()
            //    {
            //        Pacjent = new Pacjent()
            //        {
            //            Id = Convert.ToInt32(dr["pac_id"].ToString()),
            //            Imie = dr["pac_imie"].ToString(),
            //            Nazwisko = dr["pac_nazwisko"].ToString(),
            //            Pesel = dr["pac_pesel"].ToString()
            //        },
            //        godzOd = dr["rez_godz_pocz"].ToString()
            //    }
            //        );
            //}

            //dr.Close();

            //Database.zamknijPolaczenie();


            //ViewBag.Rezerwacje = rezerwacje;

            return PartialView("~/Views/Przychodnia/Poczekalnia/PoczekalniaPrzychodnia.cshtml");
        }


       

        public PartialViewResult dodajWizyte(int idrez)
        {

            
            Wizyta wizyta = new Wizyta();
            Rezerwacja rezerwacja = db.Rezerwacje.Find(idrez);
            wizyta.Pacjent = rezerwacja.Pacjent;
            wizyta.RezerwacjaId = rezerwacja.Id;
            wizyta.DataWizyty = DateTime.Now;
            wizyta.DataModyfikacji = DateTime.Now;
            wizyta.PacjentID = rezerwacja.PacjentID;
            wizyta.PracownikID = rezerwacja.PracownikID;



            return PartialView("_KartaWizyty", wizyta);
        }


        public ActionResult ZapiszDodajWizyte([Bind(Include = "Id,DataWizyty,DataModyfikacji,PacjentID,PracownikID,RezerwacjaId,Rozpoznanie,Wywiad,Badanie")] Wizyta wizyta)
        {
            db.Wizyty.Add(wizyta);

            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}