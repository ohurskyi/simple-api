using Carter;

namespace Devices.Api.Endpoints.Devices;

public class DevicesModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        // Get all devices
        app.MapGet("/api/devices", GetAllDevicesEndpoint.Handler)
            .WithName("GetDevices")
            .WithOpenApi();

        // Get device by ID
        app.MapGet("/api/devices/{id}", GetDeviceByIdEndpoint.Handler)
            .WithName("GetDeviceById")
            .WithOpenApi();

        // Get device by MAC address
        app.MapGet("/api/devices/mac/{macAddress}", GetDeviceByMacAddressEndpoint.Handler)
            .WithName("GetDeviceByMacAddress")
            .WithOpenApi();
    }
}
