using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Note.Core.Entities;

namespace Note.Infra.Data.Specs
{
    public class ItemCategorySpec
    {
        public ItemCategorySpec(EntityTypeBuilder<ItemCategory> entityBuilder)
        {
            entityBuilder
                .HasKey(o => new { o.ItemId, o.CategoryId });
            
            entityBuilder
                .HasOne(o => o.Item)
                .WithMany(o => o.ItemCategories)
                .HasForeignKey(o => o.ItemId);

            entityBuilder
                .HasOne(o => o.Category)
                .WithMany(o => o.ItemCategories)
                .HasForeignKey(o => o.CategoryId);
        }
    }
}
