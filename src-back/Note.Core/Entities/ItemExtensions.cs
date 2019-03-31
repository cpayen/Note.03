using System.Collections.Generic;
using System.Linq;

namespace Note.Core.Entities
{
    public static class ItemExtensions
    {
        public static List<Category> GetCategories(this Item item)
        {
            return item.ItemCategories.Select(o => o.Category).OrderBy(o => o.Name).ToList();
        }

        public static Item AddCategory(this Item item, Category category)
        {
            if(item.ItemCategories.Where(o => o.Category.Id == category.Id).Any())
            {
                return item;
            }

            item.ItemCategories.Add(new ItemCategory
            {
                Item = item,
                Category = category
            });

            return item;
        }

        public static Item RemoveCategory(this Item item, Category category)
        {
            item.ItemCategories.ToList().RemoveAll(o => o.Category.Id == category.Id);
            return item;
        }
    }
}
