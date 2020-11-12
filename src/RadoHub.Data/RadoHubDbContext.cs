using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RadoHub.Data.Models;

namespace RadoHub.WebApp.Data
{
    public class RadoHubDbContext : IdentityDbContext<RadoHubUser>
    {
        public RadoHubDbContext(DbContextOptions<RadoHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<CookingRecipe> CookingRecipes { get; set; }

        public DbSet<InspirationPeriod> InspirationPeriods { get; set; }

    }
}
