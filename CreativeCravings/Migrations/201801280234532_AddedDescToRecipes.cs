namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDescToRecipes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipe", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipe", "Description");
        }
    }
}
