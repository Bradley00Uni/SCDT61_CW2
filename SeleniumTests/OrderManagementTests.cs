using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace SeleniumTests
{
    public class OrderManagementTests
    {
        IWebDriver driver = new ChromeDriver();

        private string loginPassword;
        private string loginEmail;
        private void PopulateAdmin()
        {
            loginPassword = "Admin123!";
            loginEmail = "admin@admin.com";
        }
        private void Login()
        {
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/Identity/Account/Login");

            PopulateAdmin();

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

        [Test] //Tests that orders can be successfully created using the shopping cart form on the website
        public void SuccessfulOrderCreation() 
        {
            driver.FindElement(By.Id("product 2")).FindElement(By.ClassName("btn")).Click();
            Thread.Sleep(100);

            driver.FindElement(By.LinkText("PROCEED TO CHECKOUT")).Click();
            driver.FindElement(By.Id("FirstName")).SendKeys("Bradley");
            driver.FindElement(By.Id("LastName")).SendKeys("DeAth");
            driver.FindElement(By.Id("AddressLine1")).SendKeys("University Centre Somerset");
            driver.FindElement(By.Id("Postcode")).SendKeys("TA1 5AX");
            driver.FindElement(By.Id("City")).SendKeys("Taunton");
            driver.FindElement(By.Id("Country")).SendKeys("England");
            driver.FindElement(By.Id("Email")).SendKeys("user@user.com");
            var currentURL = driver.Url;
            
            driver.FindElement(By.Id("Email")).Submit();
            Thread.Sleep(100);

            if(driver.Url != currentURL)
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Order could not be registered");
                Assert.Fail();
            }
        }

        [Test] //Tests that Admin users can successfully manage Orders
        public void AdminOrderManagement()
        {
            var startUrl = "https://onlineshop202220220302112626.azurewebsites.net/Admin/Order";
            driver.Navigate().GoToUrl(startUrl);

            driver.FindElement(By.LinkText("DETAILS")).Click();
            driver.FindElement(By.LinkText("EDIT")).Click();

            driver.FindElement(By.Id("LastName")).Clear();
            driver.FindElement(By.Id("LastName")).SendKeys("Surname");
            driver.FindElement(By.Id("LastName")).SendKeys(Keys.Return);

            if(driver.Url == startUrl)
            {
                Logout();
                Assert.Pass();
            }
            else
            {
                Console.WriteLine("Management Failed");
                Assert.Fail();
            }
        }

        [Test] //Tests that Admin users can successfully delete Orders
        public void SuccessfulOrderRemoval()
        {
            var startUrl = "https://onlineshop202220220302112626.azurewebsites.net/Admin/Order";
            driver.Navigate().GoToUrl(startUrl);
            driver.FindElement(By.LinkText("DELETE")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.LinkText("BACK TO LIST")).Click();
            driver.FindElement(By.LinkText("DELETE")).Click();
            Thread.Sleep(1000);

            driver.FindElement(By.Id("Delete")).Click();

            Logout();
            Assert.Pass();
        }

        [OneTimeTearDown]
        public void End() { driver.Close(); }
    }
}
