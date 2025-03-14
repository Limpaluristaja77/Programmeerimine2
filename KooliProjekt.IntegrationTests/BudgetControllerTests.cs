using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.IntegrationTests.Helpers;
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
            _client = Factory.CreateClient();
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
        public async Task Details_should_return_ok_when_building_and_budget_are_found()
        {
            // Arrange
            var building = new Buildings { Name = "gugugaga", MaterialId = 1, PanelId = 1 };
            _context.Buildings.Add(building);
            await _context.SaveChangesAsync();

            var client = new Client { Name = "Josephine", PhoneNumber = "696969"};
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            var service = new Service { Name = "Sloppy Toppy With A Twist", Provider = "Soome Juta", Unit = "1", UnitCost = 100};
            _context.Services.Add(service);
            await _context.SaveChangesAsync();


            var budget = new Budget
            { ClientId = client.Id, BuildingsId = building.Id, ServicesId = service.Id };
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            // Act
            using var response = await _client.GetAsync("/Budgets/Details/" + budget.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        
    }
}
