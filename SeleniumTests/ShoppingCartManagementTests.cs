using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace SeleniumTests
{
    public class ShoppingCartManagementTests
    {
        private string loginPassword;
        private string loginEmail;

        IWebDriver driver = new ChromeDriver();

        private void PopulateCustomer()
        {
            loginPassword = "Customer123!";
            loginEmail = "customer@customer.com";
        }
        private void Login()
        {
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/Identity/Account/Login");

            PopulateCustomer();

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
            Login();
        }

        [Test]
        public void CartHandleQuantityOver100()
        {
            var quantity = 0;

            do
            {
                driver.FindElement(By.Id("product 2")).FindElement(By.ClassName("btn")).Click();
                driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/");
                quantity++;
            }while (quantity < 99);

            driver.FindElement(By.Id("product 2")).FindElement(By.ClassName("btn")).Click();

            if (driver.FindElement(By.ClassName("table")).Text.Contains("100"))
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Not All Products Accounted For");
                Assert.Fail();
            }
        }

        [Test]
        public void CardTotalCorrectlyUpdates()
        {
            var quantity = 0;

            do
            {
                driver.FindElement(By.Id("product 2")).FindElement(By.ClassName("btn")).Click();
                driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/");
                quantity++;
            } while (quantity < 10);

            if (driver.FindElement(By.Id("cart-status")).Text == "10")
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Incorrect Quantity Shown");
                Assert.Fail();
            }
        }

        [OneTimeTearDown]
        public void End() { driver.Close(); }
    }
}
