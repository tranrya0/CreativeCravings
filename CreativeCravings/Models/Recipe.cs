using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CreativeCravings.Models
{
    public enum Category
    {
        Breakfast, Lunch, Dinner, Dessert
    }

    public class Recipe
    {

        public int ID { get; set; }

        [StringLength(200, MinimumLength = 2, ErrorMessage = "Recipe name must be betwee 2-200 characters")]
        public string Name { get; set; }

        public Category? Category { get; set; }
        
        public DateTime? DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [StringLength(250, ErrorMessage = "Recipe description must not exceed 250 characters")]
        public string Description { get; set; }

        public virtual ICollection<RecipeIngredientXref> RecipeIngredientXrefs { get; set; }

        
    }
}