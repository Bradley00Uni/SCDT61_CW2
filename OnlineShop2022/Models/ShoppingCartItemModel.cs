using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop2022.Models
{
    public class ShoppingCartItemModel
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public ProductModel Product { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
