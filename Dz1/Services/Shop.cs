using Dz1.Context;
using Dz1.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dz1;

public class Shop
{
    private readonly ShopDbContext context;
    public Shop(ShopDbContext _context) { context = _context; }

    public void CreateCategory(string name)
    {
        var category = new Category { Name = name };
        context.Categories.Add(category);
        context.SaveChanges();
    }

    public List<Category> GetCategories() { return context.Categories.ToList(); }

    public void UpdateCategory(int id, string newName)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category != null)
        {
            category.Name = newName;
            context.SaveChanges();
        }
    }

    public void DeleteCategory(int id)
    {
        var category = context.Categories.FirstOrDefault(c => c.Id == id);
        if (category != null)
        {
            context.Categories.Remove(category);
            context.SaveChanges();
        }
    }

    public void CreateProduct(string name, string description, decimal price, int stock, int categoryId)
    {
        var product = new Product
        {
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stock,
            CategoryId = categoryId
        };

        context.Products.Add(product);
        context.SaveChanges();
    }

    public List<Product> GetProducts() { return context.Products.Include(p => p.Category).ToList(); }

    public void UpdateProduct(int id, string name, decimal price, int stock)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            product.Name = name;
            product.Price = price;
            product.StockQuantity = stock;
            context.SaveChanges();
        }
    }

    public void DeleteProduct(int id)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            context.Products.Remove(product);
            context.SaveChanges();
        }
    }

    public Category? GetCategoryByProduct(int productId)
    {
        var product = context.Products
            .Include(p => p.Category)
            .FirstOrDefault(p => p.Id == productId);

        return product?.Category;
    }

    public List<Product> GetProductsByCategory(int categoryId)
    {
        return context.Products
            .Where(p => p.CategoryId == categoryId)
            .ToList();
    }
}