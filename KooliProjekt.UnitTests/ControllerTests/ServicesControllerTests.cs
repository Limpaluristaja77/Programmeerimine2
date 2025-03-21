﻿using KooliProjekt.Services;
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
    public class ServicesControllerTests
    {
        private readonly Mock<IServicesService> _servicesServiceMock;
        private readonly ServiceController _controller;


        public ServicesControllerTests()
        {
            _servicesServiceMock = new Mock<IServicesService>();
            _controller = new ServiceController(_servicesServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Service>
    {
        new Service { Id = 1, Name = "Cleaning", Unit = "1", UnitCost = 500, Provider = "LittleMouse.co" },
        new Service { Id = 2, Name = "Demolish", Unit = "1", UnitCost = 1400, Provider = "DemoMan.co" }
    };
            var pagedResult = new PagedResult<Service> { Results = data };
            _servicesServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);

            var model = result.Model as ServiceIndexModel;
            Assert.NotNull(model);
            Assert.Equal(2, model.Data.Count());
            Assert.Equal("Cleaning", model.Data.First().Name);
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
            var list = (Service)null;
            _servicesServiceMock
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
            var list = new Service { Id = id };
            _servicesServiceMock
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
            var services = new Service
            {
                Id = 1,
                Name = "Titty",
                Unit = "1",
                UnitCost = 400,
                Provider = "Thai LadyBoy"
            };

            _servicesServiceMock.Setup(service => service.Save(services));

            var controller = new ServiceController(_servicesServiceMock.Object);

            // Act
            var result = await controller.Create(services);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_model_state_is_invalid_returns_view_with_model()
        {
            // Arrange
            var service = new Service
            {
                Id = 1,
                Name = "",
                Unit = "1",
                UnitCost = 150,
                Provider = "ChingChong"
            };

            var _servicesServiceMock= new Mock<IServicesService>();
            var _controller = new ServiceController(_servicesServiceMock.Object);
            _controller.ModelState.AddModelError("Name", "Name is required.");

            // Act
            var result = await _controller.Create(service);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(service, viewResult.Model);
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
            var list = (Service)null;
            _servicesServiceMock
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
            var list = new Service { Id = id };
            _servicesServiceMock
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
            var serviceToEdit = new Service { Id = 2 };

            // Act
            var result = await _controller.Edit(id, serviceToEdit);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_view_when_model_state_is_invalid()
        {
            // Arrange
            int serviceId = 1;
            var invalidService = new Service
            {
                Id = serviceId,
                Name = "",
                Unit = "1",
                UnitCost = 150,
                Provider = "Black Man"
            };

            _controller.ModelState.AddModelError("Name", "Name is required");

            // Act
            var result = await _controller.Edit(serviceId, invalidService) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invalidService, result.Model);
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task Edit_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int serviceId = 1;
            var serviceToEdit = new Service
            {
                Id = serviceId,
                Name = "Happy Ending",
                Unit = "1",
                UnitCost = 500,
                Provider = "Tahiti People"
            };
            _servicesServiceMock.Setup(service => service.Save(serviceToEdit));

            // Act
            var result = await _controller.Edit(serviceId, serviceToEdit) as RedirectToActionResult;

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
            var list = (Service)null;
            _servicesServiceMock
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
            var list = new Service { Id = id };
            _servicesServiceMock
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
            int serviceId = 1;
            _servicesServiceMock.Setup(service => service.Delete(serviceId)).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(serviceId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _servicesServiceMock.VerifyAll();
        }


    }
}
