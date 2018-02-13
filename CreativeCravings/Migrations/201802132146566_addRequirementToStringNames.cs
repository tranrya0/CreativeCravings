namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addRequirementToStringNames : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredient", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.RecipeIngredientXref", "QuantityType", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RecipeIngredientXref", "QuantityType", c => c.String(maxLength: 50));
            AlterColumn("dbo.Ingredient", "Name", c => c.String(maxLength: 100));
        }
    }
}
