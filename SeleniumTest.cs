using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;


namespace CS3750_PlanetExpressLMSTest
{
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
            try
            {
                driver.Navigate().GoToUrl("https://localhost:44390/login");
            }
            catch
            {
                var buttonAdvanced = driver.FindElement(By.Id("advancedButton"));
                buttonAdvanced.Click();

                var buttonException = driver.FindElement(By.Id("exceptionDialogButton"));
                buttonException.Click();
            }         
            
            var title = driver.Title;
            Assert.AreEqual("Login - Planet Express Learning Management System", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("test@test.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Welcome")));

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
            new DriverManager().SetUpDriver(new EdgeConfig(), VersionResolveStrategy.MatchingBrowser);
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
