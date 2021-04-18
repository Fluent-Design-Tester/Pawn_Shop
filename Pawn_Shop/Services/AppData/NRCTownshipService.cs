using Newtonsoft.Json;
using Pawn_Shop.Dto;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Pawn_Shop.Services.AppData
{
    class NRCTownshipService
    {
        private string baseUri = "http://localhost:8080/api/nrc-townships";
 
        public async Task<ObservableCollection<T>> GetByRegionId<T>(ObservableCollection<T> list, string regionId)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri + "?regionId=" + regionId);

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

        public async Task<bool> Save(NRCTownship newNrcTownship)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri);
            
            try
            {
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(newNrcTownship), UnicodeEncoding.Utf8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, content);
                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Update(NRCTownship updatedNrcTownship)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri);
            
            try
            {
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(updatedNrcTownship), UnicodeEncoding.Utf8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri, content);
                httpResponseMessage.EnsureSuccessStatusCode();
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> Delete(int nrcTownshipId)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri + "/" + nrcTownshipId);
            
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
