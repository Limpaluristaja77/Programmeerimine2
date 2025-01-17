using KooliProjekt.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KooliProjekt.Controllers;
using Xunit;
using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;


namespace KooliProjekt.UnitTests.ControllerTests
{
    public class PanelsControllerTests
    {
        private readonly Mock<IPanelsService> _panelsServiceMock;
        private readonly PanelsController _controller;


        public PanelsControllerTests()
        {
            _panelsServiceMock = new Mock<IPanelsService>();
            _controller = new PanelsController(_panelsServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Panel>
            {
                new Panel { Id = 1, Name = "LePanel", Unit = "30", UnitCost = 200, Manufacturer = "Tahiti White" },
                new Panel { Id = 2, Name = "Panel", Unit = "30", UnitCost = 200, Manufacturer = "Panhop White" }
            };
            var pagedResult = new PagedResult<Panel> { Results = data };
            _panelsServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(pagedResult, result.Model);


        }
    }
}
