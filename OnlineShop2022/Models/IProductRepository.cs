using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop2022.Models
{
    public interface IProductRepository
    {
        IEnumerable<ProductModel> Products { get; }
        IEnumerable<ProductModel> GetAllProducts();

        ProductModel GetProductById(int ProductId);
    }
}
