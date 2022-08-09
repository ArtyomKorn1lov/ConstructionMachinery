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
    public class AvailableTimeConfiguration : IEntityTypeConfiguration<AvailableTime>
    {
        public void Configure(EntityTypeBuilder<AvailableTime> builder)
        {
            builder.ToTable(nameof(AvailableTime));

            builder.HasKey(availableTime => availableTime.Id);
            builder.Property(availableTime => availableTime.Date).IsRequired();
        }
    }
}
