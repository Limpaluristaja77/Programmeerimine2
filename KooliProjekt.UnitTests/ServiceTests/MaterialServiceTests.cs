using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class MaterialServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IMaterialsRepository> _repositoryMock;
        private readonly MaterialsService _materialsService;

        public MaterialServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IMaterialsRepository>();
            _materialsService = new MaterialsService(_uowMock.Object);

            _uowMock.SetupGet(r => r.MaterialsRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_materials()
        {
            // Arrange
            var results = new List<Material>
            {
                new Material { Id = 1, Manufacturer = "ChingaKidzz", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069},
                new Material { Id = 2, Manufacturer = "ChingaKidzz2", Name = "Chong Wong Gong2", Unit = "1", UnitCost = 42069}
            };
            var pagedResult = new PagedResult<Material> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _materialsService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_material_for_valid_id()
        {
            // Arrange
            var material = new Material { Id = 1, Manufacturer = "GoonerZinzz", Name = "Wong Gong", Unit = "1", UnitCost = 42069 };
            _repositoryMock.Setup(r => r.Get(It.IsAny<int?>())).ReturnsAsync(material);

            // Act
            var result = await _materialsService.Get(1);

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
            var result = await _materialsService.Includes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Save_should_call_repository_save()
        {
            // Arrange
            var material = new Material { Id = 1, Manufacturer = "Biggas", Name = "NIGails", Unit = "1", UnitCost = 100 };
            _repositoryMock.Setup(r => r.Save(It.IsAny<Material>())).Returns(Task.CompletedTask);

            // Act
            await _materialsService.Save(material);

            // Assert
            _repositoryMock.Verify(r => r.Save(material), Times.Once);
        }

        [Fact]
        public async Task Delete_should_call_repository_delete_with_correct_id()
        {
            // Arrange
            int materialId = 1;
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _materialsService.Delete(materialId);

            // Assert
            _repositoryMock.Verify(r => r.Delete(materialId), Times.Once);
        }


    }
}