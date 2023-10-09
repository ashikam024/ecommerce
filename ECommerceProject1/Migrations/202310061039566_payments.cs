namespace ECommerceProject1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentMethod", c => c.String());
            AddColumn("dbo.Payments", "CardNumber", c => c.String());
            AddColumn("dbo.Payments", "ExpiryDate", c => c.String());
            AddColumn("dbo.Payments", "CVV", c => c.String());
            AddColumn("dbo.Payments", "AccountNumber", c => c.String());
            AddColumn("dbo.Payments", "BankName", c => c.String());
            AddColumn("dbo.Payments", "AccountHolder", c => c.String());
            AddColumn("dbo.Payments", "RoutingNumber", c => c.String());
            DropColumn("dbo.Payments", "PaymentType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "PaymentType", c => c.String());
            DropColumn("dbo.Payments", "RoutingNumber");
            DropColumn("dbo.Payments", "AccountHolder");
            DropColumn("dbo.Payments", "BankName");
            DropColumn("dbo.Payments", "AccountNumber");
            DropColumn("dbo.Payments", "CVV");
            DropColumn("dbo.Payments", "ExpiryDate");
            DropColumn("dbo.Payments", "CardNumber");
            DropColumn("dbo.Payments", "PaymentMethod");
        }
    }
}
