using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace goodsstore_backend.Models
{
    [Table("ITEMS")]
    public class Item
    {
        public Item()
        {
            Id = Guid.NewGuid();
            GenerateCode();
        }

        [Column("ID")]
        [Key]
        public Guid Id { get; set; }

        [Column("CODE")]
        [DisplayName("Код")]
        public string Code { get; set; } = "empty";

        [Column("NAME")]
        [DisplayName("Наименование")]
        [Required(ErrorMessage = "Не указано наименование товара")]
        public string Name { get; set; } = "noname";

        [Column("PRICE", TypeName = "decimal(18, 2)")]
        [DisplayName("Цена за шт.")]
        [Required(ErrorMessage = "Неправильная цена")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена должна быть больше 0")]
        public decimal Price { get; set; }

        [Column("CATEGORY")]
        [DisplayName("Категория")]
        public string? Category { get; set; } = null;

        public void GenerateCode()
        {
            var randomNumber = () => new Random().Next(1, 10);

            var randomUnicodeChar = () => (char)new Random().Next(65, 90);

            //формат кода XX-XXXX-YYXX, где X - число, а Y - заглавная буква алфавита
            string code = $"{randomNumber()}{randomNumber()}" +
                $"-{randomNumber()}{randomNumber()}{randomNumber()}{randomNumber()}" +
                $"-{randomUnicodeChar()}{randomUnicodeChar()}{randomNumber()}{randomNumber()}";

            Code = code;
        }
    }
}
