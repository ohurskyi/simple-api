using Devices.Api.Database;
using Devices.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Endpoints.Devices;

public static class GetAllDevicesEndpoint
{
    public static async Task<IEnumerable<DeviceDto>> Handler(ApplicationDbContext db)
    {
        return await db.Devices
            .Select(d => d.ToDto())
            .ToListAsync();
    }
}
