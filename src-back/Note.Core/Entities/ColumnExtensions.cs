using System.Collections.Generic;
using System.Linq;

namespace Note.Core.Entities
{
    public static class ColumnExtensions
    {
        public static List<Item> GetItems(this Column column)
        {
            return column.Items.OrderBy(o => o.Order).ToList();
        }

        //public static Column AddItem(this Column column, string itemName)
        //{
        //    var itemOrder = column.Items.Count;
        //    var newItem = new Item
        //    {
        //        Name = itemName,
        //        Order = itemOrder
        //    };

        //    column.Items.Add(newItem);
        //    return column;
        //}

        //public static Column AddItemAtPosition(this Column column, string itemName, int order)
        //{
        //    var newItem = new Item
        //    {
        //        Name = itemName,
        //        Order = order
        //    };

        //    foreach (var item in column.Items.Where(o => o.Order >= order))
        //    {
        //        item.Order++;
        //    }

        //    column.Items.Add(newItem);
        //    return column;
        //}
    }
}
