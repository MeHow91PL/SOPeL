using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
    public class UzytkownicyManager
    {
        /// <summary>
        /// Funcja zwraca listę użytkowników zgodną z parametrami wyszukiwania
        /// </summary>
        /// <param name="query">Wyszukiwany ciąg. Może to być PESEL lub Nazwisko[separator]Imie</param>
        /// <param name="maxIlośćUżytkowników">Maksymalna ilość użytkowników, która ma zostać zwrócona. Domyślna warotść: 10</param>
        /// <returns></returns>
        public static IEnumerable<IdentityUser> SzukajUzytkownikow(string query, int maxIlośćUżytkowników = 10)
        {
            SopelContext db = new SopelContext(); //tylko do tej statycznej metody
            IEnumerable<IdentityUser> uzytkownicy = null;

            if (string.IsNullOrWhiteSpace(query) || query == "*")
            {
                uzytkownicy = db.Users.Take(maxIlośćUżytkowników).ToList();
            }
            else
            {
                uzytkownicy = db.Users.Where(p => p.UserName.StartsWith(query)).Take(maxIlośćUżytkowników);
            }
            return uzytkownicy;
        }

        public static RezultatAkcji ZapiszUzytkownika(IdentityUser uzytkownik)
        {
            SopelContext db = new SopelContext(); //tylko do tej statycznej metody
            try
            {

                if (db.Users.Any(p => p.Id == uzytkownik.Id))
                {
                    db.Entry(uzytkownik).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    db.Users.Add(uzytkownik);
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