using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace goodsstore_backend.Models
{
    [Table("CUSTOMER")]
    public class Customer
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("CODE")]
        public string Code { get; set; }

        [Column("ADDRESS")]
        public string Address { get; set; }

        [Column("DISCOUNT")]
        public byte? Discount { get; set; }
    }
}
