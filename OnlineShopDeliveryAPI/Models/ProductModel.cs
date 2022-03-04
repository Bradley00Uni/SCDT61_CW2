namespace OnlineShopDeliveryAPI.Models
{
    public class ProductModel
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string Price { get; set; }
    }
}
