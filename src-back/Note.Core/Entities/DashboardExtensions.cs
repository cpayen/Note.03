using System.Collections.Generic;
using System.Linq;

namespace Note.Core.Entities
{
    public static class DashboardExtensions
    {
        public static List<Category> GetCategories(this Dashboard dashboard)
        {
            return dashboard.Categories.OrderBy(o => o.Name).ToList();
        }

        public static List<Column> GetColumns(this Dashboard dashboard)
        {
            return dashboard.Columns.OrderBy(o => o.Order).ToList();
        }

        public static Dashboard AddColumn(this Dashboard dashboard, Column column)
        {
            var columnOrder = dashboard.Columns.Count;
            column.Order = columnOrder;

            dashboard.Columns.Add(column);
            return dashboard;
        }

        public static Dashboard AddColumnAtPosition(this Dashboard dashboard, Column column, int order)
        {
            foreach (var col in dashboard.Columns.Where(o => o.Order >= order))
            {
                col.Order++;
            }

            column.Order = order;
            dashboard.Columns.Add(column);
            return dashboard;
        }
    }
}
