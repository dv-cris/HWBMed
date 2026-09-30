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
        public DbSet<EmployeeArea> employeeAreas { get; set; } = default!;
        public DbSet<Service> Services { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasOne(u => u.Employee)
                .WithOne(e => e.User)
                .HasForeignKey<Employee>(e => e.IdUser)
                .IsRequired();

            builder.Entity<Profile>()
                .HasMany<Employee>()
                .WithOne(e => e.Profile)
                .HasForeignKey(e => e.IdProfile)
                .IsRequired();            

            builder.Entity<EmployeeArea>()
                .HasOne(ea => ea.Employee)
                .WithMany(e => e.EmployeeAreas)
                .HasForeignKey(ea => ea.IdEmployee)
                .IsRequired();
            builder.Entity<EmployeeArea>()
                .HasOne(ea => ea.Area)
                .WithMany(a => a.EmployeeAreas)
                .HasForeignKey(ea => ea.IdArea)
                .IsRequired();

            builder.Entity<Area>()
                .HasMany<Service>(a=>a.Services)
                .WithOne(e => e.Area)
                .HasForeignKey(e => e.IdArea)
                .IsRequired();

        }
    }
}
