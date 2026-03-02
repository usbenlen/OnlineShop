using Microsoft.EntityFrameworkCore;

using Dz1.Entities;

namespace Dz1.Context;

public class ShopDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public ShopDbContext() { }
    public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options) { }

}
