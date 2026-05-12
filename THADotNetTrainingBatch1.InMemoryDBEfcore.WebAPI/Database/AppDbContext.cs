using Microsoft.EntityFrameworkCore;

namespace THADotNetTrainingBatch1.InMemoryDBEfcore.WebAPI.Database;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Tbl_Students> Students { get; set; }
}
