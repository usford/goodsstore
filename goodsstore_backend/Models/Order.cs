using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ORDER")]
    public class Order
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("CUSTOMER_ID")]
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column("ORDER_DATE")]
        public DateTime OrderDate { get; set; }

        [Column("SHIPMENT_DATE")]
        public DateTime ShipmentDate { get; set; }

        [Column("ORDER_NUMBER")]
        public int OrderNumber { get; set; }

        [Column("STATUS")]
        public string Status { get; set; }
    }
}
