using System.Collections.Generic;

namespace OnlineShopDeliveryAPI.Models
{
    public class OrderDetailsModel
    {
        public OrderModel Order { get; set; }
        public DeliveryModel Delivery { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
