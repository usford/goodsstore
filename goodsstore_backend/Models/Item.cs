using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ITEMS")]
    public class Item
    {
        public Item(string code, string name, decimal price, string category)
        {
            Id = Guid.NewGuid();
            Code = code;
            Name = name;
            Price = price;
            Category = category;
        }

        [Column("ID")]
        [Required]
        public Guid Id { get; set; }

        [Column("CODE")]
        [Required]
        public string Code { get; set; }

        [Column("NAME")]
        [Required]
        public string Name { get; set; }

        [Column("PRICE", TypeName = "decimal(18, 4)")]
        [Required]
        public decimal Price { get; set; }

        [Column("CATEGORY")]
        [Required]
        public string Category { get; set; }
    }
}
