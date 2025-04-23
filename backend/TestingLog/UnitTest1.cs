using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using NUnit.Framework;
using TestingLogin;
using TestingLogin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace TestingLog
{
    [TestFixture]
    public class AccountControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;
        private List<string> _testUsers = new List<string>(); // Track test users for cleanup

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task Login_WithValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var registerModel = new Register
            {
                Username = "testuser",
                Email = "testuser@example.com",
                Password = "Test@123"
            };
            await _client.PostAsJsonAsync("/api/Account/register", registerModel);
            _testUsers.Add(registerModel.Email); // Track for cleanup

            var loginModel = new Login
            {
                Email = "testuser@example.com",
                Password = "Test@123"
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/login", loginModel);
            var content = await response.Content.ReadAsStringAsync();

            // Debugging logs
            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Response: {content}");

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True);
        }

        [Test]
        public async Task Register_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var model = new Register
            {
                Username = "testuser",
                Email = "testusr@example.com",
                Password = "Test@1234"
            };
            _testUsers.Add(model.Email); // Track for cleanup

            // Act
            var response = await _client.PostAsJsonAsync("/api/Account/register", model);
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(content);  // Debug output

            // Assert
            Assert.That(response.IsSuccessStatusCode, Is.True,
                $"Request failed: {response.StatusCode}. Response: {content}");
        }

        [TearDown]
        public async Task Cleanup()
        {
            // Delete all test users after each test
            foreach (var email in _testUsers)
            {
                await _client.DeleteAsync($"/api/Account/delete?email={email}");
            }
            _testUsers.Clear();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _client?.Dispose();
            _factory?.Dispose();
        }
    }
}