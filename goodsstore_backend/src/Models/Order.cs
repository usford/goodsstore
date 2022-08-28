using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ORDER")]
    public class Order
    {
        public Order(Guid customerId, DateTime orderDate)
        {
            Id = Guid.NewGuid();
            CustomerId = customerId;
            OrderDate = orderDate;
        }

        [Column("ID")]
        [Key]
        public Guid Id { get; set; }

        [Column("CUSTOMER_ID")]
        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("ORDER_DATE")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Column("SHIPMENT_DATE")]
        [DefaultValue(null)]
        public DateTime? ShipmentDate { get; set; }

        [Column("ORDER_NUMBER")]
        [DefaultValue(1)]
        public int? OrderNumber { get; set; }

        [Column("STATUS")]
        [DefaultValue("Новый")]
        public string? Status { get; set; }
    }
}
