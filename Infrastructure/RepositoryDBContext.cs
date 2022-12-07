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

        
        modelBuilder.Entity<SubCategory>().
            HasOne<Category>().
            WithMany(c => c.SubCategories)
            .HasForeignKey(s => s.CategoryID).
            OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Product>().HasOne<SubCategory>().
            WithMany(s => s.Products)
            .HasForeignKey(p => p.SubCategoryID);
        
        modelBuilder.Entity<Product>().HasOne<User>().
            WithMany(u => u.products).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>().HasOne(p => p.ProductCondition).WithMany(c => c.Prodcuts)
            .HasForeignKey(p => p.ProductConditionId);

        modelBuilder.Entity<Condition>().HasData(new Condition() { Id = 1, Name = "Ubrugt" }, new Condition() {Id = 2, Name = "Fremragende"}, new Condition() {Id = 3, Name = "God"},
            new Condition() {Id = 4, Name = "Brugt"}, new Condition() { Id = 5, Name = "Nedslidt"});

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

    public DbSet<Condition> ConditionTable
    {
        get;
        set;
    }
}