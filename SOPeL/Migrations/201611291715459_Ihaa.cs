namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ihaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pacjenci", "DataUrodzenia", c => c.DateTime());
            AlterColumn("dbo.Rezerwacje", "DataRezerwacji", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rezerwacje", "DataModyfikacji", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Uzytkowicy", "DataUtworzenia", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uzytkowicy", "DataUtworzenia", c => c.DateTime());
            AlterColumn("dbo.Rezerwacje", "DataModyfikacji", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Rezerwacje", "DataRezerwacji", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Pacjenci", "DataUrodzenia", c => c.DateTime());
        }
    }
}
