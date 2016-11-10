namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filip1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Pracownicy", "Test", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Pracownicy", "Test");
        }
    }
}
