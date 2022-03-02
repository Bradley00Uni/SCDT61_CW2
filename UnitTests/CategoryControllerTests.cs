using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop2022.Areas.Admin;
using OnlineShop2022.Data;
using OnlineShop2022.Models;
using System;
using Xunit;

namespace UnitTests
{
    public class CategoryControllerTests
    {
        private AppDbContext _db;
        private CategoryModel _existingCategory; //Variable used for storing existing category in Mock database for quick refrence in tests

        private void CreateMockDB()
        {
            //Stores the new database context in the "InMemoryDatabase" imported into the project using the NuGet Package Manager
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _db = new AppDbContext(options);
            _db.Database.EnsureCreated(); //Ensures the Database has been correctly created before ending the function
        }

        private async void PopulateMockDB()
        {
            CreateMockDB(); //Calls the function to create a mock database

            //Three Categories, containing basic information, are all added to the Mock database
            var CategoryA = new CategoryModel() { Id = 1, Name = "Test Category A" };
            var CategoryB = new CategoryModel() { Id = 2, Name = "Test Category B" };
            var CategoryC = new CategoryModel() { Id = 3, Name = "Test Category C" };

            CategoryModel[] c = new CategoryModel[] { CategoryA, CategoryB, CategoryC };

            _db.Categories.AddRange(c);
            await _db.SaveChangesAsync();
        }

        private async void SetExistingCategory(int id) //Function that when called, sets the _existingCategory variable to a specified instance from the database
        {
            var categories = await _db.Categories.ToListAsync();
            _existingCategory = categories.Find(x => x.Id == id); //Sets the variable based on passed ID
        }


        [Fact] //Test to check the Index area of the Category Controller is not null
        public void CategoryControllerIndexNotNull()
        {
            //Arrange : Create the mock database and controller
            CreateMockDB();
            var controller = new CategoryController(_db);

            //Act : Produce a variable that can be assessed - the result of accessing the controller's index
            var result = controller.Index();

            //Assert : Check that the total returned matches the expected outcome - The test will pass if the returned value is not null
            Assert.NotNull(result);
        }

        [Fact] //Test to check the details of a category can be retrievd through the controller
        public async void CategoryControllerDetailsRetrievalSuccessful()
        {
            //Arrange : Create the mock database and controller
            PopulateMockDB();
            var controller = new CategoryController(_db);

            //Act : Produce a variable that can be assessed - the result of attempting to view details of category in database, based on ID
            var result = await controller.Details(1) as ViewResult;
            SetExistingCategory(1);

            //Assert : Check the reuruned data is correct, comparing the model within the returned model to the actual database entry
            Assert.Equal(result.Model, _existingCategory);
        }

        [Fact] //Test to validate controller correctly handles error from attempting to view detail of non-existent category
        public async void CategoryControllerDetailsIDNotEqual()
        {
            //Arrange : Create the mock database and controller
            PopulateMockDB();
            var controller = new CategoryController(_db);

            //Act : Produce a variable that can assessed - the result of attempting to view details with incorrect ID
            var result = await controller.Details(0) as NotFoundResult;

            //Assert : Check the result matches the expected outcome, with status code 404 indicating the category could not be found
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact] //Test to check if a new category can be added to the database through the controller
        public async void CategoryControllerCreateSuccessful()
        {
            //Arrange : Create the mock database and controller
            CreateMockDB();
            var controller = new CategoryController(_db);

            //Sets details of the category to be created
            var newCategory = new CategoryModel() { Id = 30, Name = "Created Product"};

            //Act: Produce a variable that can be assessed - the result of attempting to create a new category
            var result = await controller.Create(newCategory);

            //Assert : Check the result matches the outcome, test will pass if passed category is now in the database
            Assert.NotNull(result);
            Assert.Contains(newCategory, _db.Categories);
        }

        [Fact] //Test to validate controller correctly handles an attempt to create new category with identical ID to an existing entry
        public async void CategoryControllerCreateExistingCategory()
        {
            //Arrange : Create the mock database and controller
            PopulateMockDB();
            var controller = new CategoryController(_db);

            //Sets the details of the category to be added as the same as an existing database entry
            SetExistingCategory(1);
            var newCategory = _existingCategory;

            //Act: Produce a variable that can be assessed - the result of attempting to create a new category using invalid details
            var result = await controller.Create(newCategory);

            //Assert : Check the result matches the outcome, test will pass if a message is returned
            Assert.NotNull(result);
        }

        [Fact] //Test to check existing categories can be edited through the controller
        public async void CategoryControllerEditSuccessfull()
        {
            //Arrange : Create the mock database and controller
            PopulateMockDB();
            var controller = new CategoryController(_db);
            SetExistingCategory(1);

            //Act: Produce a variable that can be assessed - the result of attempting to edit an existing category
            var result = await controller.Edit(_existingCategory.Id) as ViewResult;

            //Assert : Check the result matches the outcome, test passes if a view is returned (This view would then be used in the front-end to amend details)
            Assert.NotNull(result.ViewData);
        }

        [Fact] //Test to validate controller correctly handles an attempt to edit a non-existent category
        public async void CategoryControllerEditIDNotValid()
        {
            //Arrange : Create the mock database and controller
            PopulateMockDB();
            var controller = new CategoryController(_db);

            //Act: Produce a variable that can be assessed - the result of attempting to edit a non-existent category
            var result = await controller.Edit(12) as NotFoundResult;

            //Assert : Check the result matches the outcome, with status code 404 indicating the category could not be found
            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact] //Test to check existing categories can be successfully deleted from the database through the controller
        public async void CategoryControllerDeleteSuccessful()
        {
            //Arrange : Create the mock database and controller
            PopulateMockDB();
            var controller = new CategoryController(_db);
            SetExistingCategory(1);

            //Act: Produce a variable that can be assessed - the result of attempting to delete a valid category
            var result = await controller.DeleteConfirmed(_existingCategory.Id);

            //Assert : Check the result matches the outcome, the test passes if the category is no longer present in the database
            Assert.NotNull(result);
            Assert.DoesNotContain(_existingCategory, _db.Categories);
        }
    }
}
