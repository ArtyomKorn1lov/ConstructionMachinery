using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class RequestStateConfiguration : IEntityTypeConfiguration<RequestState>
    {
        public void Configure(EntityTypeBuilder<RequestState> builder)
        {
            builder.ToTable(nameof(RequestState));

            builder.HasKey(requestState => requestState.Id);
            builder.Property(requestState => requestState.State).IsRequired();
            builder.HasMany(requestState => requestState.AvailabilityRequests).WithOne()
                .HasForeignKey(availabilityRequest => availabilityRequest.RequestStateId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
