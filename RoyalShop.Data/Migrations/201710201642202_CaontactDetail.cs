namespace RoyalShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CaontactDetail : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 250),
                        Website = c.String(maxLength: 250),
                        Address = c.String(maxLength: 250),
                        Other = c.String(),
                        Lat = c.Double(),
                        Ing = c.Double(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ContactDetail");
        }
    }
}
