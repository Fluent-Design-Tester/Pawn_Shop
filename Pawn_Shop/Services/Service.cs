using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.Web.Http;

namespace Pawn_Shop.Services
{
    class Service
    {
        private readonly string baseUri = "http://localhost:8080";

        public Service(string uri)
        {
            this.baseUri += uri;
        }

        public async Task<ObservableCollection<T>> GetAll<T>(ObservableCollection<T> list, string path = "")
        {
            Debug.WriteLine(path);

            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri + path);

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

        public async Task<bool> Save<T>(T obj)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri);

            try
            {
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(obj), UnicodeEncoding.Utf8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, content);
                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
        public async Task<bool> Update<T>(T obj)
        {
            HttpClient httpClient = new HttpClient();
            Uri requestUri = new Uri(baseUri);

            try
            {
                HttpStringContent content = new HttpStringContent(JsonConvert.SerializeObject(obj), UnicodeEncoding.Utf8, "application/json");
                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(requestUri, content);
                httpResponseMessage.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
        public async Task<bool> DeleteById(int id)
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
