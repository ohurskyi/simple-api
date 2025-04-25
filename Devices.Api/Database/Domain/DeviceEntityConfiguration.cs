using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Devices.Api.Database.Domain;

public class DeviceEntityConfiguration : IEntityTypeConfiguration<DeviceEntity>
{
    public void Configure(EntityTypeBuilder<DeviceEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.MacAddress)
            .IsRequired()
            .HasMaxLength(17);  // Standard MAC address format: XX:XX:XX:XX:XX:XX

        builder.HasIndex(x => x.MacAddress)
            .IsUnique();  // Ensuring MAC addresses are unique
    }
}