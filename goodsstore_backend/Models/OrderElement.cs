using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ORDER_ELEMENT")]
    public class OrderElement
    {
        public OrderElement()
        {
            Id = Guid.NewGuid();
        }

        [Column("ID")]
        [Key]
        public Guid Id { get; set; }

        [Column("ORDER_ID")]
        [DisplayName("Заказ")]
        [Required]
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        [Column("ITEM_ID")]
        [DisplayName("Товар")]
        [Required]
        public Guid ItemId { get; set; }
        public Item Item { get; set; }

        [Column("ITEMS_COUNT")]
        [DisplayName("Количество товара")]
        [Required]
        public int ItemsCount { get; set; }

        [Column("ITEM_PRICE", TypeName = "decimal(18, 4)")]
        [DisplayName("Цена за шт.")]
        [Required]
        public decimal ItemPrice { get; set; }
    }
}
