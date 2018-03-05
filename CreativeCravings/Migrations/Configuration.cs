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

            // SEED USERS AND ROLES HERE
            ApplicationDbContext appContext = new ApplicationDbContext();
            createUserRoles(appContext);
            seedUsers(appContext);
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

        // create user roles on startup if they do not already exist
        // code originally from https://code.msdn.microsoft.com/ASPNET-MVC-5-Security-And-44cbdb97
        private void createUserRoles(ApplicationDbContext context)
        {


            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin role
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                /**
                
                ----------- Create user with admin role here -----------------------


                //Here we create a Admin super user who will maintain the website                  
                var user = new ApplicationUser();
                user.UserName = "shanu";
                user.Email = "admin@gmail.com";

                string userPWD = "adminpassword";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");

                }
                
             */
            }

            // creating Creating Manager role    
            if (!roleManager.RoleExists("Moderator"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Moderator";
                roleManager.Create(role);

            }
        }

        // seed users with roles
        private void seedUsers(ApplicationDbContext context)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // create admin user
            string userName = "admin@gmail.com";
            string password = "adminpassword";

            ApplicationUser user = userManager.FindByName(userName);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true
                };
                IdentityResult userResult = userManager.Create(user, password);
                if (userResult.Succeeded)
                {
                    var result = userManager.AddToRole(user.Id, "Admin");
                }
            }

            // add moderator
            addNewUser(context, "moderator@gmail.com", "password1", "Moderator", userManager, roleManager);
            addNewUser(context, "mod@gmail.com", "1234qwer", "Moderator", userManager, roleManager);
            addNewUser(context, "normal@gmail.com", "normal", "", userManager, roleManager);

        }

        private void addNewUser(ApplicationDbContext context,
            string email, string password, string role,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            ApplicationUser user = userManager.FindByName(email);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = email,
                    Email = email,
                    EmailConfirmed = true
                };
                IdentityResult userResult = userManager.Create(user, password);
                if (userResult.Succeeded && role != "")
                {
                    var result = userManager.AddToRole(user.Id, role);
                }
            }
        }
    }


}
