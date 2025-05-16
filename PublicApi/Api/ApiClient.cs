using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.PublicApi.Api
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

        public async Task<Result<Panel>> Get(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Panel>($"Panels/{id}");
            return new Result<Panel> { Value = response };
        }

        public async Task<Result> Save(Panel list)
        {
            var result = new Result();

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
                result.Error = ex.Message;
            }
            return result;

        }

        public async Task<Result> Delete(int id)
        {
            var result = new Result();
            try
            {
                await _httpClient.DeleteAsync("Panels/" + id);

            }
            catch (Exception ex)
            {
                result.Error = ex.Message;
            }
            return result;
        }
    }
}