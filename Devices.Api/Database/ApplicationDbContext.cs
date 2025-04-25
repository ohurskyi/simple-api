using Devices.Api.Database.Domain;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Database;

public class ApplicationDbContext : DbContext
{
    public DbSet<DeviceEntity> Devices { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new DeviceEntityConfiguration());
    }
}