using InventoryAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.DataContext
{
    public class ApplicationContext(DbContextOptions<ApplicationContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(new Role { Id = 1, Name = "Admin" }, new Role { Id = 2, Name = "Staff" });

            modelBuilder.Entity<UserRole>().HasKey(x => new { x.RoleId, x.UserId });

            modelBuilder.Entity<UserRole>().HasOne( u => u.Role).WithMany(ur => ur.Users).HasForeignKey(u => u.RoleId);
            modelBuilder.Entity<UserRole>().HasOne(b => b.User).WithMany(bp => bp.Roles).HasForeignKey(b => b.UserId);

        }
        
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

    }
}
