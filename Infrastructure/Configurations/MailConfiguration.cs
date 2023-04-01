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
    public class MailConfiguration : IEntityTypeConfiguration<Mail>
    {
        public void Configure(EntityTypeBuilder<Mail> builder)
        {
            builder.ToTable(nameof(Mail));

            builder.HasKey(mail => mail.Id);
            builder.Property(mail => mail.Created).IsRequired();
            builder.Property(mail => mail.Name).IsRequired();
            builder.Property(mail => mail.Email).IsRequired();
            builder.Property(mail => mail.Phone).IsRequired();
            builder.Property(mail => mail.Description);
        }
    }
}
