using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.Specs
{
    public class ItemSpec
    {
        public ItemSpec(EntityTypeBuilder<Item> entityBuilder)
        {
            EntitySpec<Item>.SetEntitySpecs(entityBuilder);

            entityBuilder
                .Property(o => o.Name)
                .IsRequired()
                .HasMaxLength(100);

            entityBuilder
                .Property(o => o.Order)
                .IsRequired();
        }
    }
}
