namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setRecipeXrefCompositeKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.RecipeIngredientXref");
            AddPrimaryKey("dbo.RecipeIngredientXref", new[] { "RecipeID", "IngredientID" });
            DropColumn("dbo.RecipeIngredientXref", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RecipeIngredientXref", "ID", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.RecipeIngredientXref");
            AddPrimaryKey("dbo.RecipeIngredientXref", "ID");
        }
    }
}
