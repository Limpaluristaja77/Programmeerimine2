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
using Microsoft.VisualStudio.TestPlatform.Utilities;


namespace KooliProjekt.UnitTests.ControllerTests
{
    public class MaterialsControllerTests
    {
        private readonly Mock<IMaterialsService> _materialsServiceMock;
        private readonly MaterialsController _controller;


        public MaterialsControllerTests()
        {
            _materialsServiceMock = new Mock<IMaterialsService>();
            _controller = new MaterialsController(_materialsServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Material>
    {
        new Material { Id = 1, Name = "Nails", Unit = "200", UnitCost = 10, Manufacturer = "Nails.co" },
        new Material { Id = 2, Name = "Tape", Unit = "30", UnitCost = 200, Manufacturer = "Sticky White" }
    };
            var pagedResult = new PagedResult<Material> { Results = data };
            _materialsServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);

            var model = result.Model as MaterialIndexModel;
            Assert.NotNull(model);
            Assert.Equal(2, model.Data.Count());
            Assert.Equal("Nails", model.Data.First().Name);
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
            var list = (Material)null;
            _materialsServiceMock
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
            var list = new Material { Id = id };
            _materialsServiceMock
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
            var material = new Material
            {
                Id = 1,
                Name = "Bat Wings",
                Unit = "1",
                UnitCost = 40,
                Manufacturer = "Bat Eaters"
            };

            _materialsServiceMock.Setup(service => service.Save(material));

            var controller = new MaterialsController(_materialsServiceMock.Object);

            // Act
            var result = await controller.Create(material);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_model_state_is_invalid_returns_view_with_model()
        {
            // Arrange
            var material = new Material
            {
                Id = 1,
                Name = "",
                Unit = "15",
                UnitCost = 460,
                Manufacturer = "ChingChong"
            };

            var _materialServiceMock = new Mock<IMaterialsService>();
            var _controller = new MaterialsController(_materialsServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = await _controller.Create(material);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(material, viewResult.Model);
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
            var list = (Material)null;
            _materialsServiceMock
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
            var list = new Material { Id = id };
            _materialsServiceMock
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
        public async Task Edit_should_return_notfound_when_id_mismatches()
        {
            // Arrange
            int id = 1;
            var materialToEdit = new Material { Id = 2 };

            // Act
            var result = await _controller.Edit(id, materialToEdit);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_view_when_model_state_is_invalid()
        {
            // Arrange
            int materialId = 1;
            var invalidMaterial = new Material
            {
                Id = materialId,
                Name = "",
                Unit = "1",
                UnitCost = 1500,
                Manufacturer = "Rocketship with Dildo"
            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Edit(materialId, invalidMaterial) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invalidMaterial, result.Model);
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task Edit_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int materialId = 1;
            var materialToEdit = new Material
            {
                Id = materialId,
                Name = "Big DoublePen Dildo With 60Hz Screen",
                Unit = "1",
                UnitCost = 500,
                Manufacturer = "Bat Soup People"
            };
            _materialsServiceMock.Setup(service => service.Save(materialToEdit));

            // Act
            var result = await _controller.Edit(materialId, materialToEdit) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
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
            var list = (Material)null;
            _materialsServiceMock
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
            var list = new Material { Id = id };
            _materialsServiceMock
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
            int materialId = 1;
            _materialsServiceMock.Setup(service => service.Delete(materialId)).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(materialId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _materialsServiceMock.VerifyAll();
        }


    }
}
