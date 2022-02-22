using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OnlineShop2022.Models;


namespace OnlineShop2022.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _ProductRepository;
        private readonly ShoppingCartModel _shoppingCart;

        public ShoppingCartController(IProductRepository ProductRepository, ShoppingCartModel shoppingCart)
        {
            _ProductRepository = ProductRepository;
            _shoppingCart = shoppingCart;
        }

        public ViewResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

        public RedirectToActionResult AddToShoppingCart(int ProductId)
        {
            var selectedProduct = _ProductRepository.Products.FirstOrDefault(s => s.Id == ProductId);

            if (selectedProduct != null)
            {
                _shoppingCart.AddToCart(selectedProduct, 1);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult RemoveFromShoppingCart(int ProductId)
        {
            var selectedProduct = _ProductRepository.Products.FirstOrDefault(s => s.Id == ProductId); 

            if (selectedProduct != null)
            {
                _shoppingCart.RemoveFromCart(selectedProduct);
            }

            return RedirectToAction("Index");
        }

        public RedirectToActionResult ClearCart()
        {
            _shoppingCart.ClearCart();
        
            return RedirectToAction("Index");
        }


    }
}
