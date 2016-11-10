using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SOPeL.Models;

namespace SOPeL.DAL
{
    public class SopelInitializer: DropCreateDatabaseIfModelChanges<SopelContext>
    {
        protected override void Seed(SopelContext context)
        {
            var uzytkownicy = new List<Uzytkownik>
            {
                new Uzytkownik {Login="Ja", Haslo="Ja" }
            };

            uzytkownicy.ForEach(s => context.Uzytkownicy.Add(s));
            context.SaveChanges();
        }
    }
}