namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filip21 : DbMigration
    {
        public override void Up()
        {
            DropColumn("public.Pracownicy", "Test");
        }
        
        public override void Down()
        {
            AddColumn("public.Pracownicy", "Test", c => c.String());
        }
    }
}
