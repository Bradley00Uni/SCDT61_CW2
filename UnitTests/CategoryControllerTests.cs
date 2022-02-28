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
        private CategoryModel _existingCategory;

        private void CreateMockDB()
        {
            //Stores the new database context in the "InMemoryDatabase" imported into the project using the NuGet Package Manager
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            _db = new AppDbContext(options);
            _db.Database.EnsureCreated(); //Ensures the Database has been correctly created before ending the function
        }

        private async void PopulateMockDB()
        {
            CreateMockDB();

            var CategoryA = new CategoryModel() { Id = 1, Name = "Test Category A" };
            var CategoryB = new CategoryModel() { Id = 2, Name = "Test Category B" };
            var CategoryC = new CategoryModel() { Id = 3, Name = "Test Category C" };

            CategoryModel[] c = new CategoryModel[] { CategoryA, CategoryB, CategoryC };

            _db.Categories.AddRange(c);
            await _db.SaveChangesAsync();
        }

        private async void SetExistingCategory(int id)
        {
            var categories = await _db.Categories.ToListAsync();
            _existingCategory = categories.Find(x => x.Id == id);
        }

        [Fact]
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

        [Fact]
        public async void CategoryControllerDetailsRetrievalSuccessfull()
        {
            PopulateMockDB();
            var controller = new CategoryController(_db);

            var result = await controller.Details(1) as ViewResult;

            SetExistingCategory(1);

            Assert.Equal(result.Model, _existingCategory);
        }

        [Fact]
        public async void CategoryControllerDetailsIDNotEqual()
        {
            PopulateMockDB();
            var controller = new CategoryController(_db);

            var result = await controller.Details(0) as ViewResult;

            SetExistingCategory(1);

            Assert.Equal(result.Model, _existingCategory);
        }

        [Fact]
        public async void CategoryControllerCreateSuccessfull()
        {
            CreateMockDB();
            var controller = new CategoryController(_db);
            var newCategory = new CategoryModel() { Id = 1, Name = "Created Product"};

            var result = await controller.Create(newCategory);

            Assert.NotNull(result);
            Assert.Contains(newCategory, _db.Categories);
        }

        [Fact]
        public async void CategoryControllerCreateExistingCategory()
        {
            PopulateMockDB();
            var controller = new CategoryController(_db);
            var newCategory = new CategoryModel() { Id = 1, Name = "Test Category A" };

            var result = await controller.Create(newCategory);

            Assert.NotNull(result);
        }

        [Fact]
        public async void CategoryControllerEditSuccessfull()
        {
            PopulateMockDB();
            var controller = new CategoryController(_db);
            SetExistingCategory(1);

            var result = await controller.Edit(_existingCategory.Id) as ViewResult;

            Assert.NotNull(result.ViewData);
        }

        [Fact]
        public async void CategoryControllerEditIDNotEqual()
        {
            PopulateMockDB();
            var controller = new CategoryController(_db);

            var result = await controller.Edit(12) as NotFoundResult;

            Assert.Equal("404", result.StatusCode.ToString());
        }

        [Fact]
        public async void CategoryControllerDeleteSuccessful()
        {
            PopulateMockDB();
            var controller = new CategoryController(_db);
            SetExistingCategory(1);

            var result = await controller.DeleteConfirmed(_existingCategory.Id);

            Assert.NotNull(result);
            Assert.DoesNotContain(_existingCategory, _db.Categories);
        }
    }
}
