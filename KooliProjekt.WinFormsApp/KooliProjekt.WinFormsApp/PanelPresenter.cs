using KooliProjekt.WinFormsApp.Api;

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

            panelView.Presenter = this;
        }

        public void UpdateView(Api.Panel list)
        {
            if (list == null)
            {
                _panelView.Manufacturer = string.Empty;
                _panelView.UnitCost = decimal.Zero;
                _panelView.Unit = string.Empty;
                _panelView.Name = string.Empty;
                _panelView.Id = 0;
            }
            else
            {
                _panelView.Id = list.Id;
                _panelView.Name = list.Name;
                _panelView.Unit = list.Unit;
                _panelView.UnitCost = list.UnitCost;
                _panelView.Manufacturer = list.Manufacturer;
            }
        }

        public async Task Load()
        {
            var panels = await _apiClient.List();

            _panelView.Panels = panels.Value;
        }
    }
}