using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreativeCravings.DAL;
using CreativeCravings.ViewModels;

namespace CreativeCravings.Controllers
{
    public class HomeController : Controller
    {
        private RecipeContext db = new RecipeContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<RecipeCategoryGroup> data = from student in db.Recipes
                                                   group student by student.Category into categoryGroup
                                                   select new RecipeCategoryGroup()
                                                   {
                                                       CategoryType = categoryGroup.Key,
                                                       RecipeCount = categoryGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Chat() {
            return View();
        }
    }
}