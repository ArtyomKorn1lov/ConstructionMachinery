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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.Property(user => user.Name).IsRequired();
            builder.Property(user => user.Email).IsRequired();
            builder.Property(user => user.Phone).IsRequired();
            builder.Property(user => user.Password).IsRequired();
            builder.HasMany(user => user.Adverts).WithOne().HasForeignKey(adverts => adverts.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.AvailabilityRequests).WithOne()
                .HasForeignKey(availabilityRequest => availabilityRequest.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(user => user.Reviews).WithOne().HasForeignKey(review => review.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
