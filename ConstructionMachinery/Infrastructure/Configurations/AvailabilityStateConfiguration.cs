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
    public class AvailabilityStateConfiguration : IEntityTypeConfiguration<AvailabilityState>
    {
        public void Configure(EntityTypeBuilder<AvailabilityState> builder)
        {
            builder.ToTable(nameof(AvailabilityState));

            builder.HasKey(availabilityRequest => availabilityRequest.Id);
            builder.Property(availabilityRequest => availabilityRequest.State).IsRequired();
            builder.HasMany(availabilityRequest => availabilityRequest.AvailableTimes).WithOne()
                .HasForeignKey(availableTime => availableTime.AvailabilityStateId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
