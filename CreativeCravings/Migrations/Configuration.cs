namespace CreativeCravings.Migrations
{
    using CreativeCravings.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CreativeCravings.DAL.RecipeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CreativeCravings.DAL.RecipeContext context)
        {
            var recipes = new List<Recipe>
            {
            new Recipe{Name="Apple Pie",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Steak and Cheesy Mashed Potatoes",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Roasted Chicken and Rosemary Vegetables",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Gyro",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Baked Yams",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Baked Alaska",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Chicken Noodle Soup",Category=Category.Breakfast, DateCreated=System.DateTime.Now},
            new Recipe{Name="Garlic Bread",Category=Category.Breakfast, DateCreated=System.DateTime.Now}
            };
            recipes.ForEach(s => context.Recipes.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var ingredients = new List<Ingredient>
            {
            new Ingredient{Name="Apples"},
            new Ingredient{Name="Flour"},
            new Ingredient{Name="Steaks"},
            new Ingredient{Name="Potatoes"},
            new Ingredient{Name="Chicken"},
            new Ingredient{Name="Garlic"},
            new Ingredient{Name="Rosemary"}
            };
            ingredients.ForEach(s => context.Ingredients.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var recipeIngredientXrefs = new List<RecipeIngredientXref> {
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Apple Pie").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Apples").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Apple Pie").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Flour").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Steak and Cheesy Mashed Potatoes").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Flour").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Steak and Cheesy Mashed Potatoes").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Steaks").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Steak and Cheesy Mashed Potatoes").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Potatoes").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Steak and Cheesy Mashed Potatoes").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Garlic").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Steak and Cheesy Mashed Potatoes").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Rosemary").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Roasted Chicken and Rosemary Vegetables").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Chicken").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Roasted Chicken and Rosemary Vegetables").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Garlic").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
                new RecipeIngredientXref {
                    RecipeID = recipes.Single(s => s.Name == "Roasted Chicken and Rosemary Vegetables").ID,
                    IngredientID = ingredients.Single(c => c.Name == "Rosemary").ID,
                    QuantityType = "cups",
                    Quantity = 3
                },
            };

            foreach (RecipeIngredientXref e in recipeIngredientXrefs) {
                var inDataBase = context.RecipeIngredientXrefs.Where(
                    s =>
                        s.Recipe.ID == e.RecipeID &&
                        s.Ingredient.ID == e.IngredientID).SingleOrDefault();

                if (inDataBase == null) {
                    context.RecipeIngredientXrefs.Add(e);
                }
            }
            context.SaveChanges();

        }
    }
}
