using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace CreativeCravings.DAL {
    public class RecipeConfiguration : DbConfiguration {
        public RecipeConfiguration() {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());
        }
    }
}