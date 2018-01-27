namespace CreativeCravings.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RecipeIngredientXref",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RecipeID = c.Int(nullable: false),
                        IngredientID = c.Int(nullable: false),
                        QuantityType = c.String(),
                        Quantity = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ingredient", t => t.IngredientID, cascadeDelete: true)
                .ForeignKey("dbo.Recipe", t => t.RecipeID, cascadeDelete: true)
                .Index(t => t.RecipeID)
                .Index(t => t.IngredientID);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Category = c.Int(),
                        DateCreated = c.DateTime(),
                        DateUpdated = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecipeIngredientXref", "RecipeID", "dbo.Recipe");
            DropForeignKey("dbo.RecipeIngredientXref", "IngredientID", "dbo.Ingredient");
            DropIndex("dbo.RecipeIngredientXref", new[] { "IngredientID" });
            DropIndex("dbo.RecipeIngredientXref", new[] { "RecipeID" });
            DropTable("dbo.Recipe");
            DropTable("dbo.RecipeIngredientXref");
            DropTable("dbo.Ingredient");
        }
    }
}
