using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using NUnit.Framework;
using TestingLogin;
using TestingLogin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NUnit.Framework.Internal;

namespace TestingLog
{
    [TestFixture]
    public class AccountControllerTest
    {
        //private IWebDriver driver;
        //private WebDriverWait wait;

        //[OneTimeSetUp]
        //public void Setup()
        //{
        //    driver = new ChromeDriver();
        //    driver.Manage().Window.Maximize();
        //    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //}
        //[OneTimeTearDown]
        //public void OneTimeTearDown()
        //{
        //    driver.Dispose();
        //}

        //    private HttpClient _client;
        //    private WebApplicationFactory<Program> _factory;
        //    private List<string> _testUsers = new List<string>(); // Track test users for cleanup

        //    [OneTimeSetUp]
        //    public void OneTimeSetUp()
        //    {
        //        _factory = new CustomWebApplicationFactory();
        //        _client = _factory.CreateClient();
        //    }

        //    [Test]
        //    public async Task Login_WithValidCredentials_ReturnsSuccess()
        //    {
        //        // Arrange
        //        var registerModel = new Register
        //        {
        //            Username = "testuser",
        //            Email = "testuser@example.com",
        //            Password = "Test@123"
        //        };
        //        await _client.PostAsJsonAsync("/api/Account/register", registerModel);
        //        _testUsers.Add(registerModel.Email); // Track for cleanup

        //        var loginModel = new Login
        //        {
        //            Email = "testuser@example.com",
        //            Password = "Test@123"
        //        };

        //        // Act
        //        var response = await _client.PostAsJsonAsync("/api/Account/login", loginModel);
        //        var content = await response.Content.ReadAsStringAsync();

        //        // Debugging logs
        //        Console.WriteLine($"Status Code: {response.StatusCode}");
        //        Console.WriteLine($"Response: {content}");

        //        // Assert
        //        Assert.That(response.IsSuccessStatusCode, Is.True);
        //    }

        //    [Test]
        //    public async Task Register_WithValidData_ReturnsSuccess()
        //    {
        //        // Arrange
        //        var model = new Register
        //        {
        //            Username = "testuser",
        //            Email = "testusr@example.com",
        //            Password = "Test@1234"
        //        };
        //        _testUsers.Add(model.Email); // Track for cleanup

        //        // Act
        //        var response = await _client.PostAsJsonAsync("/api/Account/register", model);
        //        var content = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine(content);  // Debug output

        //        // Assert
        //        Assert.That(response.IsSuccessStatusCode, Is.True,
        //            $"Request failed: {response.StatusCode}. Response: {content}");
        //    }

        //    [TearDown]
        //    public async Task Cleanup()
        //    {
        //        // Delete all test users after each test
        //        foreach (var email in _testUsers)
        //        {
        //            await _client.DeleteAsync($"/api/Account/delete?email={email}");
        //        }
        //        _testUsers.Clear();
        //    }

        //    [OneTimeTearDown]
        //    public void OneTimeTearDown()
        //    {
        //        _client?.Dispose();
        //        _factory?.Dispose();
        //    }

        //[Test]
        //public void register()
        //{

        //    IWebDriver driver = new ChromeDriver();
        //    driver.Navigate().GoToUrl("http://localhost:4200/");
        //    driver.Manage().Window.Maximize();
        //    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        //    var register = wait.Until(d => d.FindElement(By.Id("register")));
        //    register.Click();
        //    var username = wait.Until(d => d.FindElement(By.Id("text")));
        //    var email = wait.Until(d => d.FindElement(By.Id("email")));
        //    var password = wait.Until(d => d.FindElement(By.Id("password")));
        //    var signup = wait.Until(d => d.FindElement(By.Id("signup")));
        //    var e = "rahul1@gmail.com";
        //    var p = "rahulpand123";
        //    username.SendKeys("rahulpandat");
        //    email.SendKeys(e.ToString());
        //    password.SendKeys(p.ToString());
        //    signup.Click();

        //}
       // [Test]
       // public void Invaild_email_register()
       // {

       //     IWebDriver driver = new ChromeDriver();
       //     driver.Navigate().GoToUrl("http://localhost:4200/");
       //     driver.Manage().Window.Maximize();
       //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
       //     var register = wait.Until(d => d.FindElement(By.Id("register")));
       //     register.Click();
       //     var username = wait.Until(d => d.FindElement(By.Id("text")));
       //     var email = wait.Until(d => d.FindElement(By.Id("email")));
       //     var password = wait.Until(d => d.FindElement(By.Id("password")));
       //     var signup = wait.Until(d => d.FindElement(By.Id("signup")));
       //     var e = "rahul1@gmail";
       //     var p = "rahulpand123";
       //     username.SendKeys("rahulpandat");
       //     email.SendKeys(e.ToString());
       //     password.SendKeys(p.ToString());
       //     signup.Click();

       // }
       //[Test]
       // public void  Ivalid_password_register()
       // {

