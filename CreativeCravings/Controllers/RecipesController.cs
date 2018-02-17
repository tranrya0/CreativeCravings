using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CreativeCravings.DAL;
using CreativeCravings.Models;
using PagedList;
using System.Data.Entity.Infrastructure;
using CreativeCravings.ViewModels;
using System.Diagnostics;
using Microsoft.AspNet.Identity;


namespace CreativeCravings.Controllers
{
    public class RecipesController : Controller
    {
        private RecipeContext db = new RecipeContext();

        // GET: Recipes
        public ViewResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            // sorting columns, display opposite of current order, else display default order if nothing is selected for the column
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.CategorySortParam = sortOrder == "Category" ? "category_desc" : "Category";
            var recipes = from s in db.Recipes
                           select s;

            // set pagination page to 1 if search string has a value
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            // search string
            if (!String.IsNullOrEmpty(searchString))
            {
                recipes = recipes.Where(r => r.Name.Contains(searchString));
                    // example of multiple queries
                    /**
                     *  
                     students = students.Where(s => s.LastName.Contains(searchString)
                               || s.FirstMidName.Contains(searchString));
                     */
            }
            switch (sortOrder)
            {
                case "name_desc":
                    recipes = recipes.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    recipes = recipes.OrderBy(s => s.DateCreated);
                    break;
                case "date_desc":
                    recipes = recipes.OrderByDescending(s => s.DateCreated);
                    break;
                case "Category":
                    recipes = recipes.OrderBy(s => s.Category);
                    break;
                case "category_desc":
                    recipes = recipes.OrderByDescending(s => s.Category);
                    break;
                default:
                    recipes = recipes.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(recipes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Recipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        // GET: Recipes/Create
        public ActionResult Create()
        {
            PopulateAllIngredientsData();
            return View();
        }

        // POST: Recipes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Name,Category")] Recipe recipe, string[] selectedIngredients, string[] quantity, string[] quantityType)
        {
            recipe.DateCreated = System.DateTime.Now;
            try
            {
                if (ModelState.IsValid && User.Identity.IsAuthenticated)
                {
                    // get id of current user and add it to the recipe
                    var userId = User.Identity.GetUserId();
                    recipe.ChefId = userId;
                    db.Recipes.Add(recipe);

                    db.SaveChanges();

                    CreateRecipeIngredients(selectedIngredients, recipe, quantity, quantityType);
           
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException dex)
            {
                // log error here
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(recipe);
        }

        // GET: Recipes/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Recipe recipe = db.Recipes.Find(id);
            Recipe recipe = db.Recipes
                .Include(i => i.RecipeIngredientXrefs)
                .Where(i => i.ID == id)
                .Single();
            PopulateAssignedIngredientsData(recipe);

            if (recipe == null)
            {
                return HttpNotFound();
            }
            return View(recipe);
        }

        private void PopulateAllIngredientsData() {

            var allIngredients = db.Ingredients;
            var viewModel = new List<AssignedIngredientData>();
            foreach (var ingredient in allIngredients) {
                viewModel.Add(new AssignedIngredientData {
                    IngredientID = ingredient.ID,
                    Title = ingredient.Name,
                    Assigned = false
                });
            }
            ViewBag.Ingredients = viewModel;
        }

        private void PopulateAssignedIngredientsData(Recipe recipe) {

            var allIngredients = db.Ingredients;
            var recipeIngredients = new HashSet<int>(recipe.RecipeIngredientXrefs.Select(c => c.IngredientID));
            var viewModel = new List<AssignedIngredientData>();
            foreach (var ingredient in allIngredients) {
                viewModel.Add(new AssignedIngredientData {
                    IngredientID = ingredient.ID,
                    Title = ingredient.Name,
                    Assigned = recipeIngredients.Contains(ingredient.ID)
                });
            }
            ViewBag.Ingredients = viewModel;
        }

        // POST: Recipes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditRecipe(int? id, string[] selectedIngredients, string[] quantity, string[] quantityType)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //if (quantity == null) {
            //    Debug.Print("Quanity is null");
            //} else {
            //    foreach (var q in quantity) {
            //        Debug.Print(q);
            //    }
            //}

            //Recipe recipeToUpdate = db.Recipes.Find(id);
            Recipe recipeToUpdate = db.Recipes
                .Include(i => i.RecipeIngredientXrefs)
                .Where(i => i.ID == id)
                .Single();

            if (TryUpdateModel(recipeToUpdate, "",
                new string[] { "Name", "Category"}))
            {
                try
                {
                    recipeToUpdate.DateUpdated = System.DateTime.Now;
                    db.Entry(recipeToUpdate).State = EntityState.Modified;

                    UpdateRecipeIngredients(selectedIngredients, recipeToUpdate, quantity, quantityType);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException dex)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", dex.Message);
                }
            }
            PopulateAssignedIngredientsData(recipeToUpdate);
            return View(recipeToUpdate);
        }

        private void CreateRecipeIngredients(string[] selectedIngredients, Recipe recipeToUpdate, string[] quantity, string[] quantityType) {
            if (selectedIngredients == null) {
                recipeToUpdate.RecipeIngredientXrefs = new List<RecipeIngredientXref>();
                return;
            }

            //Debug.Print("############ Recipe im adding ingredients to " + recipeToUpdate.ID.ToString() + recipeToUpdate.Name);
            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);

            foreach (var ingredient in db.Ingredients) {
                if (selectedIngredientsHS.Contains(ingredient.ID.ToString())) {

                        int recipeNum = 0;
                        for (var i = 0; i < selectedIngredients.Length; i++) {
                            if (ingredient.ID.ToString().Equals(selectedIngredients[i])) {
                                //Debug.Print("#### Selected ingredient " + selectedIngredients[i]);
                                recipeNum = Int32.Parse(selectedIngredients[i]);
                            }
                        }

                        float quan = 0.0f;
                        try {
                            quan = float.Parse(quantity[recipeNum - 1], System.Globalization.CultureInfo.InvariantCulture);
                        } catch (Exception e) {
                            Debug.Print("### exception " + quan.ToString());
                        }
                        //Debug.Print("#### qunityty " + quantity[recipeNum - 1]);
                        //Debug.Print("###### quan " + quan.ToString());
                        //Debug.Print("#### qunitytytype " + quantityType[recipeNum - 1]);

                        recipeToUpdate.RecipeIngredientXrefs = new List<RecipeIngredientXref>();
                        recipeToUpdate.RecipeIngredientXrefs.Add(new RecipeIngredientXref {
                            RecipeID = recipeToUpdate.ID,
                            IngredientID = ingredient.ID,
                            Quantity = quan,
                            QuantityType = quantityType[recipeNum - 1],
                            Recipe = recipeToUpdate,
                            Ingredient = ingredient
                        });
                } 
            }
        }

