using App.Domain;
using App.Domain.Identity;
using Base.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, AppUserRole,
    IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public DbSet<Category> Categories { get; set; } = default!;
    public DbSet<Inventory> Inventories { get; set; } = default!;
    public DbSet<Order> Orders { get; set; } = default!;
    public DbSet<OrderProduct> OrderProducts { get; set; } = default!;
    public DbSet<Payment> Payments { get; set; } = default!;
    public DbSet<Person> Persons { get; set; } = default!;
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<ProductSupplier> ProductSuppliers { get; set; } = default!;
    public DbSet<Refund> Refunds { get; set; } = default!;
    public DbSet<StockOrder> StockOrders { get; set; } = default!;
    public DbSet<StockOrderItem> StockOrderItems { get; set; } = default!;
    public DbSet<Supplier> Suppliers { get; set; } = default!;
    public DbSet<Warehouse> Warehouses { get; set; } = default!;
    public DbSet<AppRefreshToken> RefreshTokens { get; set; } = default!;
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var addedEntries = ChangeTracker.Entries()
            .Where(e => e is { Entity: IDomainMeta });

        foreach (var entry in addedEntries)
        {
            if (entry.State == EntityState.Added)
            {
                (entry.Entity as IDomainMeta)!.CreatedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.CreatedBy = "system";
            }
            else if (entry.State == EntityState.Modified)
            {
                (entry.Entity as IDomainMeta)!.ChangedAt = DateTime.UtcNow;
                (entry.Entity as IDomainMeta)!.ChangedBy = "system";

                // Prevent overwriting CreatedBy/CreatedAt/UserId on update
                entry.Property("CreatedAt").IsModified = false;
                entry.Property("CreateBy").IsModified = false;
                entry.Property("UserId").IsModified = false;
            }
        }
        
        return base.SaveChangesAsync(cancellationToken);
    }
}