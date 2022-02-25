using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop2022.Controllers;
using OnlineShop2022.Data;
using OnlineShop2022.Models;
using System;
using Xunit;


namespace UnitTests
{
    public class HomeControllerTests
    {
        private ILogger<HomeController> _logger;
        private AppDbContext _db;

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

        [Fact]
        public void HomeControllerIndexNotNull() //Test to check the Index area of the Home Controller is not null
        {
            //Arrange : Create the mock database and controller
            CreateMockDB();
            var controller = new HomeController(_logger, _db);

            //Act : Produce a variable that can be assessed
            var result = controller.Index();

            //Assert : Test if the produced result matches the criteria for a successful test
            Assert.NotNull(result);
        }

        [Fact] //Test to check the Privacy area of the Home Controller is not null
        public void HomeControllerPrivacyNotNull()
        {
            //Arrange : Create the mock controller
            var controller = new HomeController(_logger, _db);

            //Act : Produce a variable that can be assessed
            var result = controller.Privacy();

            //Assert : Test if the produced result matches the criteria for a successful test
            Assert.NotNull(result);
        }

        [Fact] //Test to check the populated Product Database can be successfully retrieved within the Home Controller
        public async void HomeControllerGetProductsSuccess()
        {
            //Arrange : Create and Populate the mock database
            PopulateMockDB();

            //Act : Produce a variable that can be assessed
            var result = await _db.Products.ToListAsync(); //Asynchronus attempt to pull all products from the database into a readable list

            //Assert : Test if the produced list is not null
            Assert.NotNull(result);
        }
    }
}
