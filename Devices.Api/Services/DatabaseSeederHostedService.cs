using Devices.Api.Database;
using Devices.Api.Database.Domain;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Services;

public class DatabaseSeederHostedService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<DatabaseSeederHostedService> _logger;

    public DatabaseSeederHostedService(
        IServiceProvider serviceProvider,
        ILogger<DatabaseSeederHostedService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            _logger.LogInformation("Ensuring database is created and migrations are applied...");
            await dbContext.Database.MigrateAsync(cancellationToken);

            if (await dbContext.Devices.AnyAsync(cancellationToken))
            {
                _logger.LogInformation("Database already contains data. Skipping seeding.");
                return;
            }

            _logger.LogInformation("Seeding database...");
            var devices = GenerateDevices();
            await dbContext.Devices.AddRangeAsync(devices, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Database seeding completed successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private static IEnumerable<DeviceEntity> GenerateDevices()
    {
        var deviceTypes = new[] { "Router", "Switch", "Access Point", "Camera", "Sensor" };
        var locations = new[] { "Office", "Warehouse", "Meeting Room", "Lab", "Reception" };
        var manufacturers = new[] { "Cisco", "HP", "Dell", "Ubiquiti", "Aruba" };

        var devices = new List<DeviceEntity>();
        
        for (int i = 1; i <= 20; i++)
        {
            var deviceType = deviceTypes[Random.Shared.Next(deviceTypes.Length)];
            var location = locations[Random.Shared.Next(locations.Length)];
            var manufacturer = manufacturers[Random.Shared.Next(manufacturers.Length)];
            
            devices.Add(new DeviceEntity
            {
                Id = Guid.NewGuid(),
                Name = $"{manufacturer} {deviceType} {i}",
                Description = $"{deviceType} located in {location}",
                MacAddress = GenerateRandomMacAddress()
            });
        }

        return devices;
    }

    private static string GenerateRandomMacAddress()
    {
        var random = Random.Shared;
        var mac = new byte[6];
        random.NextBytes(mac);
        
        // Ensure it's a unicast address
        mac[0] &= 0xFE;
        
        return string.Join(":", mac.Select(b => b.ToString("X2")));
    }
}