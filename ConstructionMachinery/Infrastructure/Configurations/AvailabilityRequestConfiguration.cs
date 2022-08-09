using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class AvailabilityRequestConfiguration : IEntityTypeConfiguration<AvailabilityRequest>
    {
        public void Configure(EntityTypeBuilder<AvailabilityRequest> builder)
        {
            builder.ToTable(nameof(AvailabilityRequest));

            builder.HasKey(availabilityRequest => availabilityRequest.Id);
            builder.Property(availabilityRequest => availabilityRequest.Address).IsRequired();
            builder.HasMany(availabilityRequest => availabilityRequest.AvailableTimes).WithOne()
                .HasForeignKey(availableTime => availableTime.AvailabilityRequestId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
