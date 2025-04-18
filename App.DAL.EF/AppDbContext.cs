using System.Reflection;
using App.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext
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
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call the base implementation first (especially needed for Identity)
        base.OnModelCreating(modelBuilder);

        // Define the converter to enforce UTC on DateTime values.
        var dateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v,
            v => v);

        // Loop through all entity types to set the converter for each DateTime property.
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // Use reflection to get all properties of type DateTime or DateTime?
            var dateTimeProperties = entityType.ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            foreach (var property in dateTimeProperties)
            {
                // Use the CLR type to register the conversion.
                modelBuilder.Entity(entityType.ClrType)
                    .Property(property.Name)
                    .HasConversion(dateTimeConverter);
            }
        }
    }*/
}