using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PayRollManagementSystemAPI.Models
{
    public class PayRollManagementSystemDbContext : IdentityDbContext<AccountUser>
    {
        public PayRollManagementSystemDbContext(DbContextOptions<PayRollManagementSystemDbContext> options) : base(options) { } 
        public DbSet<Leave>? Leave { get; set; }
        public DbSet<AllowanceAndDeduction>? AllowanceAndDeduction { get; set; }
        public DbSet<Salary>? Salary { get; set; }   
    }
}
