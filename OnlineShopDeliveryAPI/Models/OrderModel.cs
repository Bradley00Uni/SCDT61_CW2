using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopDeliveryAPI.Models
{
    public class OrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public List<ProductModel> Products { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }

        public DeliveryModel Delivery { get; set; }
    }
}
