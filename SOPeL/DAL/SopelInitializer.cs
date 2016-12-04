using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SOPeL.Models;
using System.Data.Entity.Migrations;

namespace SOPeL.DAL
{
    //public class SopelInitializer: MigrateDatabaseToLatestVersion<SopelContext, Configuration>
    //{
    //    //zostało to przenesione do configuration.cs w migracjach
    //    //protected override void Seed(SopelContext context)
    //    //{
            
    //    //        SeedsopelLocal(context);
    //    //        base.Seed(context);

                     
    //    //}

    //    public static void SeedsopelLocal(SopelContext context)
    //    {
    //        var uzytkownicy = new List<Uzytkownik>
    //        {
    //            new Uzytkownik() {Login="Ja", Haslo="Ja", Id=1 },
    //            new Uzytkownik() {Login="admin", Haslo="admin", Id=2                }
    //        };
    //        uzytkownicy.ForEach(s => context.Uzytkownicy.AddOrUpdate(s));
    //        context.SaveChanges();


    //        var opcje = new List<Opcja>
    //        {
    //           new Opcja() { Nazwa="term_godz_od", ID=1, Wartosc="08:00"},
    //           new Opcja() { Nazwa="term_godz_do", ID=2, Wartosc="16:00"},
    //           new Opcja() { Nazwa="term_czas_wiz", ID=3, Wartosc="10"}
    //        };
    //        opcje.ForEach(s => context.Opcje.AddOrUpdate(s));
    //        context.SaveChanges();

    //        var pracownicy = new List<Pracownik>
    //        {
    //            new Pracownik() { ID=1, Imie="Stephen", Nazwisko="Strange", Pesel="86062905358", Telefon="666555444", Email="ss@marvel.pl", PWZ="1234565", Specjalizacja="Chirurgia", TytulNaukowy="dr"},
    //            new Pracownik() {ID=2, Imie="Michaela", Nazwisko="Quinn", Pesel="86062905358", Telefon="123456789", Email="dr@quinn.pl", PWZ="7654323", Specjalizacja="Internista", TytulNaukowy="dr"}

    //        };
    //        pracownicy.ForEach(z => context.Pracownicy.AddOrUpdate(z));
    //        context.SaveChanges();


    //        //nie wiem cmu nie dziala jakis blad walidacji jest
    //       var pacjenci = new List<Pacjent>
    //       {
    //            new Pacjent() { ID=1, Imie="Jan", Nazwisko="Kowalski", Pesel="86062905358", Telefon="666555444", Email="kowalski@wp.pl", Aktw="T"}

    //       };
    //        pacjenci.ForEach(g => context.Pacjenci.AddOrUpdate(g));
    //        context.SaveChanges();

    //    }
    //}
}


