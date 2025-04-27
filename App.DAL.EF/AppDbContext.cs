using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
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
}