namespace ECommerceProject1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PaymentType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ShipmentModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        PaymentType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Payments", t => t.PaymentType_Id)
                .Index(t => t.PaymentType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipmentModels", "PaymentType_Id", "dbo.Payments");
            DropIndex("dbo.ShipmentModels", new[] { "PaymentType_Id" });
            DropTable("dbo.ShipmentModels");
            DropTable("dbo.Payments");
        }
    }
}
