namespace Wicresoft.MODLibrarySystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Wicresoft.MODLibrarySystem.DataAccess.DBSource>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "Wicresoft.MODLibrarySystem.DataAccess.DBSource";
        }

        protected override void Seed(Wicresoft.MODLibrarySystem.DataAccess.DBSource context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
