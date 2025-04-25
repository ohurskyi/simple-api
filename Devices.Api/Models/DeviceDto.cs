using Devices.Api.Database.Domain;

namespace Devices.Api.Models;

public record DeviceDto(Guid Id, string Name, string Description, string MacAddress);

public static class DeviceExtensions
{
    public static DeviceDto ToDto(this DeviceEntity entity) =>
        new(entity.Id, entity.Name, entity.Description, entity.MacAddress);
}
