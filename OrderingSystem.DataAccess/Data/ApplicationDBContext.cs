using Microsoft.EntityFrameworkCore;
using OrderingSystem.Global.Entities;

namespace OrderingSystem.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ignore soft deleted records
            modelBuilder.Entity<Customer>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Order>().HasQueryFilter(o => !o.IsDeleted);

            // Foreign Keys
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Update all Entities' timestamps where the Entity has state timestamps from the BaseEntity
            var entries = ChangeTracker.Entries<BaseEntity>();

            if (entries != null)
            {
                foreach (var entry in entries)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedAt = DateTime.UtcNow;
                            entry.Entity.IsDeleted = false;
                            break;
                        case EntityState.Modified:
                            entry.Entity.UpdatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            // Changing the state to be modified for soft delete
                            entry.State = EntityState.Modified;
                            entry.Entity.DeletedAt = DateTime.UtcNow;
                            entry.Entity.IsDeleted = true;
                            break;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
