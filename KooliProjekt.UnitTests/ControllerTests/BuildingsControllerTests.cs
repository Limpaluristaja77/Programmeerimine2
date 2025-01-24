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
        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Details_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Buildings)null;
            _buildingsServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Details(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Details_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var list = new Buildings { Id = id };
            _buildingsServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Details"
            );
            Assert.Equal(list, result.Model);
        }
        [Fact]
        public void Create_should_return_view()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Create"
            );
        }

        [Fact]
        public async Task Create_model_state_is_valid_redirects_to_index()
        {
            // Arrange
            var buildings = new Buildings
            {
                Id = 1,
                Name = "House",
                PanelId = 1,
                MaterialId = 1,
            };

            _buildingsServiceMock.Setup(service => service.Save(buildings));

            var controller = new BuildingsController(_buildingsServiceMock.Object);

            // Act
            var result = await controller.Create(buildings);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_model_state_is_invalid_returns_view_with_model()
        {
            // Arrange
            var buildings = new Buildings
            {
                Id = 1,
                Name = "",
                PanelId = 1,
                MaterialId = 1,
            };

            var _buildingsServiceMock = new Mock<IBuildingsService>();
            var _controller = new BuildingsController(_buildingsServiceMock.Object);
            _controller.ModelState.AddModelError("ClientId", "ClientId is required.");

            // Act
            var result = await _controller.Create(buildings);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(buildings, viewResult.Model);
        }
        [Fact]
        public async Task Edit_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_mismatches()
        {
            // Arrange
            int id = 1;
            var buildingsToEdit = new Buildings { Id = 2 };

            // Act
            var result = await _controller.Edit(id, buildingsToEdit);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_view_when_model_state_is_invalid()
        {
            // Arrange
            int buildingsId = 1;
            var invalidBuildings = new Buildings
            {
                Id = buildingsId,
                Name = "",
                PanelId = 1,
                MaterialId = 1,

            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Edit(buildingsId, invalidBuildings) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invalidBuildings, result.Model);
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task Edit_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int buildingsId = 1;
            var buildingsToEdit = new Buildings
            {
                Id = buildingsId,
                Name = "House",
                MaterialId = 1,
                PanelId = 1,

            };
            _buildingsServiceMock.Setup(service => service.Save(buildingsToEdit));

            // Act
            var result = await _controller.Edit(buildingsId, buildingsToEdit) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }


        [Fact]
        public async Task Edit_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Buildings)null;
            _buildingsServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Edit(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Edit_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var list = new Buildings { Id = id };
            _buildingsServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Edit(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Edit"
            );
            Assert.Equal(list, result.Model);
        }
        [Fact]
        public async Task Delete_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task Delete_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Buildings)null;
            _buildingsServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Delete(id) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_view_with_model_when_list_was_found()
        {
            // Arrange
            int id = 1;
            var list = new Buildings { Id = id };
            _buildingsServiceMock
                .Setup(x => x.Get(id))
                .ReturnsAsync(list);

            // Act
            var result = await _controller.Delete(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(
                string.IsNullOrEmpty(result.ViewName) ||
                result.ViewName == "Delete"
            );
            Assert.Equal(list, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_deletes_panel_redirects_to_index()
        {
            // Arrange
            int buildingsId = 1;
            _buildingsServiceMock.Setup(service => service.Delete(buildingsId)).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(buildingsId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _buildingsServiceMock.VerifyAll();
        }

    }
}
