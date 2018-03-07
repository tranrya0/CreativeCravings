using CreativeCravings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CreativeCravings.DAL
{
    public class UnitOfWork : IDisposable
    {
        private RecipeContext context = new RecipeContext();
        private GenericRepository<Post> postRepository;
        private GenericRepository<Ingredient> ingredientRepository;



        public GenericRepository<Post> PostRepository
        {
            get
            {
                return this.postRepository ?? new GenericRepository<Post>(context);
            }
        }

        public GenericRepository<Ingredient> IngredientRepository
        {
            get
            {
                return this.ingredientRepository ?? new GenericRepository<Ingredient>(context);
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}