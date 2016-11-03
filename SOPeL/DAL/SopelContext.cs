using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SOPeL.DAL
{
    public class SopelContext : DbContext
    {
        public DbSet<Adres> Adresy { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }

        public SopelContext() : base("MyDb")
        {

        }
    }
}