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
    public class ProductControllerTests
    {
        private AppDbContext _db;
        private IWebHostEnvironment _webHostEnvironment;
        private Images _images;

        //Creates a Database that can be used within the Test Project, is essentially a snapshot of the actual database used in the 'OnlineShop2022' application 
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
            var productA = new ProductModel() { Id = 1, Description = "Test Product A"};
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


        [Fact] //Test to Check that the returned error for mismatching Product Id is handled correctly
        public async void ProductUpdateProductIDNotEqual()
        {
            //Arrange : Create the mock database, images and controller
            PopulateMockDB();
            _images = new Images(_webHostEnvironment);
            ProductController controller = new ProductController(_db, _webHostEnvironment, _images);

            //Create a mock product view model and pull first product in the database 
            ProductViewModel productViewModel = new ProductViewModel();
            var p = await _db.Products.ToListAsync();
            //Ensure the two products do not have maching IDs
            var mockProduct = p[1];
            productViewModel.Product = p[0];

            //Act : Produce a variable that can be assessed - the result of the attempted update with incorrect values
            var result = await controller.Update(mockProduct.Id, productViewModel) as NotFoundResult;

            //Assert : Check the error code returned for an mismatched update attempt, where the product Id does not match the Id within the product view model
            Assert.NotNull(result);
            Assert.Equal("404", result.StatusCode.ToString());
        }


        [Fact] //Test to Check that the returned error for a null request is handled correctly
        public async void ProductUpdateProductIDIsNull()
        {
            //Arrange : Create the mock database, images and controller
            PopulateMockDB();
            _images = new Images(_webHostEnvironment);
            ProductController controller = new ProductController(_db, _webHostEnvironment, _images);

            // Act : Produce a variable that can be assessed - the result of attempting to update with no passed values
            var result = await controller.Update(null) as NotFoundResult;

            //Assert : Check the error code returned for an null update attempt, where the product Id does not match the Id within the product view model
            Assert.NotNull(result);
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact] //Test to check if Products can be created
        public async void ProductControllerCreateProductSuccessful()
        {
            //Arrange : Create the mock database, images and controller
            CreateMockDB();
            _images = new Images(_webHostEnvironment);
            ProductController controller = new ProductController(_db, _webHostEnvironment, _images);

            //Produce product to be added
            ProductViewModel productViewModel = new ProductViewModel();
            ProductModel model = new ProductModel() { Id = 66, Description = "Product Created in Unit Test", Price = 22.99, Colour = "Blue", ImagePath = "imagePath" };
            productViewModel.Product = model;

            //Act : produce a variable that can be assessed - the returned result from passing the new product to the controller
            var result = await controller.Create(productViewModel);

            //Assert : Check the returned view from the function, if view is null then the action failed
            Assert.NotNull(result);


        }

        [Fact] //Test to check if Products can be deleted
        public async void ProductControllerDeleteProductSuccessful()
        {
            //Arrange : Create the mock database, images and controller
            PopulateMockDB();
            _images = new Images(_webHostEnvironment);
            ProductController controller = new ProductController(_db, _webHostEnvironment, _images);

            //Create local instance of the Products stored in the database
            var productList = await _db.Products.ToListAsync();
            //Get the details of an exisiting product to be used for the delete function
            var product = productList[0];

            //Act : produce a variable that can be assessed - the returned result of removing a product from the database
            var result = await controller.Delete(product.Id, product);

            //Assert : Check the returned view from the function, if view is null then the action failed
            Assert.NotNull(result);
        }
    }
}