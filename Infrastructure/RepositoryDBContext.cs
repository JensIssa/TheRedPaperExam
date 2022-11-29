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
        //Incremental ID's for every add
        modelBuilder.Entity<User>().Property(f => f.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<SubCategory>().Property(s => s.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
        //Foreign key relations and one to many
        modelBuilder.Entity<SubCategory>().HasOne(s => s.Category).
            WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategoryID).OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Product>().HasOne(p => p.SubCategory).
            WithMany(s => s.Products)
            .HasForeignKey(p => p.SubCategoryID);
        modelBuilder.Entity<Product>().HasOne(p => p.user).
            WithMany(u => u.products).HasForeignKey(p => p.userId);
    }

    public DbSet<User> UserTable
    {
        get; 
        set;
    }
    public DbSet<Category> CategoryTable 
    { 
        get;
        set; 
    }

    public DbSet<SubCategory> SubCategoryTable
    {
        get;
        set;
    }
    
    public DbSet<Product> ProductTable
    {
        get;
        set;
    }
}