using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("CUSTOMERS")]
    public class Customer
    {
        public Customer(string name, string code)
        {
            Id = Guid.NewGuid();
            Name = name;
            Code = code;
        }

        [Column("ID")]
        [Key]
        public Guid Id { get; set; }

        [Column("NAME")]
        [Required]
        public string Name { get; set; }

        [Column("CODE")]
        [Required]
        public string Code { get; set; }

        [Column("ADDRESS")]
        [DefaultValue(null)]
        public string? Address { get; set; }

        [Column("DISCOUNT")]
        [DefaultValue(0)]
        public byte? Discount { get; set; }
    }
}
