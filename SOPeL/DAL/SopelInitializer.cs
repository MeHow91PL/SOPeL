using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SOPeL.Models;
using System.Data.Entity.Migrations;
using SOPeL.Migrations;

namespace SOPeL.DAL
{
    public class SopelInitializer: MigrateDatabaseToLatestVersion<SopelContext, Configuration>
    {
        //zostało to przenesione do configuration.cs w migracjach
        //protected override void Seed(SopelContext context)
        //{
            
        //        SeedsopelLocal(context);
        //        base.Seed(context);

                     
        //}

        public static void SeedsopelLocal(SopelContext context)
        {
            var uzytkownicy = new List<Uzytkownik>
            {
                new Uzytkownik() {Login="Ja", Haslo="Ja" },
                new Uzytkownik() {Login="admin", Haslo="admin" }
            };
            uzytkownicy.ForEach(s => context.Uzytkownicy.AddOrUpdate(s));
            context.SaveChanges();


            

            var pracownicy = new List<Pracownik>
            {
                new Pracownik() { Imie="Stephen", Nazwisko="Strange", Pesel="86062905358", Telefon="666555444", Email="ss@marvel.pl", PWZ="1234565", Specjalizacja="Chirurgia", TytulNaukowy="dr"},
                new Pracownik() { Imie="Michaela", Nazwisko="Quinn", Pesel="86062905358", Telefon="123456789", Email="dr@quinn.pl", PWZ="7654323", Specjalizacja="Internista", TytulNaukowy="dr"}

            };
            pracownicy.ForEach(z => context.Pracownicy.AddOrUpdate(z));
            context.SaveChanges();


            // nie wiem cmu nie dziala jakis blad walidacji jest
            //var pacjenci = new List<Pacjent>
            //{
            //    new Pacjent() { Imie="Jan", Nazwisko="Kowalski", Pesel="86062905358", Telefon="666555444", Email="kowalski@wp.pl"}

            //};
            //pacjenci.ForEach(g => context.Pacjenci.Add(g));
            //context.SaveChanges();

        }
    }
}


