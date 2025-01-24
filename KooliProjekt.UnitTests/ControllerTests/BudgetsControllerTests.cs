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
    public class BudgetsControllerTests
    {
        private readonly Mock<IBudgetService> _budgetServiceMock;
        private readonly BudgetsController _controller;



        public BudgetsControllerTests()
        {
            _budgetServiceMock = new Mock<IBudgetService>();
            _controller = new BudgetsController(_budgetServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_correct_view_with_data()
        {
            // Arrange
            int page = 1;
            var data = new List<Budget>
    {
        new Budget { 
            Id = 1, 
            ClientId = 1, 
            BuildingsId = 1, 
            ServicesId = 2, 
        },
        new Budget { 
            Id = 2, 
            ClientId = 2, 
            BuildingsId = 2, 
            ServicesId = 2
        },
    };
            var pagedResult = new PagedResult<Budget> { Results = data };
            _budgetServiceMock.Setup(x => x.List(page, It.IsAny<int>(), null)).ReturnsAsync(pagedResult);

            // Act
            var result = await _controller.Index(page) as ViewResult;

            // Assert
            Assert.NotNull(result);

            var model = result.Model as BudgetIndexModel;
            Assert.NotNull(model);
            Assert.Equal(2, model.Data.Count());
            Assert.Equal(1, model.Data.First().ClientId);
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
            var list = (Budget)null;
            _budgetServiceMock
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
            var list = new Budget { Id = id };
            _budgetServiceMock
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
            var budget = new Budget
            {
                Id = 1,
                ClientId = 1,
                BuildingsId = 1,
                ServicesId = 1,
            };

            _budgetServiceMock.Setup(service => service.Save(budget));

            var controller = new BudgetsController(_budgetServiceMock.Object);

            // Act
            var result = await controller.Create(budget);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Create_model_state_is_invalid_returns_view_with_model()
        {
            // Arrange
            var budget = new Budget
            {
                Id = 1,
                ClientId = 0,
                BuildingsId = 1,
                ServicesId = 1,
            };

            var _budgetServiceMock = new Mock<IBudgetService>();
            var _controller = new BudgetsController(_budgetServiceMock.Object);
            _controller.ModelState.AddModelError("ClientId", "ClientId is required.");

            // Act
            var result = await _controller.Create(budget);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(budget, viewResult.Model);
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
            var budgetToEdit = new Budget { Id = 2 };

            // Act
            var result = await _controller.Edit(id, budgetToEdit);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_view_when_model_state_is_invalid()
        {
            // Arrange
            int budgetId = 1;
            var invalidBudget = new Budget
            {
                Id = budgetId,
                ClientId = 0,
                BuildingsId = 1,
                ServicesId = 1,

            };

            _controller.ModelState.AddModelError("ClientId", "ClientId is required");

            // Act
            var result = await _controller.Edit(budgetId, invalidBudget) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(invalidBudget, result.Model);
            Assert.False(result.ViewData.ModelState.IsValid);
        }

        [Fact]
        public async Task Edit_should_save_and_redirect_when_model_is_valid()
        {
            // Arrange
            int budgetId = 1;
            var budgetToEdit = new Budget
            {
                Id = budgetId,
                ClientId = 1,
                BuildingsId = 1,
                ServicesId = 1,

            };
            _budgetServiceMock.Setup(service => service.Save(budgetToEdit));

            // Act
            var result = await _controller.Edit(budgetId, budgetToEdit) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);
        }


        [Fact]
        public async Task Edit_should_return_notfound_when_list_is_missing()
        {
            // Arrange
            int id = 1;
            var list = (Budget)null;
            _budgetServiceMock
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
            var list = new Budget { Id = id };
            _budgetServiceMock
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
            var list = (Budget)null;
            _budgetServiceMock
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
            var list = new Budget { Id = id };
            _budgetServiceMock
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
            int budgetId = 1;
            _budgetServiceMock.Setup(service => service.Delete(budgetId)).Verifiable();

            // Act
            var result = await _controller.DeleteConfirmed(budgetId) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _budgetServiceMock.VerifyAll();
        }

    }
}
