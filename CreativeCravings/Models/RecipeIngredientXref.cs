using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{

    public class RecipeIngredientXref
    {

        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }
        public string QuantityType { get; set; }
        public float Quantity { get; set; }
        public virtual Recipe Recipe { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        // helper property to calculate Quantity + QuantityType as string
        public string QuantityDisplayString
        {
            get
            {
                return Quantity + " " + QuantityType;
            }
        }
    }
}