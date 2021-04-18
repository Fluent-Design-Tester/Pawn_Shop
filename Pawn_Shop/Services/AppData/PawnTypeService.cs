using Newtonsoft.Json;
using Pawn_Shop.Dtos;
using System;
using System.Collections.ObjectModel;
using Windows.Storage.Streams;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Pawn_Shop.Services.AppData
{

    class PawnTypeService
    {
        private string baseUri = "http://localhost:8080/api/types";

        public async Task<ObservableCollection<T>> GetByCategoryId<T>(ObservableCollection<T> list, string categoryId)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri + "?categoryId=" + categoryId);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            string httpResponseBody;
            try
            {
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<ObservableCollection<T>>(httpResponseBody);
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return null;
            }
        }

        public async Task<bool> Save(PawnType newPawnType)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri);

            try
            {
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(newPawnType), UnicodeEncoding.Utf8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, content);
                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(PawnType updatedPawnType)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri);

            try
            {
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(updatedPawnType), UnicodeEncoding.Utf8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri, content);
                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri + "/" + id);

            HttpResponseMessage httpResponse = new HttpResponseMessage();

            try
            {
                httpResponse = await httpClient.DeleteAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
