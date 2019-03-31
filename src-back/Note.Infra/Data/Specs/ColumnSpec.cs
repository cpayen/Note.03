using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.Specs
{
    public class ColumnSpec
    {
        public ColumnSpec(EntityTypeBuilder<Column> entityBuilder)
        {
            EntitySpec<Column>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .Property(o => o.Order)
                .IsRequired();

            entityBuilder
                .HasMany(o => o.Items)
                .WithOne(o => o.Column);
        }
    }
}
