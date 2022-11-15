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
            textboxExpDate.SendKeys("December" + Keys.ArrowRight + "2023");
            textboxAmount.SendKeys("5");
            submitButton.Click();

            var newBalance = driver.FindElement(By.Id("balance")).Text;
            Assert.AreNotEqual(originalBalance, newBalance);
        }

        [TestMethod]
        public void ChromeCourseDetails()
        {
            //login
            driver.Navigate().GoToUrl("https://localhost:44390/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("faketeacher@mail.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //go to a details page
            var buttonDetails = driver.FindElement(By.ClassName("btn-primary"));
            buttonDetails.Click();

            //make sure you got there
            var message = driver.FindElement(By.ClassName("page-title"));
            var value = message.Text;
            Assert.AreEqual("Course Details", value);
        }

        [TestMethod]
        public void ChromeToDoList()
        {
            //login
            driver.Navigate().GoToUrl("https://localhost:44390/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("fakestudent@mail.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //go to a assignment page from to do list
            var toDoLink = driver.FindElement(By.Id("ass-link"));
            toDoLink.Click();

            //make sure you got there
            var message = driver.FindElement(By.ClassName("page-title"));
            var value = message.Text;
            Assert.AreEqual("Course Details >> Submit Assignment", value);
        }

        [TestMethod]
        public void ChromeSubmitAssignment()
        {
            //Log in
            driver.Navigate().GoToUrl("https://localhost:44390/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("student25@email.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //Go to CourseDetails
            var buttonDetails = driver.FindElement(By.ClassName("btn-primary"));
            buttonDetails.Click();

            //Go to submit assignment (text assignment)
            var buttonSubmitPage = driver.FindElement(By.ClassName("btn-secondary"));
            buttonSubmitPage.Click();

            //Assert you're on the right page
            var pageElement = driver.FindElement(By.ClassName("page-title"));
            var pageTitle = pageElement.Text;
            Assert.AreEqual("Course Details >> Submit Assignment", pageTitle);

            //Try submitting an assignment and verify the successful submit message appears
            var submissionTextBox = driver.FindElement(By.ClassName("submission-textbox"));
            submissionTextBox.SendKeys("asdfjkl;asdfjkl;asdfjkl;asdfjkl;");

            var buttonSubmit = driver.FindElement(By.ClassName("btn-primary"));
            buttonSubmit.Click();

            Assert.IsNotNull(driver.FindElement(By.ClassName("alert-success")));
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

            element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("balance")));

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
            textboxExpDate.SendKeys("December" + Keys.ArrowRight + "2023");
            textboxAmount.SendKeys("5");
            submitButton.Click();

            var newBalance = driver.FindElement(By.Id("balance")).Text;
            Assert.AreNotEqual(originalBalance, newBalance);
        }

        [TestMethod]
        public void FirefoxCourseDetails()
        {
            //login
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
            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("faketeacher@mail.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //wait until logged in
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-title")));

            //go to a details page
            var buttonDetails = driver.FindElement(By.ClassName("btn-primary"));
            buttonDetails.Click();



            //make sure you got there
            var message = driver.FindElement(By.ClassName("page-title"));
            var value = message.Text;
            Assert.AreEqual("Course Details", value);
        }

        [TestMethod]
        public void FireFoxToDoList()
        {
            //login
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
            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("fakestudent@mail.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //wait until logged in
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-title")));

            //go to a assignment from to do list
            var toDoLink = driver.FindElement(By.Id("ass-link"));
            toDoLink.Click();

            //make sure you got there
            var message = driver.FindElement(By.ClassName("page-title"));
            var value = message.Text;
            Assert.AreEqual("Course Details >> Submit Assignment", value);
        }

        [TestMethod]
        public void FirefoxSubmitAssignment()
        {
            //login
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

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("student25@email.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("grid-dashboard")));

            //Go to CourseDetails
            var buttonDetails = driver.FindElement(By.ClassName("btn-primary"));
            buttonDetails.Click();

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            element = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("grid-coursedetail-item")));

            //Go to submit assignment (text assignment)
            var buttonSubmitPage = driver.FindElement(By.ClassName("btn-secondary"));
            buttonSubmitPage.Click();

            //Assert you're on the right page
            var pageElement = driver.FindElement(By.ClassName("page-title"));
            var pageTitle = pageElement.Text;
            Assert.AreEqual("Course Details >> Submit Assignment", pageTitle);

            //Try submitting an assignment and verify the successful submit message appears
            var submissionTextBox = driver.FindElement(By.ClassName("submission-textbox"));
            submissionTextBox.SendKeys("asdfjkl;asdfjkl;asdfjkl;asdfjkl;");

            var buttonSubmit = driver.FindElement(By.ClassName("btn-primary"));
            buttonSubmit.Click();

            Assert.IsNotNull(driver.FindElement(By.ClassName("alert-success")));


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
            textboxExpDate.SendKeys("December" + Keys.ArrowRight + "2023");
            textboxAmount.SendKeys("5");
            submitButton.Click();

            var newBalance = driver.FindElement(By.Id("balance")).Text;
            Assert.AreNotEqual(originalBalance, newBalance);
        }


        [TestMethod]
        public void EdgeCourseDetails()
        {
            //login
            driver.Navigate().GoToUrl("https://localhost:44390/login");

            var title = driver.Title;
            Assert.AreEqual("Login - Planet Express Learning Management System", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("faketeacher@mail.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //go to a details page
            var buttonDetails = driver.FindElement(By.ClassName("btn-primary"));
            buttonDetails.Click();

            //make sure you got there
            var message = driver.FindElement(By.ClassName("page-title"));
            var value = message.Text;
            Assert.AreEqual("Course Details", value);
        }

        [TestMethod]
        public void EdgeToDoList()
        {
            //login
            driver.Navigate().GoToUrl("https://localhost:44390/login");

            var title = driver.Title;
            Assert.AreEqual("Login - Planet Express Learning Management System", title);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("fakestudent@mail.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();


            //wait until logged in
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            IWebElement element = wait.Until(ExpectedConditions.ElementIsVisible(By.Id("page-title")));

            //go to an assignment from To do list
            var toDoUrl = driver.FindElement(By.Id("ass-link"));
            toDoUrl.Click();

            //make sure you got there
            var message = driver.FindElement(By.ClassName("page-title"));
            var value = message.Text;
            Assert.AreEqual("Course Details >> Submit Assignment", value);
        }

        [TestMethod]
        public void EdgeSubmitAssignment()
        {
            //Log in
            driver.Navigate().GoToUrl("https://localhost:44390/login");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(500);

            var textboxUsername = driver.FindElement(By.Id("textbox-username"));
            var textboxPassword = driver.FindElement(By.Id("textbox-password"));
            var buttonLogin = driver.FindElement(By.Id("button-login"));

            textboxUsername.SendKeys("student25@email.com");
            textboxPassword.SendKeys("password");
            buttonLogin.Click();

            //Go to CourseDetails
            var buttonDetails = driver.FindElement(By.ClassName("btn-primary"));
            buttonDetails.Click();

            //Go to submit assignment (text assignment)
            var buttonSubmitPage = driver.FindElement(By.ClassName("btn-secondary"));
            buttonSubmitPage.Click();

            //Assert you're on the right page
            var pageElement = driver.FindElement(By.ClassName("page-title"));
            var pageTitle = pageElement.Text;
            Assert.AreEqual("Course Details >> Submit Assignment", pageTitle);

            //Try submitting an assignment and verify the successful submit message appears
            var submissionTextBox = driver.FindElement(By.ClassName("submission-textbox"));
            submissionTextBox.SendKeys("asdfjkl;asdfjkl;asdfjkl;asdfjkl;");

            var buttonSubmit = driver.FindElement(By.ClassName("btn-primary"));
            buttonSubmit.Click();

            Assert.IsNotNull(driver.FindElement(By.ClassName("alert-success")));
        }
    }
}
