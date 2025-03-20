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
    public class PanelControllerTests : TestBase
    {
        private readonly HttpClient _client;
        private readonly ApplicationDbContext _context;

        public PanelControllerTests()
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
            using var response = await _client.GetAsync("/Panels");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Details_should_return_notfound_when_list_was_not_found()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Panels/Details/100");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange

            // Act
            using var response = await _client.GetAsync("/Panels/Details/");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Details_should_return_ok_when_list_was_found()
        {
            // Arrange
            var list = new Panel {Name = "Le panel", Manufacturer = "ChingChang", Unit = "1", UnitCost = 121 };
            _context.Panels.Add(list);
            _context.SaveChanges();

            // Act
            using var response = await _client.GetAsync("/Panels/Details/" + list.Id);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Theory]
        [InlineData("/Panels/Details")]
        [InlineData("/Panels/Details/100")]
        [InlineData("/Panels/Delete")]
        [InlineData("/Panels/Delete/100")]
        [InlineData("/Panels/Edit")]
        [InlineData("/Panels/Edit/100")]
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
            var formValues = new Dictionary<string, string>();
            formValues.Add("Name", "Test");
            formValues.Add("Manufacturer", "OK");
            formValues.Add("Unit", "1");
            formValues.Add("UnitCost", "1");


            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Panels/Create", content);

            // Assert
            Assert.True(
                response.StatusCode == HttpStatusCode.Redirect ||
                response.StatusCode == HttpStatusCode.MovedPermanently);

            var list = _context.Panels.FirstOrDefault();
            Assert.NotNull(list);
            Assert.NotEqual(0, list.Id);
            Assert.Equal("Test", list.Name);
        }

        [Fact]
        public async Task Create_should_not_save_invalid_new_list()
        {
            // Arrange
            var formValues = new Dictionary<string, string>();
            formValues.Add("Id", "");
            formValues.Add("Name", "");
            formValues.Add("Manufacturer", "");
            formValues.Add("Unit", "");

            using var content = new FormUrlEncodedContent(formValues);

            // Act
            using var response = await _client.PostAsync("/Panels/Create", content);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.False(_context.Panels.Any());
        }
    }
}
