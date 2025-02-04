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
        
    }
}