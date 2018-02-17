namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedRecipeChefIdToString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipe", "ChefId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipe", "ChefId", c => c.Guid(nullable: false));
        }
    }
}
