namespace SOPeL.Migrations
{
    using DAL;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SOPeL.DAL.SopelContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SOPeL.DAL.SopelContext context)
        {
            SopelInitializer.SeedsopelLocal(context);
        }
    }
}
