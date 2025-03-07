using Microsoft.EntityFrameworkCore;
using RetirementCalculator.API.Models;
namespace RetirementCalculator.API.Data
{
    public class RetirementContext : DbContext
    {
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<RetirementScenario> RetirementScenarios { get; set; }

        public RetirementContext(DbContextOptions<RetirementContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RetirementScenario>()
                .HasOne(s => s.UserProfile)
                .WithMany(u => u.Scenarios)
                .HasForeignKey(s => s.UserProfileId);
        }
    }
}
