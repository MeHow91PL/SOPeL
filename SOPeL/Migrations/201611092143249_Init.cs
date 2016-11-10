namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.Adresy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KodPocztowy = c.String(),
                        Miasto = c.String(maxLength: 60),
                        Ulica = c.String(maxLength: 60),
                        NrDomu = c.String(maxLength: 10),
                        NrLokalu = c.String(maxLength: 10),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "public.Pacjenci",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataUrodzenia = c.DateTime(nullable: false),
                        Plec = c.String(),
                        DokumentTozsamosci = c.String(nullable: false),
                        Imie = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 60),
                        Pesel = c.String(),
                        Telefon = c.String(nullable: false, maxLength: 9),
                        Email = c.String(nullable: false, maxLength: 100),
                        Adres_Id = c.Int(),
                        AdresTymczasowy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("public.Adresy", t => t.Adres_Id)
                .ForeignKey("public.Adresy", t => t.AdresTymczasowy_Id)
                .Index(t => t.Adres_Id)
                .Index(t => t.AdresTymczasowy_Id);
            
            CreateTable(
                "public.Pracownicy",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Specjalizacja = c.String(maxLength: 100),
                        PWZ = c.String(maxLength: 8),
                        TytulNaukowy = c.String(maxLength: 15),
                        Imie = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 60),
                        Pesel = c.String(),
                        Telefon = c.String(nullable: false, maxLength: 9),
                        Email = c.String(nullable: false, maxLength: 100),
                        Adres_Id = c.Int(),
                        AdresTymczasowy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("public.Adresy", t => t.Adres_Id)
                .ForeignKey("public.Adresy", t => t.AdresTymczasowy_Id)
                .Index(t => t.Adres_Id)
                .Index(t => t.AdresTymczasowy_Id);
            
            CreateTable(
                "public.Rezerwacje",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataRezerwacji = c.DateTime(nullable: false),
                        godzOd = c.String(),
                        godzDo = c.String(),
                        DataModyfikacji = c.DateTime(nullable: false),
                        PracownikID = c.Int(nullable: false),
                        PacjentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Pacjenci", t => t.PacjentID, cascadeDelete: true)
                .ForeignKey("public.Pracownicy", t => t.PracownikID, cascadeDelete: true)
                .Index(t => t.PracownikID)
                .Index(t => t.PacjentID);
            
            CreateTable(
                "public.Uzytkowicy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Haslo = c.String(nullable: false, maxLength: 100),
                        DataUtworzenia = c.DateTime(nullable: false),
                        PracownikID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("public.Pracownicy", t => t.PracownikID)
                .Index(t => t.PracownikID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("public.Uzytkowicy", "PracownikID", "public.Pracownicy");
            DropForeignKey("public.Rezerwacje", "PracownikID", "public.Pracownicy");
            DropForeignKey("public.Rezerwacje", "PacjentID", "public.Pacjenci");
            DropForeignKey("public.Pracownicy", "AdresTymczasowy_Id", "public.Adresy");
            DropForeignKey("public.Pracownicy", "Adres_Id", "public.Adresy");
            DropForeignKey("public.Pacjenci", "AdresTymczasowy_Id", "public.Adresy");
            DropForeignKey("public.Pacjenci", "Adres_Id", "public.Adresy");
            DropIndex("public.Uzytkowicy", new[] { "PracownikID" });
            DropIndex("public.Rezerwacje", new[] { "PacjentID" });
            DropIndex("public.Rezerwacje", new[] { "PracownikID" });
            DropIndex("public.Pracownicy", new[] { "AdresTymczasowy_Id" });
            DropIndex("public.Pracownicy", new[] { "Adres_Id" });
            DropIndex("public.Pacjenci", new[] { "AdresTymczasowy_Id" });
            DropIndex("public.Pacjenci", new[] { "Adres_Id" });
            DropTable("public.Uzytkowicy");
            DropTable("public.Rezerwacje");
            DropTable("public.Pracownicy");
            DropTable("public.Pacjenci");
            DropTable("public.Adresy");
        }
    }
}
