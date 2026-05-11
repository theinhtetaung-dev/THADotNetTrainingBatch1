using Microsoft.EntityFrameworkCore;
using THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Model;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Student> Students { get; set; }
}
