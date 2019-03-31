using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.Specs
{
    public class CategorySpec
    {
        public CategorySpec(EntityTypeBuilder<Category> entityBuilder)
        {
            EntitySpec<Category>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
