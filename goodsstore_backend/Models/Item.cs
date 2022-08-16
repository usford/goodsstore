using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ITEMS")]
    public class Item
    {
        [Column("ID")]
        public Guid Id { get; set; }

        [Column("CODE")]
        public string Code { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("PRICE")]
        public decimal Price { get; set; }

        [Column("CATEGORY")]
        public string Category { get; set; }
    }
}
