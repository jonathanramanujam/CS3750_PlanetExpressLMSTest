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

            var message = driver.FindElement(By.Id("page-title"));
            var value = message.Text;
            Assert.AreEqual("Dashboard", value);
        }

        [TestMethod]
        public void ChromeMakePayment()
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

/*            var message = driver.FindElement(By.Id("page-title"));
            var value = message.Text;
            Assert.AreEqual("Dashboard", value);*/

            var paymentLink = driver.FindElement(By.LinkText("Account"));
            paymentLink.Click();

            var originalBalance = driver.FindElement(By.Id("balance")).Text;

            var textboxFirstName = driver.FindElement(By.Name("txtFirstName"));
            var textboxLastName = driver.FindElement(By.Name("txtLastName"));
            var textboxCardNumber = driver.FindElement(By.Name("txtCardNumber"));
            var textboxExpDate = driver.FindElement(By.Name("txtExpDate"));
            var textboxCvv = driver.FindElement(By.Name("txtCvv"));
            var textboxAmount = driver.FindElement(By.Name("txtAmount"));
            var submitButton = driver.FindElement(By.ClassName("btn-primary"));

            textboxFirstName.SendKeys("test");
            textboxLastName.SendKeys("test");
            textboxCardNumber.SendKeys("4242424242424242");
            textboxCvv.SendKeys("123");
            textboxExpDate.SendKeys("December2023");
            textboxAmount.SendKeys("5");
            submitButton.Click();

            var newBalance = driver.FindElement(By.Id("balance")).Text;
            Assert.AreNotEqual(originalBalance, newBalance);
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
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-title")));

            var message = driver.FindElement(By.Id("page-title"));
            var value = message.Text;
            Assert.AreEqual("Dashboard", value);
        }

        [TestMethod]
        public void FirefoxMakePayment()
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
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-title")));

            /*            var message = driver.FindElement(By.Id("page-title"));
                        var value = message.Text;
                        Assert.AreEqual("Dashboard", value);*/

            var paymentLink = driver.FindElement(By.LinkText("Account"));
            paymentLink.Click();

            element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-title")));

            var originalBalance = driver.FindElement(By.Id("balance")).Text;

            var textboxFirstName = driver.FindElement(By.Name("txtFirstName"));
            var textboxLastName = driver.FindElement(By.Name("txtLastName"));
            var textboxCardNumber = driver.FindElement(By.Name("txtCardNumber"));
            var textboxExpDate = driver.FindElement(By.Name("txtExpDate"));
            var textboxCvv = driver.FindElement(By.Name("txtCvv"));
            var textboxAmount = driver.FindElement(By.Name("txtAmount"));
            var submitButton = driver.FindElement(By.ClassName("btn-primary"));

            textboxFirstName.SendKeys("test");
            textboxLastName.SendKeys("test");
            textboxCardNumber.SendKeys("4242424242424242");
            textboxCvv.SendKeys("123");
            textboxExpDate.SendKeys("December2023");
            textboxAmount.SendKeys("5");
            submitButton.Click();

            var newBalance = driver.FindElement(By.Id("balance")).Text;
            Assert.AreNotEqual(originalBalance, newBalance);
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

            var message = driver.FindElement(By.Id("page-title"));
            var value = message.Text;
            Assert.AreEqual("Dashboard", value);
        }

        [TestMethod]
        public void EdgeMakePayment()
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

            /*            var message = driver.FindElement(By.Id("page-title"));
                        var value = message.Text;
                        Assert.AreEqual("Dashboard", value);*/

            var paymentLink = driver.FindElement(By.LinkText("Account"));
            paymentLink.Click();

            var originalBalance = driver.FindElement(By.Id("balance"));

            var textboxFirstName = driver.FindElement(By.Name("txtFirstName"));
            var textboxLastName = driver.FindElement(By.Name("txtLastName"));
            var textboxCardNumber = driver.FindElement(By.Name("txtCardNumber"));
            var textboxExpDate = driver.FindElement(By.Name("txtExpDate"));
            var textboxCvv = driver.FindElement(By.Name("txtCvv"));
            var textboxAmount = driver.FindElement(By.Name("txtAmount"));
            var submitButton = driver.FindElement(By.ClassName("btn-primary"));

            textboxFirstName.SendKeys("test");
            textboxLastName.SendKeys("test");
            textboxCardNumber.SendKeys("4242424242424242");
            textboxCvv.SendKeys("123");
            textboxExpDate.SendKeys("December2023");
            textboxAmount.SendKeys("5");
            submitButton.Click();

            var newBalance = driver.FindElement(By.Id("balance"));
            Assert.AreNotEqual(originalBalance.Text, newBalance.Text);
        }
    }
}
