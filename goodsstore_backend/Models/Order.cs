using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using goodsstore_backend.Enums;

namespace goodsstore_backend.Models
{
    [Table("ORDER")]
    public class Order
    {
        public Order()
        {
            Id = Guid.NewGuid();
        }

        [Column("ID")]
        [Key]
        public Guid Id { get; set; }

        [Column("CUSTOMER_ID")]
        [DisplayName("Заказчик")]
        [Required(ErrorMessage = "Не выбран заказчик")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("ORDER_DATE")]
        [DisplayName("Дата создания заказа")]
        [DataType(DataType.Date, ErrorMessage = "Некорректная дата")]
        [Required(ErrorMessage = "Не выбрана дата создания заказа")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column("SHIPMENT_DATE")]
        [DisplayName("Дата доставки")]
        [DataType(DataType.Date, ErrorMessage = "Некорректная дата")]
        [DefaultValue(null)]
        public DateTime? ShipmentDate { get; set; }

        [Column("ORDER_NUMBER")]
        [DefaultValue(1)]
        public int OrderNumber { get; set; } = 1;

        [Column("STATUS")]
        [DisplayName("Статус")]
        [DefaultValue(OrderStatus.New)]
        public OrderStatus Status { get; set; } = OrderStatus.New;
    }
}
