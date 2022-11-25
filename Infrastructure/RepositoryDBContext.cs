using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class RepositoryDBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public RepositoryDBContext(DbContextOptions<RepositoryDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(f => f.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();

    }
    
    public DbSet<User> UserTable { get; set; }
    public DbSet<Category> CategoryTable { get; set; }
}