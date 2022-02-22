using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace OnlineShop2022.Models
{
    public class ProductViewModel
    {
        public ProductModel Product { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}
