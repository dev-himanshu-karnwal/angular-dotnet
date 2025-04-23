using Microsoft.AspNetCore.Mvc.Testing;

namespace TestingLogin
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test"); // Uses test-specific DB
        }
    }
}
