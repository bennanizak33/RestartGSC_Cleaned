namespace McDonalds.Data.Context.Migrations
{
    using McDonalds.Data.Context.Migrations.Seeds;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<McDonalds.Data.Context.McDonaldsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(McDonalds.Data.Context.McDonaldsContext context)
        {
            HolydaysSeed.Holydays(context);

            RestaurantsSeed.Restaurants(context);

            context.SaveChanges();
        }
    }
}
