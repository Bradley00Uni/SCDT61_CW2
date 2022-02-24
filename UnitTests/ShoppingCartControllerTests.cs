using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using OnlineShop2022.Helpers;
using OnlineShop2022.Models;
using System;
using Xunit;

namespace UnitTests
{
    public class ShoppingCartControllerTests
    {
        private IProductRepository _ProductRepository;
        private ShoppingCartModel _shoppingCart;

        private AppDbContext _db;

        private void CreateMockDB()
        {
            //Stores the new database context in the "InMemoryDatabase" imported into the project using the NuGet Package Manager
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _db = new AppDbContext(options);
            _db.Database.EnsureCreated(); //Ensures the Database has been correctly created before ending the function

        }
        private async void PopulateMockDB() //When called, this function creates falsified product models that can be called in the test cases
        {
            CreateMockDB(); //Calls the function to create a mock database

            //Five products, that have basic information in the required ID and Description fields
            var productA = new ProductModel() { Id = 1, Description = "Test Product A" };
            var productB = new ProductModel() { Id = 2, Description = "Test Product B" };
            var productC = new ProductModel() { Id = 3, Description = "Test Product C" };
            var productD = new ProductModel() { Id = 4, Description = "Test Product D" };
            var productE = new ProductModel() { Id = 5, Description = "Test Product E" };

            //Adds these mock products to the database
            ProductModel[] p = new ProductModel[] { productA, productB, productC, productD, productE };
            foreach (var product in p)
            {
                _db.Products.Add(product);
            }

            await _db.SaveChangesAsync(); //save changes to the database
        }

        private void CreateMockRepository()
        {
            _ProductRepository = new ProductRepository(_db);
            _shoppingCart = new ShoppingCartModel(_db);
        }

        private async void PopulateMockRepository(ShoppingCartController controller)
        {
            var products = await _db.Products.ToListAsync();

            foreach (var p in products)
            {
                controller.AddToShoppingCart(p.Id);
            }
        }

        [Fact]
        private async void ShoppingCartAddToCartSuccessfull()
        {
            PopulateMockDB();
            CreateMockRepository();
            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            var products = await _db.Products.ToListAsync();
            var toAdd = new ProductModel() { Id = 6, Description = "New Product", Price = 4.99 };
            _db.Products.Add(toAdd);
            PopulateMockRepository(controller);

            controller.AddToShoppingCart(toAdd.Id);

            var newCart = _shoppingCart.GetShoppingCartItems();

            Assert.Equal((products.Count + 1), newCart.Count);
        }

        [Fact]
        private async void ShoppingCartRemoveFromCartSuccessfull()
        {
            PopulateMockDB();
            CreateMockRepository();

            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            PopulateMockRepository(controller);

            var products = await _db.Products.ToListAsync();
            var toRemove = products[2].Id;

            controller.RemoveFromShoppingCart(toRemove);

            var newCart = _shoppingCart.GetShoppingCartItems();

            Assert.Equal((products.Count - 1), newCart.Count);
        }

        [Fact]
        private void ShoppingCartClearCartSuccessfull()
        {
            PopulateMockDB();
            CreateMockRepository();

            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            PopulateMockRepository(controller);

            var result = controller.ClearCart();

            Assert.NotNull(result);
        }
    }
}
