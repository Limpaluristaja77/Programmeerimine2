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
using KooliProjekt.Models;


namespace KooliProjekt.UnitTests.ControllerTests
{
    public class BuildingsControllerTests
    {
        private readonly Mock<IBuildingsService> _buildingsServiceMock;
        private readonly BuildingsController _controller;


        public BuildingsControllerTests()
        {
            _buildingsServiceMock = new Mock<IBuildingsService>();
            _controller = new BuildingsController(_buildingsServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Buildings>
    {
        new Buildings { Id = 1, Name = "House", PanelId = 1, MaterialId = 2},
        new Buildings { Id = 2, Name = "School", PanelId = 2, MaterialId = 2}
    };
            var pagedResult = new PagedResult<Buildings> { Results = data };
            _buildingsServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);

            var model = result.Model as BuildingIndexModel;
            Assert.NotNull(model);
            Assert.Equal(2, model.Data.Count());
            Assert.Equal("House", model.Data.First().Name);
        }

    }
}
