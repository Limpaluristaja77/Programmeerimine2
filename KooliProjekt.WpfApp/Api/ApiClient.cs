using System.IO;
using System.Net.Http;
using System.Net.Http.Json;

namespace KooliProjekt.WpfApp.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7136/api/");
        }

        public async Task <Result<List<Panel>>> List()
        {
            var result = new Result<List<Panel>>();

            try
            {
                result.Value = await _httpClient.GetFromJsonAsync<List<Panel>>("Panels");
            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }

            return result;
        }

        public async Task Save(Panel list)
        {

            try
            {
                if (list.Id == 0)
                {
                    await _httpClient.PostAsJsonAsync("Panels", list);
                }
                else
                {
                    await _httpClient.PutAsJsonAsync("Panels/" + list.Id, list);

                }

            }
            catch(Exception ex)
            {

            }

        }

        public async Task Delete(int id)
        {
            try
            {
                await _httpClient.DeleteAsync("Panels/" + id);

            }
            catch (Exception ex)
            { 
                
            }
        }
    }
}