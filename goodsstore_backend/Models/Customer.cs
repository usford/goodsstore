using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("CUSTOMERS")]
    public class Customer
    {
        public Customer()
        {
            Id = Guid.NewGuid();
            GenerateCode();
        }

        [Column("ID")]
        [Key]
        public Guid Id { get; set; }

        [Column("NAME")]
        [Required]
        public string Name { get; set; } = "noname";

        [Column("CODE")]
        public string Code { get; set; } = "empty";

        [Column("ADDRESS")]
        [DefaultValue(null)]
        public string? Address { get; set; } = null;

        [Column("DISCOUNT")]
        [DefaultValue(0)]
        public byte Discount { get; set; } = 0;

        private void GenerateCode()
        {
            //Формат XXXX-ГГГГ, где X - число, а ГГГГ - год
            string leftSideCode = new Random().Next(0, 10000).ToString("0000");
            string rightSideCode = DateTime.Now.Year.ToString();
            Code = $"{leftSideCode}-{rightSideCode}";
        }
    }
}
