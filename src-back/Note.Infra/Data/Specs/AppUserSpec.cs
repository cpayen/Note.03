using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.Specs
{
    public class AppUserSpec
    {
        public AppUserSpec(EntityTypeBuilder<AppUser> entityBuilder)
        {
            EntitySpec<AppUser>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .HasIndex(o => o.UserName)
                .IsUnique();

            entityBuilder
                .Property(o => o.UserName)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .Property(o => o.FirstName)
                .HasMaxLength(250);

            entityBuilder
                .Property(o => o.LastName)
                .HasMaxLength(250);

            entityBuilder
                .Property(o => o.Email)
                .HasMaxLength(250);

            entityBuilder
                .Property(o => o.Salt)
                .IsRequired();

            entityBuilder
                .Property(o => o.Password)
                .IsRequired();
        }
    }
}
