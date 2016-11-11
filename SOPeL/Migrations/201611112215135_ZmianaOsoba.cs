namespace SOPeL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZmianaOsoba : DbMigration
    {
        public override void Up()
        {
            AlterColumn("public.Pacjenci", "Telefon", c => c.String(maxLength: 9));
            AlterColumn("public.Pacjenci", "Email", c => c.String(maxLength: 100));
            AlterColumn("public.Pracownicy", "Telefon", c => c.String(maxLength: 9));
            AlterColumn("public.Pracownicy", "Email", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("public.Pracownicy", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("public.Pracownicy", "Telefon", c => c.String(nullable: false, maxLength: 9));
            AlterColumn("public.Pacjenci", "Email", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("public.Pacjenci", "Telefon", c => c.String(nullable: false, maxLength: 9));
        }
    }
}
