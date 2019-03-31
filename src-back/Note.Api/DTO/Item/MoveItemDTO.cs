using System;

namespace Note.Api.DTO.Item
{
    public class MoveItemDTO
    {
        /// <summary>
        /// If not set, the item will stay in the same column.
        /// </summary>
        public Guid? ColumnId { get; set; }

        /// <summary>
        /// If not set, the item will be moved to the last position of the new column.
        /// </summary>
        public int? Position { get; set; }
    }
}
