using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SOPeL.DAL
{
    public class SopelContext : DbContext
    {

        public SopelContext() : base("sopelLocal")
        {

        }
        // urucohmienie  Inicjalizatora
        //
        static SopelContext()
        {
            Database.SetInitializer<SopelContext>(new SopelInitializer());
        }

        public DbSet<Adres> Adresy { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }

        internal static SopelContext Create()
        {
            return new SopelContext();
        }

        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; } 
        public DbSet<Opcja> Opcje { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    modelBuilder.HasDefaultSchema("public");
        //}
    }
}