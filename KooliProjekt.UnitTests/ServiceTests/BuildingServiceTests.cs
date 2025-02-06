using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class BuildingServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IBuildingsRepository> _repositoryMock;
        private readonly BuildingsService _buildingsService;

        public BuildingServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IBuildingsRepository>();
            _buildingsService = new BuildingsService(_uowMock.Object);

            _uowMock.SetupGet(r => r.BuildingsRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_buildings()
        {
            // Arrange
            var results = new List<Buildings>
            {
                new Buildings { Id = 1, Name = "GoonHouse",MaterialId = 1, PanelId = 1},
                new Buildings { Id = 2, Name = "BoonerHoose",MaterialId = 2, PanelId = 2}
            };
            var pagedResult = new PagedResult<Buildings> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _buildingsService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_building_for_valid_id()
        {
            // Arrange
            var building = new Buildings { Id = 1, MaterialId = 1, PanelId = 1 };
            _repositoryMock.Setup(r => r.Get(It.IsAny<int?>())).ReturnsAsync(building);

            // Act
            var result = await _buildingsService.Get(1);

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
            var result = await _buildingsService.Includes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Save_should_call_repository_save()
        {
            // Arrange
            var building = new Buildings { Id = 1, Name = "Hoose" ,MaterialId = 1, PanelId = 1 };
            _repositoryMock.Setup(r => r.Save(It.IsAny<Buildings>())).Returns(Task.CompletedTask);

            // Act
            await _buildingsService.Save(building);

            // Assert
            _repositoryMock.Verify(r => r.Save(building), Times.Once);
        }

        [Fact]
        public async Task Delete_should_call_repository_delete_with_correct_id()
        {
            // Arrange
            int buildingId = 1;
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _buildingsService.Delete(buildingId);

            // Assert
            _repositoryMock.Verify(r => r.Delete(buildingId), Times.Once);
        }


    }
}