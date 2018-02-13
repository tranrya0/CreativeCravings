namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModelValidations : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ingredient", "Name", c => c.String(maxLength: 100));
            AlterColumn("dbo.RecipeIngredientXref", "QuantityType", c => c.String(maxLength: 50));
            AlterColumn("dbo.Recipe", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Recipe", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipe", "Description", c => c.String());
            AlterColumn("dbo.Recipe", "Name", c => c.String());
            AlterColumn("dbo.RecipeIngredientXref", "QuantityType", c => c.String());
            AlterColumn("dbo.Ingredient", "Name", c => c.String());
        }
    }
}
