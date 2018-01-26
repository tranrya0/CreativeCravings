using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using CreativeCravings.Models;


namespace CreativeCravings.ViewModels
{
    public class RecipeCategoryGroup
    {
        
        public Category? CategoryType { get; set; }

        public int RecipeCount { get; set; }

    }
}