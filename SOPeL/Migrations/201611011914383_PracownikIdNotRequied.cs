namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PracownikIdNotRequied : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Uzytkowicy", "PracownikID", "dbo.Pracownicy");
            DropIndex("dbo.Uzytkowicy", new[] { "PracownikID" });
            AlterColumn("dbo.Uzytkowicy", "PracownikID", c => c.Int());
            CreateIndex("dbo.Uzytkowicy", "PracownikID");
            AddForeignKey("dbo.Uzytkowicy", "PracownikID", "dbo.Pracownicy", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uzytkowicy", "PracownikID", "dbo.Pracownicy");
            DropIndex("dbo.Uzytkowicy", new[] { "PracownikID" });
            AlterColumn("dbo.Uzytkowicy", "PracownikID", c => c.Int(nullable: false));
            CreateIndex("dbo.Uzytkowicy", "PracownikID");
            AddForeignKey("dbo.Uzytkowicy", "PracownikID", "dbo.Pracownicy", "ID", cascadeDelete: true);
        }
    }
}
