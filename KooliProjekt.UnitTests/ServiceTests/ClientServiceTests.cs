using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ClientServiceTests
    {
        private readonly Mock<IUnitOfWork> _uowMock;
        private readonly Mock<IClientRepository> _repositoryMock;
        private readonly ClientService _clientService;

        public ClientServiceTests()
        {
            _uowMock = new Mock<IUnitOfWork>();
            _repositoryMock = new Mock<IClientRepository>();
            _clientService = new ClientService(_uowMock.Object);

            _uowMock.SetupGet(r => r.ClientRepository)
                    .Returns(_repositoryMock.Object);
        }

        [Fact]
        public async Task List_should_return_list_of_clients()
        {
            // Arrange
            var results = new List<Client>
            {
                new Client { Id = 1, Name = "Special Service", PhoneNumber = "22334342"},
                new Client { Id = 2, Name = "Special Service", PhoneNumber = "22334342"}
            };
            var pagedResult = new PagedResult<Client> { Results = results };
            _repositoryMock.Setup(r => r.List(It.IsAny<int>(), It.IsAny<int>()))
                           .ReturnsAsync(pagedResult);

            // Act
            var result = await _clientService.List(1, 10);

            // Assert
            Assert.Equal(pagedResult, result);
        }

        [Fact]
        public async Task Get_should_return_client_for_valid_id()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "Jessica", PhoneNumber = "2232323232323" };
            _repositoryMock.Setup(r => r.Get(It.IsAny<int?>())).ReturnsAsync(client);

            // Act
            var result = await _clientService.Get(1);

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
            var result = await _clientService.Includes(1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Save_should_call_repository_save()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "jessica", PhoneNumber = "3323232323"};
            _repositoryMock.Setup(r => r.Save(It.IsAny<Client>())).Returns(Task.CompletedTask);

            // Act
            await _clientService.Save(client);

            // Assert
            _repositoryMock.Verify(r => r.Save(client), Times.Once);
        }

        [Fact]
        public async Task Delete_should_call_repository_delete_with_correct_id()
        {
            // Arrange
            int clientId = 1;
            _repositoryMock.Setup(r => r.Delete(It.IsAny<int>())).Returns(Task.CompletedTask);

            // Act
            await _clientService.Delete(clientId);

            // Assert
            _repositoryMock.Verify(r => r.Delete(clientId), Times.Once);
        }


    }
}