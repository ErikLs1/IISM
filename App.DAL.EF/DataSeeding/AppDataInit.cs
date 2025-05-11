using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.DataSeeding;

public class AppDataInit
{
    public static void SeedAppData(AppDbContext context)
    {
        
        /* Insert Categories */
        foreach (var cat in InitialData.Categories)
        {
            var category = new Category()
            {
                Id = cat.id ?? Guid.NewGuid(),
                CategoryName = cat.categoryName,
                CategoryDescription = cat.categoryDescription,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            var result = context.Categories.Add(category);
            if (result.State != EntityState.Added)
            {
                throw new ApplicationException("Category creation failed!");
            }
        }

        context.SaveChanges();

        /* Insert Suppliers */
        foreach (var sup in InitialData.Suppliers)
        {
            var supplier = new Supplier()
            {
                Id = sup.id ?? Guid.NewGuid(),
                SupplierName = sup.supplierName,
                SupplierPhoneNumber = sup.supplierPhoneNumber,
                SupplierEmail = sup.supplierEmail,
                SupplierAddress = sup.supplierAddress,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            var result = context.Suppliers.Add(supplier);
            if (result.State != EntityState.Added)
            {
                throw new ApplicationException("Supplier creation failed!");
            }
        }

        context.SaveChanges();
        
        /* Insert Products */
        var categoryMap = context.Categories.ToDictionary(c => c.CategoryName, c => c.Id);

        foreach (var p in InitialData.Products)
        {
            if (!categoryMap.TryGetValue(p.categoryName, out var catId))
                throw new ApplicationException($"Failed to get category {p.categoryName}");

            var product = new Product()
            {
                Id = p.id ?? Guid.NewGuid(),
                ProductName = p.productName,
                ProductDescription = p.productDescription,
                ProductPrice = p.productPrice,
                ProductStatus = p.productStatus,
                CategoryId = catId,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "system"
            };

            context.Products.Add(product);
        }
        
        context.SaveChanges();
        
        /* Insert Product Suppliers */
        var rnd = new Random(42);
        var products = context.Products.ToList();
        var suppliers = context.Suppliers.ToList();
        var productSuppliers = new List<ProductSupplier>();

        foreach (var product in products)
        {
            var picks = suppliers.OrderBy(_ => rnd.Next()).Take(3);
            foreach (var supplier in picks)
            {
                var factor = 0.8 + rnd.NextDouble() * 0.4;
                var cost = Math.Round(product.ProductPrice * (decimal)factor, 2);
                
                productSuppliers.Add(new ProductSupplier
                {
                    Id         = Guid.NewGuid(),
                    ProductId  = product.Id,
                    SupplierId = supplier.Id,
                    UnitCost   = cost,
                    CreatedAt  = DateTime.UtcNow,
                    CreatedBy  = "system"
                });
            }
        }
        
        context.ProductSuppliers.AddRange(productSuppliers);
        context.SaveChanges();
    }

    public static void MigrateDatabase(AppDbContext context)
    {
        context.Database.Migrate();
    }

    public static void DeleteDatabase(AppDbContext context)
    {
        context.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        foreach (var (roleName, id) in InitialData.Roles)
        {
            var role = roleManager.FindByNameAsync(roleName).Result;

            if (role != null) continue;

            role = new AppRole()
            {
                Id = id ?? Guid.NewGuid(),
                Name = roleName,
            };

            var result = roleManager.CreateAsync(role).Result;
            if (!result.Succeeded)
            {
                throw new ApplicationException("Role creation failed!");
            }
        }

        foreach (var userInfo in InitialData.Users)
        {
            var user = userManager.FindByEmailAsync(userInfo.name).Result;
            if (user == null)
            {
                user = new AppUser()
                {
                    Id = userInfo.id ?? Guid.NewGuid(),
                    Email = userInfo.name,
                    UserName = userInfo.name,
                    EmailConfirmed = true,
                    FirstName = userInfo.firstName,
                    LastName = userInfo.lastName,
                };
                var result = userManager.CreateAsync(user, userInfo.password).Result;
                if (!result.Succeeded)
                {
                    throw new ApplicationException("User creation failed!");
                }
            }

            foreach (var role in userInfo.roles)
            {
                if (userManager.IsInRoleAsync(user, role).Result)
                {
                    Console.WriteLine($"User {user.UserName} already in role {role}");
                    continue;
                }

                var roleResult = userManager.AddToRoleAsync(user, role).Result;
                if (!roleResult.Succeeded)
                {
                    foreach (var error in roleResult.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
                else
                {
                    Console.WriteLine($"User {user.UserName} added to role {role}");
                }
            }
        }
    }
}