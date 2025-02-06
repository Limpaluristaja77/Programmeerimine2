using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ServiceServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IServiceRepository> _repositoryMock;
        private readonly ServicesService _servicesService;

        public ServiceServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IServiceRepository>();
            _servicesService = new ServicesService(_uowMock.Object);

            _uowMock.SetupGet(r => r.ServiceRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_services()
        {
            // Arrange
            var results = new List<Service>
            {
                new Service { Id = 1, Provider = "USSR", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069},
                new Service { Id = 2, Provider = "LAPSA", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069}
            };
            var pagedResult = new PagedResult<Service> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _servicesService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_service_for_valid_id()
        {
            // Arrange
            var service = new Service { Id = 1, Provider = "Guguga", Name = "Chong Wong Gong", Unit = "1", UnitCost = 42069 };
            _repositoryMock.Setup(r => r.Get(It.IsAny<int?>())).ReturnsAsync(service);

            // Act
            var result = await _servicesService.Get(1);

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
            var result = await _servicesService.Includes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Save_should_call_repository_save()
        {
            // Arrange
            var service = new Service { Id = 1, Provider = "Biggas", Name = "Jalapenos", Unit = "1", UnitCost = 100 };
            _repositoryMock.Setup(r => r.Save(It.IsAny<Service>())).Returns(Task.CompletedTask);

            // Act
            await _servicesService.Save(service);

            // Assert
            _repositoryMock.Verify(r => r.Save(service), Times.Once);
        }

        [Fact]
        public async Task Delete_should_call_repository_delete_with_correct_id()
        {
            // Arrange
            int serviceId = 1;
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _servicesService.Delete(serviceId);

            // Assert
            _repositoryMock.Verify(r => r.Delete(serviceId), Times.Once);
        }


    }
}