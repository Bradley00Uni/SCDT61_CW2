using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;


namespace SeleniumTests
{
    public class ProductManagementTests
    {
        private string loginName;
        private string loginPassword;
        private string loginEmail;

        IWebDriver driver = new ChromeDriver();

        private void PopulateAdmin()
        {
            loginName = "admin";
            loginPassword = "Admin123!";
            loginEmail = "admin@admin.com";
        }
        private void PopulateCustomer()
        {
            loginName = "customer";
            loginPassword = "Customer123!";
            loginEmail = "customer@customer.com";
        }
        private void Login(string role)
        {
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/Identity/Account/Login");

            if (role == "admin") { PopulateAdmin(); }
            else if (role == "customer") { PopulateCustomer(); }

            IWebElement emailInput = driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(loginEmail);
            IWebElement passwordInput = driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys(loginPassword);
            emailInput.SendKeys(Keys.Return);
            Thread.Sleep(1000);
        }
        public void Logout()
        {
            IWebElement logoutButton = driver.FindElement(By.Id("logout"));
            logoutButton.Click();
        }

        [SetUp]
        public void Setup()
        {
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void ProductsDisplayedOnLoad()
        {
            var productCount = 0;
            Login("customer");

            if(driver.FindElement(By.Id("product 2")).Text.Contains("£120")){
                productCount++;
            }

            if (driver.FindElement(By.Id("product 3")).Text.Contains("£120")){
                productCount++;
            }

            if (driver.FindElement(By.Id("product 4")).Text.Contains("£110"))
            {
                productCount++;
            }

            if(productCount == 3)
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
                Console.WriteLine("Not All Products Returned");
            }
        }

        [Test]
        public void ProductDescriptionsDisplayed()
        {
            Login("customer");

            if (driver.FindElement(By.Id("product 2 desc")).Text.Contains("3 Stack Shelving") && driver.FindElement(By.Id("product 3 desc")).Text.Contains("Desk"))
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Descriptions shown do not Match Products");
                Assert.Fail();
            }
        }

        [Test]
        public void NaviagateToProductBrowse()
        {
            Login("customer");

            driver.FindElement(By.Id("Browse")).Click();
            Thread.Sleep(500);

            var newURL = driver.Url;

            if(newURL == "https://onlineshop202220220302112626.azurewebsites.net/Home/Products")
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Wrong View Returned");
                Assert.Fail();
            }
        }

        [Test]
        public void AdminCategoryManagement()
        {
            Login("admin");
            driver.FindElement(By.Id("adminLink")).Click();
            driver.FindElement(By.Id("Category Management")).Click();
            driver.FindElement(By.Id("Create")).Click();

            var targetField = driver.FindElement(By.Id("Name"));
            targetField.SendKeys("House Plant");
            targetField.SendKeys(Keys.Return);
            Thread.Sleep(500);

            if(driver.FindElement(By.ClassName("table")).Text.Contains("House Plant"))
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Category Creation Failed");
                Assert.Fail();
            }
        }

        [Test]
        public void AdminProductManagement()
        {
            Login("admin");
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/Admin/Product");
            driver.FindElement(By.Id("Create")).Click();

            driver.FindElement(By.Id("Product.Description")).SendKeys("Lemon Tree");
            driver.FindElement(By.Id("Product.Price")).SendKeys("40");
            var dropdown = driver.FindElement(By.Id("Product_CategoryId"));
            dropdown.SendKeys(Keys.ArrowDown);
            dropdown.SendKeys(Keys.ArrowDown);
            dropdown.SendKeys(Keys.Return);

            driver.FindElement(By.Id("Product.ImagePath")).SendKeys("C:\\Users\\bradl\\Documents\\SCDT61_Cw2\\OnlineShop2022---TBF-master\\SeleniumTests\\resources\\lemon_tree.jpg");

            driver.FindElement(By.Id("Submit")).Click();
            Thread.Sleep(1000);

            if(driver.FindElement(By.ClassName("table-striped")).Text.Contains("Lemon Tree"))
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Product Creation Failed");
                Assert.Fail();
            }
        }

        [OneTimeTearDown]
        public void End() { driver.Close(); }

        //View Procuct Magement and Update Category
        //Add, Edit and Delete Product

    }
}
