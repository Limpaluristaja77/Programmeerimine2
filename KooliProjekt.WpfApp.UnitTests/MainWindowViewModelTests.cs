using KooliProjekt.PublicApi.Api;
using KooliProjekt.WpfApp;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kooliprojekt.WpfApp.UnitTests
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<IApiClient> _apiMock;
        private readonly MainWindowViewModel _vm;

        public MainWindowViewModelTests()
        {
            _apiMock = new Mock<IApiClient>();
            _vm = new MainWindowViewModel(_apiMock.Object);
        }

        [Fact]
        public async Task SaveCommand_saves_panel_when_selectedItem_is_not_null()
        {
            // Arrange
            var panel = new Panel { Id = 0, Name = "Panel" };
            _vm.SelectedItem = panel;

            _apiMock.Setup(x => x.Save(It.IsAny<Panel>()))
                    .ReturnsAsync(new Result());

            _apiMock.Setup(x => x.List())
                    .ReturnsAsync(new Result<List<Panel>> { Value = new List<Panel> { panel } });

            // Act
            _vm.SaveCommand.Execute(panel);
            await Task.Delay(100);

            // Assert
            _apiMock.Verify(x => x.Save(panel), Times.Once);
            _apiMock.Verify(x => x.List(), Times.Once);
            Assert.Contains(panel, _vm.Lists);
        }

        [Fact]
        public async Task DeleteCommand_deletes_panel_when_confirmDelete_returns_true()
        {
            // Arrange
            var panel = new Panel { Id = 1, Name = "Panel" };
            _vm.SelectedItem = panel;
            _vm.Lists.Add(panel);
            _vm.ConfirmDelete = p => true;

            _apiMock.Setup(x => x.Delete(panel.Id))
                    .ReturnsAsync(new Result());

            // Act
            _vm.DeleteCommand.Execute(panel);
            await Task.Delay(100);

            // Assert
            _apiMock.Verify(x => x.Delete(panel.Id), Times.Once);
            Assert.DoesNotContain(panel, _vm.Lists);
            Assert.Null(_vm.SelectedItem);
        }

        [Fact]
        public async Task DeleteCommand_does_not_delete_if_confirmDelete_returns_false()
        {
            // Arrange
            var panel = new Panel { Id = 2, Name = "GuguGaga" };
            _vm.SelectedItem = panel;
            _vm.Lists.Add(panel);
            _vm.ConfirmDelete = p => false;

            // Act
            _vm.DeleteCommand.Execute(panel);
            await Task.Delay(100);

            // Assert
            _apiMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Never);
            Assert.Contains(panel, _vm.Lists);
        }

        [Fact]
        public async Task Load_populates_lists_when_successful()
        {
            // Arrange
            var panel = new Panel { Id = 3, Name = "Panel" };
            _apiMock.Setup(x => x.List())
                    .ReturnsAsync(new Result<List<Panel>> { Value = new List<Panel> { panel } });

            // Act
            await _vm.Load();

            // Assert
            Assert.Single(_vm.Lists);
            Assert.Equal("Panel", _vm.Lists[0].Name);
        }

        [Fact]
        public async Task Load_calls_OnError_when_api_fails()
        {
            // Arrange
            string errorMessage = null;
            _vm.OnError = e => errorMessage = e;

            _apiMock.Setup(x => x.List())
                    .ReturnsAsync(new Result<List<Panel>> { Error = "API failed"});

            // Act
            await _vm.Load();

            // Assert
            Assert.Equal("API failed", errorMessage);
            Assert.Empty(_vm.Lists);
        }
    }
}
