using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class PanelServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IPanelsRepository> _repositoryMock;
        private readonly PanelsService _panelsService;

        public PanelServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IPanelsRepository>();
            _panelsService = new PanelsService(_uowMock.Object);

            _uowMock.SetupGet(r => r.PanelsRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_panels()
        {
            // Arrange
            var results = new List<Panel>
            {
                new Panel { Id = 1, Manufacturer = "MEMEME ME", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069},
                new Panel { Id = 2, Manufacturer = "LAPSA", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069}
            };
            var pagedResult = new PagedResult<Panel> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _panelsService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_panel_for_valid_id()
        {
            // Arrange
            var panel = new Panel { Id = 1, Manufacturer = "MEMEMEME", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069 };
            _repositoryMock.Setup(r => r.Get(It.IsAny<int?>())).ReturnsAsync(panel);

            // Act
            var result = await _panelsService.Get(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task Includes_should_return_true_if_id_exists()
        {
            // Arrange
            _repositoryMock.Setup(r => r.Includes(It.IsAny<int>())).ReturnsAsync(true);

            // Act
            var result = await _panelsService.Includes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Save_should_call_repository_save()
        {
            // Arrange
            var panel = new Panel { Id = 1, Manufacturer = "Biggas", Name = "lepanelos", Unit = "1", UnitCost = 100 };
            _repositoryMock.Setup(r => r.Save(It.IsAny<Panel>())).Returns(Task.CompletedTask);

            // Act
            await _panelsService.Save(panel);

            // Assert
            _repositoryMock.Verify(r => r.Save(panel), Times.Once);
        }

        [Fact]
        public async Task Delete_should_call_repository_delete_with_correct_id()
        {
            // Arrange
            int panelId = 1;
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _panelsService.Delete(panelId);

            // Assert
            _repositoryMock.Verify(r => r.Delete(panelId), Times.Once);
        }


    }
}