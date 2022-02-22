using OnlineShop2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop2022.Models
{
    public class ShoppingCartViewModel
    {
        public ShoppingCartModel ShoppingCart { get; set; }
        public double ShoppingCartTotal { get; set; }
    }
}
