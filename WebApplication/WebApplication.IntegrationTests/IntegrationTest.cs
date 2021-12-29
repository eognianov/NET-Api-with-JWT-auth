using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WebApplication.Contracts.V1;
using WebApplication.Contracts.V1.InputModels;
using WebApplication.Contracts.V1.ViewModels;
using WebApplication.Data;

namespace WebApplication.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        protected readonly IServiceProvider _serviceProvider;
        
        public IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options =>
                        {
                            options.UseInMemoryDatabase("TestDb");
                        });
                    });
                });
            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequestInputModel
            {
                Email = "emotest@sap.com",
                Password = "Test123!"
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthSuccessViewModel>();
            if (registrationResponse?.Token == null)
            {
                return String.Empty;
            }

            return registrationResponse.Token;
        }

        public void Dispose()
        {
            using (var serviceScope = _serviceProvider.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<DataContext>();
                context?.Database.EnsureDeleted();
            }
        }
    }
}