using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CS3750_PlanetExpressLMS.Models;
using CS3750_PlanetExpressLMS.Data;
using CS3750_PlanetExpressLMS.Pages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using CS3750_PlanetExpressLMS;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium;

namespace CS3750_PlanetExpressLMSTest
{
    [TestClass]
    public class FirstScriptTest
    {

        protected IWebDriver driver;

        [TestInitialize]
        public void CreateDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            driver.Quit();
        }

        [TestMethod]
        public void ChromeSession()
        {
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");

            var title = driver.Title;
            Assert.AreEqual("Web form", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textBox = driver.FindElement(By.Name("my-text"));
            var submitButton = driver.FindElement(By.TagName("button"));

            textBox.SendKeys("Selenium");
            submitButton.Click();

            var message = driver.FindElement(By.Id("message"));
            var value = message.Text;
            Assert.AreEqual("Received!", value);
        }
    }

    [TestClass]
    public class SeleniumChrome
    {
        protected IWebDriver driver;

        [TestInitialize]
        public void CreateDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            driver.Quit();
        }

        [TestMethod]
        public void ChromeLogin()
        {
            driver.Navigate().GoToUrl("https://localhost:44390/login");

            var title = driver.Title;
            Assert.AreEqual("Login - Planet Express Learning Management System", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("test@test.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            var message = driver.FindElement(By.Id("Welcome"));
            var value = message.Text;
            Assert.AreEqual("Welcome test testing!", value);
        }
    }

    [TestClass]
    public class SeleniumFirefox
    {
        protected IWebDriver driver;

        [TestInitialize]
        public void CreateDriver()
        {
            new DriverManager().SetUpDriver(new FirefoxConfig());
            driver = new FirefoxDriver();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            driver.Quit();
        }

        [TestMethod]
        public void FirefoxLogin()
        {
            driver.Navigate().GoToUrl("https://localhost:44390/login");

            var title = driver.Title;
            Assert.AreEqual("Login - Planet Express Learning Management System", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("test@test.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            var message = driver.FindElement(By.Id("Welcome"));
            var value = message.Text;
            Assert.AreEqual("Welcome test testing!", value);
        }
    }

    [TestClass]
    public class SeleniumEdge
    {
        protected IWebDriver driver;

        [TestInitialize]
        public void CreateDriver()
        {
            new DriverManager().SetUpDriver(new EdgeConfig());
            driver = new EdgeDriver();
        }

        [TestCleanup]
        public void QuitDriver()
        {
            driver.Quit();
        }

        [TestMethod]
        public void EdgeLogin()
        {
            driver.Navigate().GoToUrl("https://localhost:44390/login");

            var title = driver.Title;
            Assert.AreEqual("Login - Planet Express Learning Management System", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("test@test.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            var message = driver.FindElement(By.Id("Welcome"));
            var value = message.Text;
            Assert.AreEqual("Welcome test testing!", value);
        }
    }
}
