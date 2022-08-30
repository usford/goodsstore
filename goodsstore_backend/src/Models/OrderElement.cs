using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ORDER_ELEMENT")]
    public class OrderElement
    {
        public OrderElement(Guid orderId, Guid itemId, int itemsCount, decimal itemPrice)
        {
            Id = Guid.NewGuid();
            OrderId = orderId;
            ItemId = itemId;
            ItemsCount = itemsCount;
            ItemPrice = itemPrice;
        }

        [Column("ID")]
        [Required]
        public Guid Id { get; set; }

        [Column("ORDER_ID")]
        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Column("ITEM_ID")]
        [Required]
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        [Column("ITEMS_COUNT")]
        [Required]
        public int ItemsCount { get; set; }

        [Column("ITEM_PRICE", TypeName = "decimal(18, 4)")]
        [Required]
        public decimal ItemPrice { get; set; }
    }
}
