namespace ECommerceProject1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class shipment : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Ships", newName: "ShipmentModels");
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        PriceAtPurchase = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Cards", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Cards", "ProductId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ProductImage", c => c.String());
            AddColumn("dbo.Orders", "SubTotal", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ShippingAddress", c => c.String());
            AddColumn("dbo.Orders", "BillingInformation", c => c.String());
            AddColumn("dbo.Orders", "PaymentMethod", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CardNumber", c => c.Double(nullable: false));
            AddColumn("dbo.Orders", "CVV", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "Card_Id", c => c.Int());
            CreateIndex("dbo.Orders", "Card_Id");
            AddForeignKey("dbo.Orders", "Card_Id", "dbo.Cards", "Id");
            DropColumn("dbo.Orders", "UserName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "UserName", c => c.String());
            DropForeignKey("dbo.Orders", "Card_Id", "dbo.Cards");
            DropIndex("dbo.Orders", new[] { "Card_Id" });
            DropColumn("dbo.Orders", "Card_Id");
            DropColumn("dbo.Orders", "CVV");
            DropColumn("dbo.Orders", "CardNumber");
            DropColumn("dbo.Orders", "PaymentMethod");
            DropColumn("dbo.Orders", "BillingInformation");
            DropColumn("dbo.Orders", "ShippingAddress");
            DropColumn("dbo.Orders", "SubTotal");
            DropColumn("dbo.Orders", "ProductImage");
            DropColumn("dbo.Cards", "ProductId");
            DropColumn("dbo.Cards", "UserId");
            DropTable("dbo.OrderItems");
            RenameTable(name: "dbo.ShipmentModels", newName: "Ships");
        }
    }
}
