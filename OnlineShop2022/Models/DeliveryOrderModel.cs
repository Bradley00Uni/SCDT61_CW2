using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop2022.Models
{
    public class DeliveryOrderModel
    {
        [Key]
        public int OrderId { get; set; }

        public double OrderTotal { get; set; }

        public DateTime OrderPlaced { get; set; }

        public string OrderStatus { get; set; }

        public string UserID { get; set; }
    }
}
