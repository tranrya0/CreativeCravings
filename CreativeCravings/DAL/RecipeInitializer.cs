using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using CreativeCravings.Models;

namespace CreativeCravings.DAL
{
    public class RecipeInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RecipeContext>
    {
        protected override void Seed(RecipeContext context)
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

            recipes.ForEach(s => context.Recipes.Add(s));
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
            ingredients.ForEach(s => context.Ingredients.Add(s));
            context.SaveChanges();

            // Creating REcipeIngredientXrefs
            var recipeIngredientXrefs = new List<RecipeIngredientXref>
            {
                new RecipeIngredientXref{RecipeID=1, IngredientID=1, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=1, IngredientID=2, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=2, IngredientID=2, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=2, IngredientID=3, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=2, IngredientID=4, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=2, IngredientID=6, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=2, IngredientID=7, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=3, IngredientID=5, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=3, IngredientID=6, QuantityType="cups", Quantity=3},
                new RecipeIngredientXref{RecipeID=3, IngredientID=7, QuantityType="cups", Quantity=3}
            };

            recipeIngredientXrefs.ForEach(s => context.RecipeIngredientXrefs.Add(s));
            context.SaveChanges();
        }
    }
}