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
    public class PracownicyManager
    {
        /// <summary>
        /// Funcja zwraca listę pracowników zgodną z parametrami wyszukiwania
        /// </summary>
        /// <param name="query">Wyszukiwany ciąg. Może to być PESEL lub Nazwisko[separator]Imie</param>
        /// <param name="maxIlośćPacjentów">Maksymalna ilość rekordów, która ma zostać zwrócona. Domyślna warotść: 10</param>
        /// <param name="PokazUsuniete">Jeżeli true to metoda zwróci również pracowników usuniętych</param>
        /// <returns></returns>
        public static IEnumerable<Pracownik> SzukajPracownikow(string query, int maxRekordow = 10, bool PokazUsuniete = false)
        {
            SopelContext db = new SopelContext(); //tylko do tej statycznej metody
            IEnumerable<Pracownik> pracownicy = null;

            Regex pattern = new Regex(@"^[0-9]{1,11}$");

            if (string.IsNullOrWhiteSpace(query) || query == "*")
            {

                pracownicy = db.Pracownicy.Where(p => (!PokazUsuniete ? p.Aktw == Aktywny.Tak : true)).Take(maxRekordow);
            }
            else if (pattern.IsMatch(query)) // jeżeli wprowadzony ciąg jest liczbą to szukaj po peselu
            {
                pracownicy = db.Pracownicy.Where(p => p.Pesel.StartsWith(query) && (!PokazUsuniete ? p.Aktw == Aktywny.Tak : true)).Take(maxRekordow);
            }
            else
            {
                string znakPodziału = db.Opcje.Single(o => o.Nazwa == OpcjeManager.Ogólne.ZnakPodziałuImieniaINazwiska.nazwa).Wartosc;

                if (query.Contains(znakPodziału))
                {
                    int pozycjaZnakuPodzialu = query.IndexOf(znakPodziału);
                    string nazw = query.Substring(0, pozycjaZnakuPodzialu);
                    string imie = query.Substring(pozycjaZnakuPodzialu + 1); // pozycjaZnakuPodzialu + 1 to pierwsza litra wyszukiwanego imienia

                    pracownicy = db.Pracownicy.Where(
                        p => p.Nazwisko.StartsWith(nazw) && //wyszukuje nazwisko w substringu od początku do wystąpienia znaku podziału
                        p.Imie.StartsWith(imie)//wyszukuje imię w substringu od wystąpienia znaku podziału
                        && (!PokazUsuniete ? p.Aktw == Aktywny.Tak : true))
                        .Take(maxRekordow); // pobiera maxRekordów pierwszych wyników i konwertuje je do listy
                }
                else
                {
                    pracownicy = db.Pracownicy.Where(p => p.Nazwisko.StartsWith(query) && (!PokazUsuniete ? p.Aktw == Aktywny.Tak : true)).Take(maxRekordow);
                }

            }
            return pracownicy;
        }

        public static RezultatAkcji ZapiszPracownika(Pracownik pracownik)
        {
            SopelContext db = new SopelContext(); //tylko do tej statycznej metody
            try
            {
                if (db.Pracownicy.Any(p => p.ID == pracownik.ID))
                {
                    db.Entry(pracownik).State = EntityState.Modified;
                    db.Entry(pracownik.Adres).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Pracownicy.Add(pracownik);
                    db.SaveChanges();
                }
                return new RezultatAkcji(true);
            }
            catch (Exception ex)
            {
                return new RezultatAkcji(false, "Błąd przy wykonaniu metody ZapiszPacjenta.\nSzczegóły: " + ex.Message);
            }
        }
    }
}