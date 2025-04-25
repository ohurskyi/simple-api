namespace Devices.Api.Database.Domain;

public class DeviceEntity
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public required string MacAddress { get; set; }
}