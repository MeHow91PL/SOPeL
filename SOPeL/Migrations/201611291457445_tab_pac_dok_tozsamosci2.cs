namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tab_pac_dok_tozsamosci2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pacjenci", "Aktw", c => c.String(maxLength: 1));
            AddColumn("dbo.Pracownicy", "Aktw", c => c.String(maxLength: 1));
            DropColumn("dbo.Pacjenci", "Akt");
            DropColumn("dbo.Pracownicy", "Akt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Pracownicy", "Akt", c => c.Boolean(nullable: false));
            AddColumn("dbo.Pacjenci", "Akt", c => c.Boolean(nullable: false));
            DropColumn("dbo.Pracownicy", "Aktw");
            DropColumn("dbo.Pacjenci", "Aktw");
        }
    }
}
