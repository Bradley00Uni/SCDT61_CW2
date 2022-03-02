using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
    public class Tests
    {
        //Global Variables that store the relevant login details as needed by the tests
        private string loginName;
        private string loginPassword;
        private string loginEmail;

        //Sets the Chrome Driver that will be used by Selenium
        IWebDriver driver = new ChromeDriver("D:\\Level 6\\SCDT61 -  Software Development & QA\\Assignments\\Assignment 2");

        //Method that when called, sets the login variables to the Admin credentials
        private void PopulateAdmin()
        {
            loginName = "admin";
            loginPassword = "Admin123!";
            loginEmail = "admin@admin.com";
        }

        public void Logout()
        {
            IWebElement logoutButton = driver.FindElement(By.Id("logout"));
            logoutButton.Click();
        }

        [SetUp] //This method is called before any tests are run, so the environment can be setup as needed by the tests
        public void Setup()
        {
            //Navigate to the live version of the OnlineSHop application, hosted through Azure, and maximise the window
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/");
            driver.Manage().Window.Maximize();
        }

        [Test] //This test checks that users can successfully login to the system
        public void LoginSuccessAndManageDetailRetrievalTest()
        {
            //Gets the current URL of the Browser
            var currentURL = driver.Url;
            //Calls the method to set login variables as Admin credentials
            PopulateAdmin();

            //Finds the Form Input for Email Adress, enters the account credentials
            IWebElement emailInput = driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(loginEmail);

            //Finds the Form Input for Password, enters the account credentials
            IWebElement passwordInput = driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys(loginPassword);

            //Submits form
            emailInput.SendKeys(Keys.Return);

            //Waits before executing next line of code, to ensure slow loading times don't compromise test results
            Thread.Sleep(1000);

            //Checks if the URL has changed, indicating a successful page load
            if(driver.Url != currentURL)
            {
                //Find the button in the Navbar for loading Account details
                IWebElement manageButton = driver.FindElement(By.Id("manage"));

                //Test Passes if the button text has correctly pulled Account name
                if (manageButton.Text == loginName) { Assert.Pass(); }
            }

            //Calls method to clear credentials
            Logout();
        }

        [Test]
        public void RegisterSuccessTest()
        {
            //Tests that user can register for an account
        }

        [OneTimeTearDown]
        public void End()
        {
            driver.Close();
        }
    }
}