using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{
    public class Ingredient
    {
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 2, ErrorMessage = "Ingredient name must be betwee 2-100 characters")]
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredientXref> RecipeIngredientXrefs { get; set; }
    }
}