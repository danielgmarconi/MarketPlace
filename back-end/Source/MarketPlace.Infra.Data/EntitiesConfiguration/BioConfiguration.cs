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
    internal class BioConfiguration : IEntityTypeConfiguration<Bio>
    {
        void IEntityTypeConfiguration<Bio>.Configure(EntityTypeBuilder<Bio> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.FullName).HasMaxLength(250).IsRequired();
            builder.Property(t => t.ProfilePhotoBase64).HasMaxLength(700);
            builder.Property(t => t.SexPerson).HasMaxLength(1);
            builder.Property(t => t.Description).HasMaxLength(700);
            builder.HasOne(p => p.User)
                   .WithOne(p => p.Bio)
                   .HasForeignKey<Bio>(p => p.UserId)
                   .IsRequired();
        }
    }
}
