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
            new Recipe{Name="Apple Pie",Category=Category.Breakfast,},
            new Recipe{Name="Steak and Cheesy Mashed Potatoes",Category=Category.Breakfast,},
            new Recipe{Name="Roasted Chicken and Rosemary Vegetables",Category=Category.Breakfast},
            new Recipe{Name="Gyro",Category=Category.Breakfast},
            new Recipe{Name="Baked Yams",Category=Category.Breakfast},
            new Recipe{Name="Baked Alaska",Category=Category.Breakfast},
            new Recipe{Name="Chicken Noodle Soup",Category=Category.Breakfast},
            new Recipe{Name="Garlic Bread",Category=Category.Breakfast}
            };

            recipes.ForEach(s => context.Recipes.Add(s));
            context.SaveChanges();
            var ingredients = new List<Ingredient>
            {
            new Ingredient{RecipeID=1,Name="Apples",Quantity=3,},
            new Ingredient{RecipeID=1,Name="Flour",Quantity=3,},
            new Ingredient{RecipeID=2,Name="Steaks",Quantity=2,},
            new Ingredient{RecipeID=2,Name="Potatoes",Quantity=4,},
            new Ingredient{RecipeID=3,Name="Chicken",Quantity=4,},
            new Ingredient{RecipeID=3,Name="Garlic",Quantity=3,},
            new Ingredient{RecipeID=3,Name="Rosemary",Quantity=4,}
            };
            ingredients.ForEach(s => context.Ingredients.Add(s));
            context.SaveChanges();
        }
    }
}