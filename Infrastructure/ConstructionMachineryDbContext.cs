using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Configurations;

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
        }
    }
}
