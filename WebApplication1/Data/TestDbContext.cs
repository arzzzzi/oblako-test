using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}

