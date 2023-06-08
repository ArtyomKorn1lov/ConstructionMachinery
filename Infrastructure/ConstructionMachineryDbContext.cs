using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configurations;
using Domain.Entities;

namespace Infrastructure
{
    public class ConstructionMachineryDbContext : DbContext
    {
        public ConstructionMachineryDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new AdvertConfiguration());
            builder.ApplyConfiguration(new AvailabilityRequestConfiguration());
            builder.ApplyConfiguration(new AvailabilityStateConfiguration());
            builder.ApplyConfiguration(new AvailableTimeConfiguration());
            builder.ApplyConfiguration(new ImageConfiguration());
            builder.ApplyConfiguration(new RequestStateConfiguration());
            builder.ApplyConfiguration(new ReviewConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new ReviewStateConfiguration());
            builder.ApplyConfiguration(new MailConfiguration());

            builder.Entity<ReviewState>().HasData(
                new ReviewState
                {
                    Id = 1,
                    State = "1",
                },
                new ReviewState
                {
                    Id = 2,
                    State = "2",
                },
                new ReviewState
                {
                    Id = 3,
                    State = "3",
                },
                new ReviewState
                {
                    Id = 4,
                    State = "4",
                },
                new ReviewState
                {
                    Id = 5,
                    State = "5",
                });
            builder.Entity<RequestState>().HasData(
                new RequestState
                {
                    Id = 1,
                    State = "success"
                },
                new RequestState
                {
                    Id = 2,
                    State = "denied"
                },
                new RequestState
                {
                    Id = 3,
                    State = "expected"
                });
            builder.Entity<AvailabilityState>().HasData(
                new AvailabilityState
                {
                    Id = 1,
                    State = "free"
                },
                new AvailabilityState
                {
                    Id = 2,
                    State = "private"
                },
                new AvailabilityState
                {
                    Id = 3,
                    State = "expected"
                });
        }
    }
}
