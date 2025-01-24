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

            var model = result.Model as PanelIndexModel;
            Assert.NotNull(model);
            Assert.Equal(2, model.Data.Count()); 
            Assert.Equal("LePanel", model.Data.First().Name); 
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
            var list = (Panel) null;
            _panelsServiceMock
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
            var list = new Panel { Id = id };
            _panelsServiceMock
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
            var panels = new Panel
            {
                Id = 1,
                Name = "Panel",
                Unit = "1",
                UnitCost = 100,
                Manufacturer = "Manufacturer"
            };

            _panelsServiceMock.Setup(service => service.Save(panels));

            var _controller = new PanelsController(_panelsServiceMock.Object);

            // Act
            var result = await _controller.Create(panels);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_model_state_is_invalid_returns_view_with_model()
        {
            // Arrange
            var panels = new Panel
            {
                Id = 1,
                Name = "", 
                Unit = "1",
                UnitCost = 150,
                Manufacturer = "Manufacturer"
            };

            var _panelsServiceMock = new Mock<IPanelsService>();
            var _controller = new PanelsController(_panelsServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = await _controller.Create(panels);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(panels, viewResult.Model);  
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
        public async Task Edit_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Panel) null;
            _panelsServiceMock
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
            var list = new Panel { Id = id };
            _panelsServiceMock
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
        public async Task Edit_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int panelId = 1;
            var panelToEdit = new Panel
            {
                Id = panelId,
                Name = "PanelBlack",
                Unit = "30",
                UnitCost = 100,
                Manufacturer = "Tahiti"
            };
            _panelsServiceMock.Setup(service => service.Save(panelToEdit));

            // Act
            var result = await _controller.Edit(panelId, panelToEdit) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_id_mismatches()
        {
            // Arrange
            int id = 1;
            var panelToEdit = new Panel { Id = 2 };

            // Act
            var result = await _controller.Edit(id, panelToEdit);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_view_when_model_state_is_invalid()
        {
            // Arrange
            int panelId = 1;
            var invalidPanel = new Panel
            {
                Id = panelId,
                Name = "",
                Unit = "30",
                UnitCost = 100,
                Manufacturer = "Black Man Manufacturer"
            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Edit(panelId, invalidPanel) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invalidPanel, result.Model);
            Assert.False(result.ViewData.ModelState.IsValid);
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
            var list = (Panel) null;
            _panelsServiceMock
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
            var list = new Panel { Id = id };
            _panelsServiceMock
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
            int panelId = 1;
            _panelsServiceMock.Setup(service => service.Delete(panelId)).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(panelId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);  
            _panelsServiceMock.VerifyAll();
        }




    }
}
