using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class ReviewStateConfiguration : IEntityTypeConfiguration<ReviewState>
    {
        public void Configure(EntityTypeBuilder<ReviewState> builder)
        {
            builder.ToTable(nameof(ReviewState));

            builder.HasKey(reviewState => reviewState.Id);
            builder.Property(reviewState => reviewState.State).IsRequired();
            builder.HasMany(reviewState => reviewState.Reviews).WithOne()
                .HasForeignKey(reviewState => reviewState.ReviewStateId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
