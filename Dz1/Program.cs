using Dz1.Context;
using Dz1.Entities;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dz1;

internal class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        string connectionString = configuration.GetConnectionString("DefaultConnection");

        var optionsBuilder = new DbContextOptionsBuilder<ShopDbContext>();
        optionsBuilder.UseSqlServer(connectionString);

        using (var context = new ShopDbContext(optionsBuilder.Options))
        {
            //context.Database.EnsureCreated();

            //CreateProduct(context);
            //UpdateProductName(context, 1, "New Laptop");
            //UpdateStock(context, 1, 0);
            //ShowOutOfStock(context);
            //ShowTop3Expensive(context);
            //DeleteProduct(context, 1);

            //Dz2
            var shop = new Shop(context);

            shop.CreateCategory("Electronics");
            shop.CreateProduct("Laptop", "Gaming laptop", 45000, 10, 1);

            var products = shop.GetProducts();
            foreach (var p in products) Console.WriteLine($"{p.Name} - {p.Price}");

            var category = shop.GetCategoryByProduct(1);
            Console.WriteLine($"Product category: {category?.Name}");

            var productsByCategory = shop.GetProductsByCategory(1);
            foreach (var p in productsByCategory) Console.WriteLine($"Product in category: {p.Name}");

            //shop.DeleteProduct(1);
            //shop.DeleteCategory(1);
        }
    }

    static void CreateProduct(ShopDbContext context)
    {
        var category = new Category { Name = "Electronics" };
        context.Categories.Add(category);
        context.SaveChanges();

        var product = new Product
        {
            Name = "Laptop",
            Description = "Gaming laptop",
            Price = 45000,
            StockQuantity = 10,
            CategoryId = category.Id
        };

        Console.WriteLine($"Created {product.Name}");
        context.Products.Add(product);
        context.SaveChanges();
    }

    static void UpdateProductName(ShopDbContext context, int id, string newName)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine($"Updated {product.Name} to {newName}");
            product.Name = newName;
            context.SaveChanges();
        }
    }

    static void UpdateStock(ShopDbContext context, int id, int newQuantity)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine($"Updated stock from {product.StockQuantity} to {newQuantity}");
            product.StockQuantity = newQuantity;
            context.SaveChanges();
        }
    }

    static void DeleteProduct(ShopDbContext context, int id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            Console.WriteLine($"Deleted {product.Name}, ID:{id}");
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }

    static void ShowOutOfStock(ShopDbContext context)
    {
        var products = context.Products.Where(p => p.StockQuantity == 0).ToList();

        Console.WriteLine("Products out of stock:");
        foreach (var p in products) Console.WriteLine($"{p.Name} - {p.Price}");
    }

    static void ShowTop3Expensive(ShopDbContext context)
    {
        var products = context.Products
            .OrderByDescending(p => p.Price)
            .Take(3)
            .ToList();

        Console.WriteLine("Top 3 most expensive:");
        foreach (var p in products) Console.WriteLine($"{p.Name} - {p.Price}");
    }
}
