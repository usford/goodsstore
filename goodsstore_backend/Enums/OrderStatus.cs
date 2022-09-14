using System.ComponentModel.DataAnnotations;

namespace goodsstore_backend.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Новый")]
        New,
        [Display(Name = "Выполняется")]
        Performed,
        [Display(Name = "Завершен")]
        Completed
    }
}
