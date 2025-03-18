using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Infra.Data.EntitiesConfiguration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(t =>  t.Id);
            builder.Property(t => t.Name).HasMaxLength(200).IsRequired();
            builder.Property(t =>t.Email).HasMaxLength(250).IsRequired();
            builder.Property(t => t.Password).HasMaxLength(1500).IsRequired();
            builder.Property(t => t.IsAdmin).IsRequired();
            builder.Property(t => t.IsActive).IsRequired();
        }
    }
}
