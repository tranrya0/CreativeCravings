namespace CreativeCravings.Migrations
{
    using CreativeCravings.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
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
            // get user
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            ApplicationUser user = userManager.FindByName("normal@gmail.com");
            ApplicationUser adminUser = userManager.FindByName("admin@gmail.com");

            // add posts
            var posts = new List<Post>
            {
                new Post{Title="Start of the website", Body="This is the start of our website!", AuthorID=adminUser.Id },
                new Post{Title="Adding Recipes", Body="Added some basic recipes!", AuthorID=adminUser.Id },
                new Post{Title="Ingredients Update!", Body="Added some ingredients to the website as well!", AuthorID=adminUser.Id },
                new Post{Title="Fixes", Body="Fixed some bugs so ingredients for new recipes should now update properly!", AuthorID=adminUser.Id }
            };

            posts.ForEach(s => context.Posts.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            // add recipes
            var recipes = new List<Recipe>
            {
            new Recipe{Name="Apple Pie",Category=Category.Breakfast, DateCreated=System.DateTime.Now,
                Description="A pie with apple filling", ChefId=user.Id},
            new Recipe{Name="Steak and Cheesy Mashed Potatoes",Category=Category.Breakfast, DateCreated=System.DateTime.Now,
                Description="Steak with a side of cheesey mashed potatoes", ChefId=user.Id},
            new Recipe{Name="Roasted Chicken and Rosemary Vegetables",Category=Category.Breakfast, DateCreated=System.DateTime.Now,
                Description="Roasted chicken, topped with garlic, pepper, rosemary, and a side of vegetables", ChefId=user.Id},
            new Recipe{Name="Gyro",Category=Category.Breakfast, DateCreated=System.DateTime.Now,
                Description="Classic Gyro, filled with pork or chicken, tomatom, onions, and tzatziki sauce", ChefId=user.Id},
            new Recipe{Name="Baked Yams",Category=Category.Dessert, DateCreated=System.DateTime.Now,
                Description="Baked Yams, topped with sour cream. Can substitute sweet potatoes", ChefId=user.Id},
            new Recipe{Name="Baked Alaska",Category=Category.Dessert, DateCreated=System.DateTime.Now,
                Description="Dessert consisting of ice cream and cake, topped with browned meringue", ChefId=user.Id},
            new Recipe{Name="Chicken Noodle Soup",Category=Category.Lunch, DateCreated=System.DateTime.Now, ChefId=user.Id},
            new Recipe{Name="Garlic Bread",Category=Category.Lunch, DateCreated=System.DateTime.Now, ChefId=user.Id}
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
