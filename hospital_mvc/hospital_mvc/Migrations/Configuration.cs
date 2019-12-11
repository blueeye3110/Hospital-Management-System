namespace hospital_mvc.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<hospital_mvc.Models.Hospital>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "hospital_mvc.Models.Hospital";
        }

        protected override void Seed(hospital_mvc.Models.Hospital context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
