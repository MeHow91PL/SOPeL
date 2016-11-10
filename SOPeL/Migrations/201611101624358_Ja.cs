namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Ja : DbMigration
    {
        public override void Up()
        {
            AddColumn("public.Uzytkowicy", "jakiesPole", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("public.Uzytkowicy", "jakiesPole");
        }
    }
}
