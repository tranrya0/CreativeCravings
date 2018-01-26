using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngredientXref> RecipeIngredientXrefs { get; set; }
    }
}