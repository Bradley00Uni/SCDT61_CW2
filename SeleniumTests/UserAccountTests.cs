using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
    public class Tests
    {
        private string loginName;
        private string loginPassword;
        private string loginEmail;

        IWebDriver driver = new ChromeDriver("D:\\Level 6\\SCDT61 -  Software Development & QA\\Assignments\\Assignment 2");

        private void PopulateAdmin()
        {
            loginName = "admin";
            loginPassword = "Admin123!";
            loginEmail = "admin@admin.com";
        }

        [SetUp]
        public void Setup()
        {
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void LoginAndDetailRetrievalTest()
        {
            var currentURL = driver.Url;
            PopulateAdmin();

            IWebElement emailInput = driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(loginEmail);

            IWebElement passwordInput = driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys(loginPassword);
            emailInput.SendKeys(Keys.Return);

            Thread.Sleep(1000);

            if(driver.Url != currentURL)
            {
                IWebElement manageButton = driver.FindElement(By.Id("manage"));

                if (manageButton.Text == loginName) { Assert.Pass(); }
            }
        }

        [TearDown]
        public void End()
        {
            driver.Close();
        }
    }
}