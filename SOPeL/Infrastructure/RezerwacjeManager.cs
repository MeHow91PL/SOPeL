using SOPeL.DAL;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOPeL.Infrastructure
{
    public abstract class RezerwacjeManager
    {
        public static RezultatAkcji ZapiszRezerwacje(Rezerwacja rezerwacja)
        {
            SopelContext db = new SopelContext();

            Pacjent pac = db.Pacjenci.Find(rezerwacja.PacjentID);
            Pracownik prac = db.Pracownicy.Find(rezerwacja.PracownikID);
            try
            {
                if (db.Rezerwacje.Any(r => r.PacjentID == rezerwacja.PacjentID && r.DataRezerwacji == rezerwacja.DataRezerwacji && r.Stat == Status.Rezerwacja && r.Aktw == Aktywny.Tak))
                {
                    throw new Exception("Dnia " + rezerwacja.DataRezerwacji.ToShortDateString() + " istnieje już inna rezerwacja dla pacjenta " + pac.Imie + " " + pac.Nazwisko);
                }

                if (rezerwacja.Id == 0) //Model == 0 dla nowej rezeracji
                {

                    if (db.Rezerwacje.Any(r => r.DataRezerwacji == rezerwacja.DataRezerwacji && r.godzOd == rezerwacja.godzOd && r.Stat == Status.Rezerwacja && r.Aktw == Aktywny.Tak))
                    {
                        throw new Exception("Istnieje już inna rezerwacja w podanym terminie");
                    }

                    db.Rezerwacje.Add(new Rezerwacja
                    {
                        DataRezerwacji = rezerwacja.DataRezerwacji,
                        godzOd = rezerwacja.godzOd,
                        PacjentID = rezerwacja.PacjentID,
                        PracownikID = rezerwacja.PracownikID,
                        DataModyfikacji = DateTime.Now,
                        Stat = rezerwacja.Stat
                    });
                }
                else // Gdy rezerwacja jest edytowana
                {
                    Rezerwacja rez = db.Rezerwacje.Find(rezerwacja.Id);
                    rez.Pacjent = db.Pacjenci.Find(rezerwacja.PacjentID);
                    rez.Pracownik = db.Pracownicy.Find(rezerwacja.PracownikID);
                    rez.DataModyfikacji = DateTime.Now;
                    rez.Stat = rezerwacja.Stat;
                    rez.Pacjent = db.Pacjenci.Find(rezerwacja.PacjentID);
                    db.Entry(rez).State = EntityState.Modified;
                }

                db.SaveChanges();

                return new RezultatAkcji(true, "Rezerwacja zapisana pomyślnie");
            }
            catch (Exception ex)
            {
                return new RezultatAkcji(false, ex.Message);
            }
        }

        public static RezultatAkcji UsunRezerwacje(int idRez)
        {
            SopelContext db = new SopelContext();

            try
            {
                Rezerwacja rez = db.Rezerwacje.Find(idRez);
                rez.Aktw = Aktywny.Nie;
                db.Entry(rez).State = EntityState.Modified;

                db.SaveChanges();

                return new RezultatAkcji(true, "Rezerwacja usunięta pomyślnie");
            }
            catch (Exception ex)
            {
                return new RezultatAkcji(false, ex.Message);
            }
        }
    }
}