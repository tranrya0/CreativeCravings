namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUserRoles : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "ChefId", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipe", "ChefId");
        }
    }
}
