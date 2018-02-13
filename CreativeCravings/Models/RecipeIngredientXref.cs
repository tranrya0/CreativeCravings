using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{

    public class RecipeIngredientXref
    {

        public int ID { get; set; }

        public int RecipeID { get; set; }
        public int IngredientID { get; set; }

        [Required, StringLength(50, MinimumLength = 1, ErrorMessage = "Ingredient quantity type must not exceed 50 characters")]
        [Display(Name = "Quantity Type")]
        public string QuantityType { get; set; }
        public float Quantity { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        // helper property to calculate Quantity + QuantityType as string
        [Display(Name = "Quantity Display")]
        public string QuantityDisplayString
        {
            get
            {
                return Quantity + " " + QuantityType;
            }
        }
    }
}