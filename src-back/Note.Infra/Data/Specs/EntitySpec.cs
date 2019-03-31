using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities.Base;

namespace Note.Infra.Data.Specs
{
    internal class EntitySpec<T> where T : Entity
    {
        public static void SetEntitySpecs(EntityTypeBuilder<T> entityBuilder)
        {
            entityBuilder
                .HasKey(o => o.Id);

            entityBuilder
                .Property(o => o.Id)
                .ValueGeneratedOnAdd();

            entityBuilder
                .Property(o => o.CreatedAt)
                .IsRequired();

            entityBuilder
                .Property(o => o.CreatedBy)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .Property(o => o.UpdatedBy)
                .HasMaxLength(100);
        }
    }
}
