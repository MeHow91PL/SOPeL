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
    public class WizytaController : Controller
    {
        SopelContext db = new SopelContext();
        //GET: Poczekalnia
        public ActionResult Index()
        {
            //var yesterDay = DateTime.Now.AddDays(-1);
            //var yesterDayshort = yesterDay.ToShortDateString();
            //var nextDay = DateTime.Now.AddDays(+1);
            //var toDay = DateTime.Now.ToShortDateString();

            //var rez = (from n in db.Rezerwacje.OrderByDescending(n => n.DataRezerwacji) where  (n.DataRezerwacji.Date() = yesterDayshort ) /*&& (n.DataRezerwacji < nextDay)*/ select n).ToList();

            //var dzis = DateTime.Now.ToString("yyyy-MM-dd");
            //var rez = db.Rezerwacje.Where(r => r.DataRezerwacji.ToString() == dzis).ToList();



            var prac = db.Pracownicy.ToList();
            var wiz = db.Wizyty.ToList();
            var rez = db.Rezerwacje.Where(r => r.Stat == "R").ToList();
            var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
            return View(model);

        }
        public ActionResult WyswietlanieLekarzy(int? idlekarza)
        {
          


            if ((idlekarza == null)|| (idlekarza==0))
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r=>r.Stat=="R").ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia", model);
               
            }
            else
            {
                var prac = db.Pracownicy.ToList();
                var wiz = db.Wizyty.ToList();
                var rez = db.Rezerwacje.Where(r => r.PracownikID == idlekarza && r.Stat=="R").ToList();
                var model = new WizytaViewModel { pracownicy = prac, rezerwacje = rez, wizyty = wiz };
                return PartialView("WizytaPrzychodnia",model);
                
            }
        }




        //internal List<Pracownik> GetListaPracownikow(string zapytanie)
        //{
        //    List<Pracownik> pracownicy = new List<Pracownik>();

        //    //NpgsqlDataReader dr = Database.wykonajZapytanieDQL(zapytanie);

        //    //while (dr.Read())
        //    //{
        //    //    pracownicy.Add(new Pracownik() { Id = (int)dr["id"], Imie = dr["imie"].ToString(), Nazwisko = dr["nazwisko"].ToString(), Specjalizacja = dr["specjalizacja"].ToString() });
        //    //}

        //    //dr.Close();

        //    return pracownicy;
        //}



        //public PartialViewResult pobierzRezerwacje(string pracID)
        //{
        //    List<Rezerwacja> rezerwacje = new List<Rezerwacja>();

        //    //Database.otworzPolaczenie("serwer1518407.home.pl", "18292517_0000002", "Sopel2016", "18292517_0000002");

        //    //NpgsqlDataReader dr = Database.wykonajZapytanieDQL("Select * from v_rezerwacjePacjentow where rez_data=current_date and prac_id = " + pracID);

        //    //while (dr.Read())
        //    //{
        //    //   rezerwacje.Add(new Rezerwacja()
        //    //    {
        //    //        Pacjent = new Pacjent()
        //    //        {
        //    //            Id = Convert.ToInt32(dr["pac_id"].ToString()),
        //    //            Imie = dr["pac_imie"].ToString(),
        //    //            Nazwisko = dr["pac_nazwisko"].ToString(),
        //    //            Pesel = dr["pac_pesel"].ToString()
        //    //        },
        //    //        godzOd = dr["rez_godz_pocz"].ToString()
        //    //    }
        //    //        );
        //    //}

        //    //dr.Close();

        //    //Database.zamknijPolaczenie();


        //    //ViewBag.Rezerwacje = rezerwacje;

        //    return PartialView("PoczekalniaPrzychodnia");
        //}


       

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
            var idrez = wizyta.RezerwacjaId;
            Rezerwacja rezerwacja = db.Rezerwacje.Find(idrez);
            rezerwacja.Stat = "W";
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}