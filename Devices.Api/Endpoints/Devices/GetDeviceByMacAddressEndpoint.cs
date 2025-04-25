using Devices.Api.Database;
using Devices.Api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace Devices.Api.Endpoints.Devices;

public static class GetDeviceByMacAddressEndpoint
{
    public static async Task<Results<NotFound, Ok<DeviceDto>>> Handler(
        string macAddress, 
        ApplicationDbContext db)
    {
        var device = await db.Devices
            .Where(d => d.MacAddress == macAddress)
            .Select(d => d.ToDto())
            .FirstOrDefaultAsync();
        
        return device is null ? TypedResults.NotFound() : TypedResults.Ok(device);
    }
}
