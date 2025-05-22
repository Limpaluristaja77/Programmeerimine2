using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;


namespace KooliProjekt.IntegrationTests
{
    [Collection("Sequential")]
    public class BudgetControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public BudgetControllerTests()
        {

            var options = new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            };
            _client = Factory.CreateClient(options);
            _context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));
        }

        [Fact]
        public async Task Index_should_return_correct_response()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Budgets");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Budgets/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Budgets/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        [Fact]
        public async Task Details_should_return_ok_when_list_was_found()
        {
            // Arrange
            var material = new Material { Name = "Gong", Unit = "1", UnitCost = 100, Manufacturer = "Ling" };
            var panel = new Panel { Name = "Gong", Unit = "1", UnitCost = 100, Manufacturer = "Ling" };
            var building = new Buildings { Name = "Building", MaterialId = material.Id, PanelId = panel.Id };
            var client = new Client { Name = "Client", PhoneNumber = "12345678" };
            var service = new Service { Name = "Service", Provider = "gong", Unit = "1", UnitCost = 100 };

            _context.Buildings.Add(building);
            _context.Clients.Add(client);
            _context.Services.Add(service);

            var budget = new Budget
            {
                BuildingsId = building.Id,
                ClientId = client.Id,
                ServicesId = service.Id,
                Buildings = building,
                Client = client,
                Services = service
            };

            _context.Budgets.Add(budget);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Budgets/Details/" + budget.Id);
            
            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Theory]
        [InlineData("/Budgets/Details")]
        [InlineData("/Budgets/Details/100")]
        [InlineData("/Budgets/Delete")]
        [InlineData("/Budgets/Delete/100")]
        [InlineData("/Budgets/Edit")]
        [InlineData("/Budgets/Edit/100")]
        public async Task Should_return_notfound(string url)
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }


        [Fact]
        public async Task Create_should_save_new_list()
        {
            // Arrange
            var material = new Material { Name = "Gong", Unit = "1", UnitCost = 100, Manufacturer = "Ling" };
            var panel = new Panel { Name = "Gong", Unit = "1", UnitCost = 100, Manufacturer = "Ling" };
            _context.Materials.Add(material);
            _context.Panels.Add(panel);
            await _context.SaveChangesAsync();

            var building = new Buildings { Name = "Building", MaterialId = material.Id, PanelId = panel.Id };
            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            var client = new Client { Name = "Client", PhoneNumber = "12345678" };
            _context.Clients.Add(client);

            var service = new Service { Name = "Service", Provider = "gong", Unit = "1", UnitCost = 100 };
            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            var formValues = new Dictionary<string, string>
            {
               { "BuildingsId", building.Id.ToString() },
               { "ClientId", client.Id.ToString() },
               { "ServicesId", service.Id.ToString() },
               { "Date", "2024-12-12" }
            };
            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Budgets/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.Budgets.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
        }


        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("BuildingsId", "");
            formValues.Add("ClientId", "");
            formValues.Add("ServicesId", "");
            formValues.Add("Date", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Budgets/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.Budgets.Any());
        }
    }
}
