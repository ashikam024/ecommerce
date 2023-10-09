namespace ECommerceProject1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipmentmodelandpaymentadded : DbMigration
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
            
            AddColumn("dbo.ShipmentModels", "PaymentType_Id", c => c.Int());
            CreateIndex("dbo.ShipmentModels", "PaymentType_Id");
            AddForeignKey("dbo.ShipmentModels", "PaymentType_Id", "dbo.Payments", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShipmentModels", "PaymentType_Id", "dbo.Payments");
            DropIndex("dbo.ShipmentModels", new[] { "PaymentType_Id" });
            DropColumn("dbo.ShipmentModels", "PaymentType_Id");
            DropTable("dbo.Payments");
        }
    }
}
