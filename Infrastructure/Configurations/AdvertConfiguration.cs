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
    public class AdvertConfiguration : IEntityTypeConfiguration<Advert>
    {
        public void Configure(EntityTypeBuilder<Advert> builder)
        {
            builder.ToTable(nameof(Advert));

            builder.HasKey(advert => advert.Id);
            builder.Property(advert => advert.Name).IsRequired();
            builder.Property(advert => advert.Description);
            builder.Property(advert => advert.Price).IsRequired();
            builder.HasMany(advert => advert.Images).WithOne().HasForeignKey(image => image.AdvertId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(advert => advert.AvailableTimes).WithOne().HasForeignKey(availableTime => availableTime.AdvertId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(advert => advert.Reviews).WithOne().HasForeignKey(review => review.AdvertId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
