namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DodanoCosTam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uzytkowicy", "DataUtworzenia", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uzytkowicy", "DataUtworzenia");
        }
    }
}
