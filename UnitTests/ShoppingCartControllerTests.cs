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
using System.Collections.Generic;
using System.Linq;
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
            var productA = new ProductModel() { Id = 1, Description = "Test Product A", Price = 10 };
            var productB = new ProductModel() { Id = 2, Description = "Test Product B", Price = 20 };
            var productC = new ProductModel() { Id = 3, Description = "Test Product C", Price = 30 };
            var productD = new ProductModel() { Id = 4, Description = "Test Product D", Price = 40 };
            var productE = new ProductModel() { Id = 5, Description = "Test Product E", Price = 50 };

            //Adds these mock products to the database
            ProductModel[] p = new ProductModel[] { productA, productB, productC, productD, productE };
            foreach (var product in p)
            {
                _db.Products.Add(product);
            }

            await _db.SaveChangesAsync(); //save changes to the database
        }

        private void CreateMockRepository() //When called, the Product Repository and Shopping Cart needed for handling database modification by the Shopping Cart Controller are instantiated
        {
            _ProductRepository = new ProductRepository(_db);
            _shoppingCart = new ShoppingCartModel(_db);
        }

        private async void PopulateMockRepository(ShoppingCartController controller) //When Called, the mock Shopping Cart is populated with products from the database
        {
            //Adds all products created in the PopulateMockDB funtion to the Shopping Cart
            var products = await _db.Products.ToListAsync();

            foreach (var p in products)
            {
                controller.AddToShoppingCart(p.Id);
            }
        }

        [Fact] //Test to check new products can be added to the user's shopping cart
        private async void ShoppingCartAddToCartSuccessfull() 
        {
            //Arrange : Creates and Populates the Database, Repository and Shopping Cart needed by the Controller
            PopulateMockDB();
            CreateMockRepository();
            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            var products = await _db.Products.ToListAsync();

            //Creates a new Product type, which is subsequently added to the database so that it can be referenced by methods in the Controller
            var toAdd = new ProductModel() { Id = 6, Description = "New Product", Price = 4.99 };
            _db.Products.Add(toAdd);
            PopulateMockRepository(controller);

            //Act : Produces a variable that can be assessed - a local instance of the shopping cart after attempting to add a product
            controller.AddToShoppingCart(toAdd.Id);
            var newCart = _shoppingCart.GetShoppingCartItems();

            //Assert : The Test Passes if the Product Count in the Cart is one higher than the list of Products. As by default the number of products is five, an increase means that a new product is present in the Cart
            Assert.Equal((products.Count + 1), newCart.Count);
        }

        [Fact] //Test to check products can be removed from the user's shopping cart
        private async void ShoppingCartRemoveFromCartSuccessfull()
        {
            //Arrange : Creates and Populates the Database, Repository and Shopping Cart needed by the Controller
            PopulateMockDB();
            CreateMockRepository();

            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            PopulateMockRepository(controller);

            //Takes a product from the Database to be removed from the user's cart. As the product list is additionally used to populate the Product Repository, this variable is guaranteed to match one present in the mock Shopping Cart
            var products = await _db.Products.ToListAsync();
            var toRemove = products[2].Id;

            //Act : Produces a variable that can be assessed - a local instance of the shopping cart after attempting to remove a product
            controller.RemoveFromShoppingCart(toRemove);
            var newCart = _shoppingCart.GetShoppingCartItems();

            //Assert : The Test Passes if the Product Count in the Cart is one less than the list of Products. This means that a new product has been successfully removed, shown by less instances in the new cart when compared to the original count
            Assert.Equal((products.Count - 1), newCart.Count);
        }

        [Fact] //Test to check a user's shopping cart can be cleared/emptied
        private void ShoppingCartClearCartSuccessfull()
        {
            //Arrange : Creates and Populates the Database, Repository and Shopping Cart needed by the Controller
            PopulateMockDB();
            CreateMockRepository();

            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            PopulateMockRepository(controller);

            //Act : Produce a variable that can be assessed - the result of the attempt to clear the cart
            var result = controller.ClearCart();

            //Assert : Check that a response is sent by the method to clear the cart. If a result is present, then the function executed successfully
            Assert.NotNull(result);
        }

        [Fact] //Test to check the total calculated from the user's shopping cart is accurate
        private async void ShoppingCartTotalIsAccurate()
        {
            //Arrange : Creates and Populates the Database, Repository and Shopping Cart needed by the Controller
            PopulateMockDB();
            CreateMockRepository();

            ShoppingCartController controller = new ShoppingCartController(_ProductRepository, _shoppingCart);
            PopulateMockRepository(controller);

            //Instantiates a new Shopping Cart model so that the assosciated method for calcualting the total can be accessed
            var model = new ShoppingCartModel(_db) { ShoppingCartId = _shoppingCart.ShoppingCartId, ShoppingCartItems = _shoppingCart.ShoppingCartItems };

            var products = await _db.Products.ToListAsync();
            List<double> prices = new List<double>();
            
            //Act : Produces two variables that can be assessed - The expectedTotal variable is shows what the order total should be from the products in the cart, whilst the result variable is the outcome of the attempt to retrieve the total from the method
            foreach (var p in products) { prices.Add(p.Price); }
            var expectedTotal = prices.Sum();

            var result = model.GetShoppingCartTotal();

            //Assert : Checks that the total returned matches the expected outcome - The test will pass if the two values are equal
            Assert.Equal(expectedTotal, result);
        }
    }
}