       //     IWebDriver driver = new ChromeDriver();
       //     driver.Navigate().GoToUrl("http://localhost:4200/");
       //     driver.Manage().Window.Maximize();
       //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
       //     var register = wait.Until(d => d.FindElement(By.Id("register")));
       //     register.Click();
       //     var username = wait.Until(d => d.FindElement(By.Id("text")));
       //     var email = wait.Until(d => d.FindElement(By.Id("email")));
       //     var password = wait.Until(d => d.FindElement(By.Id("password")));
       //     var signup = wait.Until(d => d.FindElement(By.Id("signup")));
       //     var e = "rahul1@gmail.com";
       //     var p = "rahul";
       //     username.SendKeys("rahulpandat");
       //     email.SendKeys(e.ToString());
       //     password.SendKeys(p.ToString());
       //     signup.Click();

       // }
       //   [Test]
       // public void login()//valid
       // {
       //     IWebDriver driver = new ChromeDriver();
       //     driver.Navigate().GoToUrl("http://localhost:4200/");
       //     driver.Manage().Window.Maximize();
       //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
       //     var email = wait.Until(d => d.FindElement(By.Id("email")));
       //     var password = wait.Until(d => d.FindElement(By.Id("password"))); // Adjust ID as needed
       //     var button = wait.Until(d => d.FindElement(By.Id("login")));
       //     email.SendKeys("rahul1@gmail.com");
       //     password.SendKeys("rahulpand123");
            
       //     button.Click();
           
       //     //email.SendKeys(Keys.Return);
       //     //password.SendKeys(Keys.Return);
       //     //button.SendKeys(Keys.Return);
       // }
       // [Test]
       // public void Invalid_Email_Login() {
       //     IWebDriver driver = new ChromeDriver();
       //     driver.Navigate().GoToUrl("http://localhost:4200/");
       //     driver.Manage().Window.Maximize();
       //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
       //     var email = wait.Until(d => d.FindElement(By.Id("email")));
       //     var password = wait.Until(d => d.FindElement(By.Id("password")));
       //     var button = wait.Until(d => d.FindElement(By.Id("login")));

       //     string emailValue = "rahul1@gmail";
       //     string passwordValue = "rahulpand123";

       //     email.SendKeys(emailValue.ToString());
       //     password.SendKeys(passwordValue.ToString());
       //     button.Click();

            
       // }
       // [Test]
       // public void Invalid_Email_Password()
       // {
       //     IWebDriver driver = new ChromeDriver();
       //     driver.Navigate().GoToUrl("http://localhost:4200/");
       //     driver.Manage().Window.Maximize();
       //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
       //     var email = wait.Until(d => d.FindElement(By.Id("email")));
       //     var password = wait.Until(d => d.FindElement(By.Id("password")));
       //     var button = wait.Until(d => d.FindElement(By.Id("login")));

       //     string emailValue = "rahul1@gmail.com";
       //     string passwordValue = "rahulpand";

       //     email.SendKeys(emailValue.ToString());
       //     password.SendKeys(passwordValue.ToString());
       //     button.Click();

       // }
       // [Test]
       // public void Dashboard_Shows_Username()
       // {
       //     IWebDriver driver = new ChromeDriver();
       //     driver.Navigate().GoToUrl("http://localhost:4200/");
       //     driver.Manage().Window.Maximize();

       //     WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

       //     // 🔐 Login
       //     var email = wait.Until(d => d.FindElement(By.Id("email")));
       //     var password = driver.FindElement(By.Id("password"));
       //     var loginBtn = driver.FindElement(By.Id("login"));

       //     string emailValue = "rahul1@gmail.com";
       //     string passwordValue = "rahulpand123";
       //     string expectedUsername = "rahulpandat"; // Change this if the dashboard shows something else (e.g., just 'rahul')

       //     email.SendKeys(emailValue);
       //     password.SendKeys(passwordValue);
       //     loginBtn.Click();

       //     // ✅ Wait for dashboard to load and show username
       //     var usernameElement = wait.Until(d => d.FindElement(By.Id("username"))); // Use the actual ID or unique selector

       //     // Assert it contains the correct username
       //     Assert.That(usernameElement.Text, Does.Contain(expectedUsername));

       //     driver.Quit();
        //}
  
       [Test]
        public void Dashboard_Should_Not_Load_Without_Login()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/dashboard"); // or your dashboard URL
            driver.Manage().Window.Maximize();

            WebDriverWait wait =new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            // 🔍 Check if redirected to login or see an unauthorized message
            bool isLoginDisplayed = wait.Until(d => d.FindElement(By.Id("login"))).Displayed;

            //Assert.That(isLoginDisplayed,Is.True, message : "User should have to login first");
            IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("alert('User should have to login first');");

        }

       // [Test]
        public void Dashboard_Should_Display_Username_Element()
        {
            IWebDriver driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:4200/");
            driver.Manage().Window.Maximize();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var email = wait.Until(d => d.FindElement(By.Id("email")));
            var password = driver.FindElement(By.Id("password"));
            var loginBtn = driver.FindElement(By.Id("login"));

            email.SendKeys("rahul1@gmail.com");
            password.SendKeys("rahulpand123");
            loginBtn.Click();

            try
            {
                var usernameElement = wait.Until(d => d.FindElement(By.Id("username")));
                Assert.That(usernameElement.Displayed, Is.True, "Username should be displayed after login.");
            }
            catch (WebDriverTimeoutException)
            {
                Assert.Fail("Username element was not found on the dashboard.");
            }

            driver.Quit();
        }

    }

}