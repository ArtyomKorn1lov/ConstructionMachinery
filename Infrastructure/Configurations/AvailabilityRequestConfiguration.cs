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
            builder.Property(availabilityRequest => availabilityRequest.Created).IsRequired();
            builder.Property(availabilityRequest => availabilityRequest.Updated);
            builder.Property(availabilityRequest => availabilityRequest.Address).IsRequired();
            builder.Property(availabilityRequest => availabilityRequest.Conditions);
            builder.Property(availabilityRequest => availabilityRequest.Sum).IsRequired();
            builder.Property(availabilityRequest => availabilityRequest.IsAvailable).IsRequired();
            builder.HasMany(availabilityRequest => availabilityRequest.AvailableTimes).WithOne()
                .HasForeignKey(availableTime => availableTime.AvailabilityRequestId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
