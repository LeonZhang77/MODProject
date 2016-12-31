namespace Wicresoft.BadmintonSystem.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Wicresoft.BadmintonSystem.DataAccess.DBSource>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Wicresoft.BadmintonSystem.DataAccess.DBSource context)
        {
        }
    }
}