        private void UpdateRecipeIngredients(string[] selectedIngredients, Recipe recipeToUpdate, string[] quantity, string[] quantityType) {

            if (selectedIngredients == null) {
                recipeToUpdate.RecipeIngredientXrefs = new List<RecipeIngredientXref>();
                return;
            }
            var selectedIngredientsHS = new HashSet<string>(selectedIngredients);

            var recipeIngredients = new HashSet<int>
                (recipeToUpdate.RecipeIngredientXrefs.Select(c => c.IngredientID));

            foreach (var ingredient in db.Ingredients) {
                    if (selectedIngredientsHS.Contains(ingredient.ID.ToString())) {
                    if (!recipeIngredients.Contains(ingredient.ID)) {

                        int recipeNum = 0;
                        for (var i = 0; i < selectedIngredients.Length; i++) {
                            if (ingredient.ID.ToString().Equals(selectedIngredients[i])) {
                            Debug.Print("#### Selected ingredient " + selectedIngredients[i]);
                                recipeNum = Int32.Parse(selectedIngredients[i]);
                            }
                        }

                        float quan = 0.0f;
                        try {
                            quan = float.Parse(quantity[recipeNum - 1], System.Globalization.CultureInfo.InvariantCulture);
                        } catch (Exception e) {
                            Debug.Print("### exception " + quan.ToString());
                        }
                        Debug.Print("#### qunityty" + quantity[recipeNum - 1]);
                        Debug.Print("#### qunitytytype" + quantityType[recipeNum - 1]);

                        recipeToUpdate.RecipeIngredientXrefs.Add(new RecipeIngredientXref {
                            RecipeID = recipeToUpdate.ID,
                            IngredientID = ingredient.ID,
                            Quantity = quan,
                            QuantityType = quantityType[recipeNum - 1],
                            Recipe = recipeToUpdate,
                            Ingredient = ingredient
                        });
                    }
                } else {
                    RecipeIngredientXref toberemoved = null;
                    foreach (var i in recipeToUpdate.RecipeIngredientXrefs) {
                        if (i.IngredientID == ingredient.ID) {
                            toberemoved = i;
                            //Debug.Print(toberemoved.Ingredient.Name);
                        }
                    }
                    if (toberemoved != null) {
                        recipeToUpdate.RecipeIngredientXrefs.Remove(toberemoved);
                    }
                    
                }
            }
        }

        // GET: Recipes/Delete/5
        [Authorize]
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system adminstrator.";
            }

            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }

            return View(recipe);
        }

        // POST: Recipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Recipe recipe = db.Recipes.Find(id);
                db.Recipes.Remove(recipe);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException dex)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
