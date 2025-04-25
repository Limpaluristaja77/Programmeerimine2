using KooliProjekt.PublicApi;
using KooliProjekt.PublicApi.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace KooliProjekt.BlazorApp.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject]
        protected IApiClient apiClient { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected NavigationManager NavManager { get; set; }

        protected List<Panel> panels { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var result = await apiClient.List();
            panels = result.Value;
        }

        protected async Task Delete(int id)
        {
            bool confirmed = await JsRuntime.InvokeAsync<bool>("confirm", "Are you sure?");
            if (!confirmed)
            {
                return;
            }

            await apiClient.Delete(id);

            NavManager.Refresh();
        }
    }
}