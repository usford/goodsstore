using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ORDER_ELEMENT")]
    public class OrderElement
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("ORDER_ID")]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Column("ITEM_ID")]
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        [Column("ITEMS_COUNT")]
        public int ItemsCount { get; set; }

        [Column("ITEM_PRIcE")]
        public decimal ItemPrice { get; set; }
    }
}
