namespace ECommerceProject1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedblockuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsBlocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsBlocked");
        }
    }
}
