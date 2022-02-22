using OnlineShop2022.Data;
using OnlineShop2022.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop2022.Models
{
    public class ProductRepository : IProductRepository
    {
        private AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ProductModel> Products
        {
            get
            {
                return _appDbContext.Products;
            }
        }

        public IEnumerable<ProductModel> GetAllProducts()
        {
            
            return _appDbContext.Products;
        }

        public ProductModel GetProductById(int ProductId)
        {
            return _appDbContext.Products.FirstOrDefault(s => s.Id == ProductId);
        }
    }
}
