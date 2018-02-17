using CreativeCravings.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CreativeCravings.Startup))]
namespace CreativeCravings
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ConfigureAuth(app);
            createUserRoles(context);
            seedUsers(context);
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
