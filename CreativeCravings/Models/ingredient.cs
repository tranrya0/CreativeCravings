namespace CreativeCravings.Models
{
    public class Ingredient
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public string Name { get; set; }
        public float Quantity { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}