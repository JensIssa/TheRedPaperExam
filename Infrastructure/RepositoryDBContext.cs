using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class RepositoryDBContext : Microsoft.EntityFrameworkCore.DbContext
{
    public RepositoryDBContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().Property(f => f.Id).ValueGeneratedOnAdd();

    }
    
    public DbSet<User> UserTable { get; set; }
    
}