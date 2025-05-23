using KooliProjekt.PublicApi.Api;
using System.Collections.Generic;
using System.Threading.Tasks;
using Panel = KooliProjekt.PublicApi.Api.Panel;

namespace KooliProjekt.WinFormsApp
{
    public class PanelPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IPanelView _panelView;

        public PanelPresenter(IPanelView panelView, IApiClient apiClient)
        {
            _apiClient = apiClient;
            _panelView = panelView;

            _panelView.Presenter = this;
        }

        public async Task Load()
        {
            var response = await _apiClient.List();
            _panelView.Panels = response.Value;
        }

        public async Task Delete(int panelId)
        {
            await _apiClient.Delete(panelId);
        }

        public async Task Save(Panel panel)
        {
            await _apiClient.Save(panel);
        }

        public void UpdateView(Panel panel)
        {
            if (panel == null)
            {
                _panelView.Manufacturer = string.Empty;
                _panelView.UnitCost = 0;
                _panelView.Unit = string.Empty;
                _panelView.Name = string.Empty;
                _panelView.Id = 0;
            }
            else
            {
                _panelView.Id = panel.Id;
                _panelView.Name = panel.Name;
                _panelView.Unit = panel.Unit;
                _panelView.UnitCost = panel.UnitCost;
                _panelView.Manufacturer = panel.Manufacturer;
            }
        }
    }
}
