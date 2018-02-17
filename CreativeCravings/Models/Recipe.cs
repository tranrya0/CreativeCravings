using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreativeCravings.Models
{
    public enum Category
    {
        Breakfast, Lunch, Dinner, Dessert
    }

    public class Recipe
    {

        public int ID { get; set; }

        // user id
        //[ForeignKey("ApplicationUser")]
        public string ChefId { get; set; }

        [Required, StringLength(200, MinimumLength = 2, ErrorMessage = "Recipe name must be betwee 2-200 characters")]
        public string Name { get; set; }

        [DisplayFormat(NullDisplayText = "Unassigned")]
        public Category? Category { get; set; }

        [Display(Name = "Date Created")]
        public DateTime? DateCreated { get; set; }

        [Display(Name = "Date Updated")]
        public DateTime? DateUpdated { get; set; }

        [StringLength(250, ErrorMessage = "Recipe description must not exceed 250 characters")]
        public string Description { get; set; }

        public virtual ICollection<RecipeIngredientXref> RecipeIngredientXrefs { get; set; }

        //public virtual ApplicationUser ApplicationUser { get; set; }

        
    }
}