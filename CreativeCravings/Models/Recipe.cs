using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCravings.Models
{
    public enum Category
    {
        Breakfast, Lunch, Dinner, Dessert
    }

    public class Recipe
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public Category? Category { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }

        
    }
}