using Newtonsoft.Json;
using Pawn_Shop.Dtos;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            string httpResponseBody = "";
            list = new ObservableCollection<T>();

            try
            {
                //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                
                httpResponse.EnsureSuccessStatusCode();
                
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
                list = JsonConvert.DeserializeObject<ObservableCollection<T>>(httpResponseBody);
                
                return list;
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }

        public async Task<bool> Save(NRCTownship newNrcTownship)
        {
            try
            {
                // Construct the HttpClient and Uri
                HttpClient httpClient = new HttpClient();
                Uri requestUri = new Uri(baseUri);

                // Construct the given object as JSON to post
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(newNrcTownship), UnicodeEncoding.Utf8, "application/json");

                // Post the JSON and await for a response
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, content);

                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                // httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                Debug.WriteLine("Failed");
                return false;
            }
        }

        public async Task<bool> Update(NRCTownship updatedNrcTownship)
        {
            try
            {
                // Construct the HttpClient and Uri
                HttpClient httpClient = new HttpClient();
                Uri requestUri = new Uri(baseUri);

                // Construct the JSON to post
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(updatedNrcTownship), UnicodeEncoding.Utf8, "application/json");

                // Post the JSON and await for a response
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri, content);

                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed");
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
                // `logging`: httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
                return false;
            }
        }
    }
}
