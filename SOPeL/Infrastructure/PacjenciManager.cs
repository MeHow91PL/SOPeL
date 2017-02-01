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
        static int maxIlośćPacjentów = 10;

        public static List<Pacjent> SzukajPacjentow(string query)
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
    }
}