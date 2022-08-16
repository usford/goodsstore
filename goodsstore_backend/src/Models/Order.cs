using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ORDER")]
    public class Order
    {
        public Order(Guid id, Guid customerId, DateTime orderDate)
        {
            Id = id;
            CustomerId = customerId;
            OrderDate = orderDate;
        }

        [Column("ID")]
        [Required]
        public Guid Id { get; set; }

        [Column("CUSTOMER_ID")]
        [Required]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("ORDER_DATE")]
        [Required]
        public DateTime OrderDate { get; set; }

        [Column("SHIPMENT_DATE")]
        [Required]
        public DateTime? ShipmentDate { get; set; }

        [Column("ORDER_NUMBER")]
        public int? OrderNumber { get; set; }

        [Column("STATUS")]
        public string? Status { get; set; }
    }
}
