namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tab_pac_dok_tozsamosci3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pacjenci", "DataUrodzenia", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pacjenci", "DataUrodzenia", c => c.DateTime(nullable: false));
        }
    }
}
