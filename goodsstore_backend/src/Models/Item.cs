using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ITEMS")]
    public class Item
    {
        public Item(Guid id, string code)
        {
            Id = id;
            Code = code;
        }

        [Column("ID")]
        [Required]
        public Guid Id { get; set; }

        [Column("CODE")]
        [Required]
        [RegularExpression(@"^\d{2}-\d{4}-[A-Z]{2}\d{2}$", ErrorMessage = "Неверный формат кода")]
        public string Code { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }

        [Column("PRICE", TypeName = "decimal(18, 4)")]
        public decimal? Price { get; set; }

        [Column("CATEGORY")]
        public string? Category { get; set; }
    }
}
