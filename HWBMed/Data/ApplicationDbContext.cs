using HWBMed.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HWBMed.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Profile> Profiles { get; set; } = default!;
        public DbSet<Area> Areas { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Employee> Employees { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.IdUser)
                .IsRequired();
            builder.Entity<Profile>()
                .HasOne(p => p.Employee)
                .WithOne(e => e.Profile)
                .HasForeignKey<Employee>(e => e.IdProfile)
                .IsRequired();

        }
    }
}
