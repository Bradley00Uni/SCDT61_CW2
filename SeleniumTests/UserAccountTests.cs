using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace SeleniumTests
{
    public class UserAccountTests
    {
        //Global Variables that store the relevant login details as needed by the tests
        private string loginName;
        private string loginPassword;
        private string loginEmail;

        //Sets the Chrome Driver that will be used by Selenium
        IWebDriver driver = new ChromeDriver();

        //Method that when called, sets the login variables to the Admin credentials
        private void PopulateAdmin()
        {
            loginName = "admin";
            loginPassword = "Admin123!";
            loginEmail = "admin@admin.com";
        }

        //Method that when called, sets the login variables to the Manager credentials
        private void PopulateManager()
        {
            loginName = "manager";
            loginPassword = "Manager123!";
            loginEmail = "manager@manager.com";
        }

        //Method that when called, sets the login variables to the Customer credentials
        private void PopulateCustomer()
        {
            loginName = "customer";
            loginPassword = "Customer123!";
            loginEmail = "customer@customer.com";
        }

        //Method that when called, enters valid login credentials based on passed role
        private void Login(string role)
        {
            driver.Navigate().GoToUrl("https://onlineshop202220220302112626.azurewebsites.net/Identity/Account/Login");

            if(role == "admin") { PopulateAdmin(); }
            else if(role == "manager") { PopulateManager(); }
            else if(role == "customer") { PopulateCustomer(); }

            IWebElement emailInput = driver.FindElement(By.Id("Input_Email"));
            emailInput.SendKeys(loginEmail);
            IWebElement passwordInput = driver.FindElement(By.Id("Input_Password"));
            passwordInput.SendKeys(loginPassword);
            emailInput.SendKeys(Keys.Return);
            Thread.Sleep(1000);
        }

        //Method that when called, logs out the current user
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

        [Test] //This test checks that users can successfully register an account 
        public void RegisterSuccessTest()
        {
            //Finds and clicks the link to naviagte: Login Page --> Register Page
            IWebElement registerLink = driver.FindElement(By.Id("register-link"));
            registerLink.Click();

            //Wait for the page to load, then get the new URL
            Thread.Sleep(1000);
            var currentURL = driver.Url;

            //Create an Array containing all fields in the registration form
            IWebElement[] registerForm = new IWebElement[]
            {
                driver.FindElement(By.Id("Input_Fname")),
                driver.FindElement(By.Id("Input_Sname")),
                driver.FindElement(By.Id("Input_Email")),
                driver.FindElement(By.Id("Input_Password")),
                driver.FindElement(By.Id("Input_ConfirmPassword")),
            };

            //Create an Array containing all the details to be entered into the form
            string[] details = new string[] { "Example", "Surname", "selenium@example.com", "Password123!", "Password123!" };


            //For each field in the form, enter the relevant information
            var x = 0;
            foreach(var r in registerForm){ r.SendKeys(details[x]); x++; }
            //Submit the form
            registerForm[4].SendKeys(Keys.Return);

            //Waits before executing next line of code, to ensure slow loading times don't compromise test results
            Thread.Sleep(1000);

            //Checks if the URL has changed, indicating a successful page load. If true, pass the test, else fail. Page will only change on successful register/login
            if (driver.Url != currentURL){ Assert.Pass(); Logout(); }
            else{ Assert.Fail(); }
        }

        [Test] //Test to validate the NavBar menus that require certain roles, only show when users are Authorised
        public void MenusRestrictedToAuthorisedAccounts()
        {
            //Declare the elements that will be checked
            IWebElement adminButton = null;
            IWebElement managerButton = null;

            //Passes the Admin role to log in
            Login("admin");
            
            //Attempts to find the buttons used to access role-specific menus
            try
            { adminButton = driver.FindElement(By.Id("adminLink")); }
            catch {}

            try { managerButton = driver.FindElement(By.Id("managerLink")); }
            catch{}

            //In the event the correct menus aren't shown - fail the test. Otherwise, reset the elements and log out
            if(adminButton == null || managerButton == null)
            {
                Assert.Fail();
                Console.WriteLine("Admin Failed");
                End();
            }
            else
            {
                Logout();
                adminButton = managerButton = null;
            }

            //Passes the Manager role to log in
            Login("manager");

            //Attempts to find the buttons used to access role-specific menus
            try
            { adminButton = driver.FindElement(By.Id("adminLink")); }
            catch { }

            try { managerButton = driver.FindElement(By.Id("managerLink")); }
            catch { }

            //In the event the correct menus aren't shown - fail the test. Otherwise, reset the elements and log out
            if (managerButton == null || adminButton != null)
            {
                Assert.Fail();
                Console.WriteLine("Manager Failed");
                End();
            }
            else
            {
                Logout();
                adminButton = managerButton = null;
            }

            //Passes the Customer role to log in
            Login("customer");

            //Attempts to find the buttons used to access role-specific menus
            try
            { adminButton = driver.FindElement(By.Id("adminLink")); }
            catch { }

            try { managerButton = driver.FindElement(By.Id("managerLink")); }
            catch { }

            //In the event the correct menus aren't shown - fail the test. Otherwise, log out and pass the test
            if (adminButton != null || managerButton != null)
            {
                Assert.Fail();
                Console.WriteLine("Customer Failed");
            }
            else
            {
                Logout();
                Assert.Pass();
            }

        }

        [Test]
        public void ManageUserRoles()
        {
            Login("admin");

            driver.FindElement(By.Id("adminLink")).Click();
            driver.FindElement(By.Id("User Management")).Click();

            var targetField = driver.FindElement(By.Id("Example Surname"));
            targetField.FindElement(By.Id("manageButton")).Click();
            Thread.Sleep(600);

            driver.FindElement(By.Id("Manager")).Click();
            driver.FindElement(By.Id("update")).Click();

            Thread.Sleep(1000);

            targetField = driver.FindElement(By.Id("Example Surname"));
            if (targetField.FindElement(By.Id("roles")).Text.Contains("Manager") == true)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [OneTimeTearDown]
        public void End(){ driver.Close(); }
    }
}