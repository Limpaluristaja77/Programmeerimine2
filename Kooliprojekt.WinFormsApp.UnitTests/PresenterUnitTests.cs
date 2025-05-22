using Xunit;
using Moq;
using System.Threading.Tasks;
using KooliProjekt.WinFormsApp;
using KooliProjekt.PublicApi;
using KooliProjekt.PublicApi.Api;
using System.Collections.Generic;

namespace KooliProjekt.Tests
{
    public class PanelPresenterTests
    {
        private readonly Mock<IApiClient> _mockApiClient;
        private readonly Mock<IPanelView> _mockPanelView;
        private readonly PanelPresenter _presenter;

        public PanelPresenterTests()
        {
            _mockApiClient = new Mock<IApiClient>();
            _mockPanelView = new Mock<IPanelView>();
            _presenter = new PanelPresenter(_mockPanelView.Object, _mockApiClient.Object);
        }

        [Fact]
        public void UpdateView_WithNullPanel_ClearsView()
        {
            // Act
            _presenter.UpdateView(null);

            // Assert
            _mockPanelView.VerifySet(v => v.Manufacturer = string.Empty);
            _mockPanelView.VerifySet(v => v.UnitCost = decimal.Zero);
            _mockPanelView.VerifySet(v => v.Unit = string.Empty);
            _mockPanelView.VerifySet(v => v.Name = string.Empty);
            _mockPanelView.VerifySet(v => v.Id = 0);
        }

        [Fact]
        public void UpdateView_WithPanel_SetsViewProperties()
        {
            var panel = new Panel
            {
                Id = 1,
                Name = "Panel",
                Unit = "1",
                UnitCost = 123.45M,
                Manufacturer = "Power"
            };

            // Act
            _presenter.UpdateView(panel);

            // Assert
            _mockPanelView.VerifySet(v => v.Id = panel.Id);
            _mockPanelView.VerifySet(v => v.Name = panel.Name);
            _mockPanelView.VerifySet(v => v.Unit = panel.Unit);
            _mockPanelView.VerifySet(v => v.UnitCost = panel.UnitCost);
            _mockPanelView.VerifySet(v => v.Manufacturer = panel.Manufacturer);
        }

        [Fact]
        public async Task Load_CallsApiAndSetsPanels()
        {
            // Arrange
            var panelList = new List<Panel>
            {
                new Panel { Id = 1, Name = "Panel", Unit = "1", UnitCost = 100, Manufacturer = "LEELEE" }
            };

            var apiResponse = new Result<List<Panel>>
            {
                Value = panelList
            };
            _mockApiClient.Setup(a => a.List()).ReturnsAsync(apiResponse);

            // Act
            await _presenter.Load();

            // Assert
            _mockPanelView.VerifySet(v => v.Panels = panelList);
        }
    }
}
