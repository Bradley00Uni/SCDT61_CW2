using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShopDeliveryAPI.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public double Price { get; set; }
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public OrderModel Order { get; set; }
    }
}
