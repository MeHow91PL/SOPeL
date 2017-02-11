using SOPeL.DAL;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SOPeL.Infrastructure
{
    public class PacjenciManager
    {
        /// <summary>
        /// Funcja zwraca listę pacjentów zgodną z parametrami wyszukiwania
        /// </summary>
        /// <param name="query">Wyszukiwany ciąg. Może to być PESEL lub Nazwisko[separator]Imie</param>
        /// <param name="maxIlośćPacjentów">Maksymalna ilość pacjentów, która ma zostać zwrócona. Domyślna warotść: 10</param>
        /// <returns></returns>
        public static List<Pacjent> SzukajPacjentow(string query, int maxIlośćPacjentów = 10)
        {
            SopelContext db = new SopelContext(); //tylko do tej statycznej metody
            List<Pacjent> pacjenci = null;


            Regex pattern = new Regex(@"^[0-9]{1,11}$");

            if (string.IsNullOrWhiteSpace(query) || query == "*")
            {
                pacjenci = db.Pacjenci.Take(maxIlośćPacjentów).ToList();
            }
            else if (pattern.IsMatch(query)) // jeżeli wprowadzony ciąg jest liczbą to szukaj po peselu
            {
                pacjenci = db.Pacjenci.Where(p => p.Pesel.StartsWith(query)).Take(maxIlośćPacjentów).ToList();
            }
            else
            {

                string znakPodziału = db.Opcje.Single(o => o.Nazwa == OpcjeManager.Ogólne.ZnakPodziałuImieniaINazwiska.nazwa).Wartosc;

                if (query.Contains(znakPodziału))
                {
                    int pozycjaZnakuPodzialu = query.IndexOf(znakPodziału);
                    string nazw = query.Substring(0, pozycjaZnakuPodzialu);
                    string imie = query.Substring(pozycjaZnakuPodzialu + 1); // pozycjaZnakuPodzialu + 1 to pierwsza litra wyszukiwanego imienia

                    pacjenci = db.Pacjenci.Where(
                        p => p.Nazwisko.StartsWith(nazw) && //wyszukuje nazwisko w substringu od początku do wystąpienia znaku podziału
                        p.Imie.StartsWith(imie))//wyszukuje imię w substringu od wystąpienia znaku podziału
                        .Take(maxIlośćPacjentów).ToList(); // pobiera 20 pierwszych wyników i konwertuje je do listy
                }
                else
                {
                    pacjenci = db.Pacjenci.Where(p => p.Nazwisko.StartsWith(query)).Take(maxIlośćPacjentów).ToList();
                }

            }

            return pacjenci;
        }

        public static RezultatAkcji ZapiszPacjenta(Pacjent pacjent)
        {
            SopelContext db = new SopelContext(); //tylko do tej statycznej metody
            try
            {
                if (string.IsNullOrWhiteSpace(pacjent.Imie))
                {
                    throw new BrakImienia("Nie podano imienia pacjenta");
                }
                if (string.IsNullOrWhiteSpace(pacjent.Nazwisko))
                {
                    throw new BrakNazwiska("Nie podano nazwiska pacjenta");
                }

                if (db.Pacjenci.Any(p => p.ID == pacjent.ID))// Edycja pacjenta
                {
                    db.Entry(pacjent).State = EntityState.Modified;
                    db.Entry(pacjent.Adres).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else //Nowy pacjent
                {
                    if (!string.IsNullOrEmpty(pacjent.Pesel) && db.Pacjenci.Any(p => p.Pesel == pacjent.Pesel))
                    {
                        throw new Exception("Pacjent o podanych numerze pesel już istnieje!");
                    }
                    else
                    {
                        db.Pacjenci.Add(pacjent);
                        db.SaveChanges();
                    }
                }
                return new RezultatAkcji(true, "Pacjent zapisany poprawnie");
            }
            catch (BrakImienia ex)
            {
                return new RezultatAkcji(false, ex.Message);
            }
            catch (BrakNazwiska ex)
            {
                return new RezultatAkcji(false, ex.Message);
            }
            catch (Exception ex)
            {
                return new RezultatAkcji(false, "Pacjent nie został zapisany.\nSzczegóły: " + ex.Message);
            }
        }

    }
}