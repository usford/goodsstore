using System.ComponentModel.DataAnnotations;

namespace goodsstore_backend.Models.Attributes
{
    public class CorrectShipmentDate : ValidationAttribute
    {
        DateTime _orderDate;
        public CorrectShipmentDate(DateTime orderDate)
        {
            _orderDate = orderDate;
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime shipmentDate)
            {
                return shipmentDate > _orderDate;
            }
            return false;
        }
    }
}
