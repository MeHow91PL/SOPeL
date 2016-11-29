namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tab_pac_dok_tozsamosci : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pacjenci", "DokumentTozsamosci", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pacjenci", "DokumentTozsamosci", c => c.String(nullable: false));
        }
    }
}
