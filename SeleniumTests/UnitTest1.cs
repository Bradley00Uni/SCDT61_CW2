using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace SeleniumTests
{
    public class Tests
    {
        IWebDriver driver = new ChromeDriver("D\\Level 6\\SCDT61 -  Software Development & QA\\Assignments\\Assignment 2\\chromedriver");

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}