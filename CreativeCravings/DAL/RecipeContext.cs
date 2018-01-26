using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreativeCravings.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CreativeCravings.DAL
{
    public class RecipeContext : DbContext
    {

        public RecipeContext() : base("RecipeContext")
        {
        }

        // each DbSet corresponds to a database table
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredientXref> RecipeIngredientXrefs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // prevent tables from being pluralized
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}