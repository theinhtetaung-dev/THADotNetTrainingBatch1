using Microsoft.EntityFrameworkCore;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tbl_Students> Students { get; set; }

    public DbSet<Tbl_User>Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tbl_Students>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Tbl_User>()
            .Property(u => u.Id)
            .ValueGeneratedOnAdd();
    }
}

