namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_Filip_baza : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Adresy", newSchema: "public");
            MoveTable(name: "dbo.Pacjenci", newSchema: "public");
            MoveTable(name: "dbo.Pracownicy", newSchema: "public");
            MoveTable(name: "dbo.Rezerwacje", newSchema: "public");
            MoveTable(name: "dbo.Uzytkowicy", newSchema: "public");
        }
        
        public override void Down()
        {
            MoveTable(name: "public.Uzytkowicy", newSchema: "dbo");
            MoveTable(name: "public.Rezerwacje", newSchema: "dbo");
            MoveTable(name: "public.Pracownicy", newSchema: "dbo");
            MoveTable(name: "public.Pacjenci", newSchema: "dbo");
            MoveTable(name: "public.Adresy", newSchema: "dbo");
        }
    }
}
