namespace McDonalds.Data.Context.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Holydays",
                c => new
                    {
                        HolydayId = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Label = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HolydayId);
            
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        RestaurantId = c.Int(nullable: false),
                        Nom = c.String(),
                        ShortName = c.String(),
                        ServerName = c.String(),
                        ServerIpAddress = c.String(),
                        Email = c.String(),
                        City = c.String(),
                        Address_1 = c.String(),
                        Address_2 = c.String(),
                        Address_3 = c.String(),
                        Address_4 = c.String(),
                        ZipCode = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        FaxNumber = c.String(),
                        OpeningDate = c.DateTime(),
                        PermanentClosureDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.RestaurantId);
            
            CreateTable(
                "dbo.ServerEvents",
                c => new
                    {
                        ServerEventId = c.Int(nullable: false, identity: true),
                        RestaurantId = c.Int(nullable: false),
                        Event = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        UpTimes = c.DateTime(),
                        Detail = c.String(),
                    })
                .PrimaryKey(t => t.ServerEventId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ServerEvents");
            DropTable("dbo.Restaurants");
            DropTable("dbo.Holydays");
        }
    }
}
