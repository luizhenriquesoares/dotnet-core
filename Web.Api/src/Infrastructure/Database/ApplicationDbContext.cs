using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Web.Api.Modules.Auth.Domain;

namespace Web.Api.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<IdentityUserClaim<long>> IdentityUserClaim { get; set; }
        public DbSet<IdentityUserRole<long>> IdentityUserRole { get; set; }
        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(x => new { x.Id });
            modelBuilder.Entity<Phone>().HasKey(x => new { x.PhoneId });
            modelBuilder.Entity<IdentityUserRole<long>>().HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<ApplicationRole>().HasKey(x => new { x.Id });
        }

    }
}
