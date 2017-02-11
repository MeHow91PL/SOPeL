using Microsoft.AspNet.Identity.EntityFramework;
using SOPeL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SOPeL.DAL
{
    public class SopelContext : IdentityDbContext
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
        internal static SopelContext Create()
        {
            return new SopelContext();
        }

        public DbSet<Adres> Adresy { get; set; }
        public DbSet<Pacjent> Pacjenci { get; set; }
        public DbSet<Pracownik> Pracownicy { get; set; }
        public DbSet<Rezerwacja> Rezerwacje { get; set; }
        public DbSet<Uzytkownik> Uzytkownicy { get; set; }
        public DbSet<Opcja> Opcje { get; set; }
        public DbSet<Wizyta> Wizyty { get; set; }
        public DbSet<ICD10> KodyICD10 { get; set; }
        public DbSet<Leki> Leki { get; set; }
        public DbSet<Recepty> Recepty { get; set; }




        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        //    modelBuilder.HasDefaultSchema("public");
        //}
    }
}