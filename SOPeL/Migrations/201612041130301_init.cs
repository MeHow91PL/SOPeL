namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Adresy",
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
                "dbo.Opcje",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nazwa = c.String(),
                        Typ = c.Int(nullable: false),
                        Wartosc = c.String(),
                        Ostatnia_modyfikacja = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Pacjenci",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DataUrodzenia = c.DateTime(),
                        Plec = c.String(),
                        DokumentTozsamosci = c.String(),
                        Imie = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 60),
                        Pesel = c.String(),
                        Telefon = c.String(maxLength: 9),
                        Email = c.String(maxLength: 100),
                        Aktw = c.String(maxLength: 1),
                        Adres_Id = c.Int(),
                        AdresTymczasowy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adresy", t => t.Adres_Id)
                .ForeignKey("dbo.Adresy", t => t.AdresTymczasowy_Id)
                .Index(t => t.Adres_Id)
                .Index(t => t.AdresTymczasowy_Id);
            
            CreateTable(
                "dbo.Pracownicy",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Specjalizacja = c.String(maxLength: 100),
                        PWZ = c.String(maxLength: 8),
                        TytulNaukowy = c.String(maxLength: 15),
                        Imie = c.String(nullable: false, maxLength: 50),
                        Nazwisko = c.String(nullable: false, maxLength: 60),
                        Pesel = c.String(),
                        Telefon = c.String(maxLength: 9),
                        Email = c.String(maxLength: 100),
                        Aktw = c.String(maxLength: 1),
                        Adres_Id = c.Int(),
                        AdresTymczasowy_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Adresy", t => t.Adres_Id)
                .ForeignKey("dbo.Adresy", t => t.AdresTymczasowy_Id)
                .Index(t => t.Adres_Id)
                .Index(t => t.AdresTymczasowy_Id);
            
            CreateTable(
                "dbo.Rezerwacje",
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
                .ForeignKey("dbo.Pacjenci", t => t.PacjentID, cascadeDelete: true)
                .ForeignKey("dbo.Pracownicy", t => t.PracownikID, cascadeDelete: true)
                .Index(t => t.PracownikID)
                .Index(t => t.PacjentID);
            
            CreateTable(
                "dbo.Uzytkowicy",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 50),
                        Haslo = c.String(nullable: false, maxLength: 100),
                        DataUtworzenia = c.DateTime(),
                        PracownikID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pracownicy", t => t.PracownikID)
                .Index(t => t.PracownikID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uzytkowicy", "PracownikID", "dbo.Pracownicy");
            DropForeignKey("dbo.Rezerwacje", "PracownikID", "dbo.Pracownicy");
            DropForeignKey("dbo.Rezerwacje", "PacjentID", "dbo.Pacjenci");
            DropForeignKey("dbo.Pracownicy", "AdresTymczasowy_Id", "dbo.Adresy");
            DropForeignKey("dbo.Pracownicy", "Adres_Id", "dbo.Adresy");
            DropForeignKey("dbo.Pacjenci", "AdresTymczasowy_Id", "dbo.Adresy");
            DropForeignKey("dbo.Pacjenci", "Adres_Id", "dbo.Adresy");
            DropIndex("dbo.Uzytkowicy", new[] { "PracownikID" });
            DropIndex("dbo.Rezerwacje", new[] { "PacjentID" });
            DropIndex("dbo.Rezerwacje", new[] { "PracownikID" });
            DropIndex("dbo.Pracownicy", new[] { "AdresTymczasowy_Id" });
            DropIndex("dbo.Pracownicy", new[] { "Adres_Id" });
            DropIndex("dbo.Pacjenci", new[] { "AdresTymczasowy_Id" });
            DropIndex("dbo.Pacjenci", new[] { "Adres_Id" });
            DropTable("dbo.Uzytkowicy");
            DropTable("dbo.Rezerwacje");
            DropTable("dbo.Pracownicy");
            DropTable("dbo.Pacjenci");
            DropTable("dbo.Opcje");
            DropTable("dbo.Adresy");
        }
    }
}
