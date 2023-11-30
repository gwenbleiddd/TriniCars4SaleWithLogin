using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TriniCars4SaleWithLogin.Models;

namespace TriniCars4SaleWithLogin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TriniCars4SaleWithLogin.Models.Vehicle> Vehicle { get; set; } = default!;
    }
}
