using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientAppliaction.Networking
{
    public class AppWebRequests
    {
        public static async Task<KeyValuePair<HttpStatusCode, T>> GetRequest<T>(string link)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage result = await client.GetAsync(link);
                return new KeyValuePair<HttpStatusCode, T>(result.StatusCode,
                    JsonConvert.DeserializeObject<T>(result.Content.ReadAsStringAsync().Result));
            }
            catch (Exception e)
            {
                return new KeyValuePair<HttpStatusCode, T>(HttpStatusCode.InternalServerError, default);
            }
        }

        public static async Task<KeyValuePair<HttpStatusCode, T>> PostRequest<T>(string link, object content)
        {
            try
            {
                string json = JsonConvert.SerializeObject(content);
                HttpContent httpcontent = new StringContent(json, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.PostAsync(link, httpcontent);
                return new KeyValuePair<HttpStatusCode, T>(response.StatusCode, 
                    JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result));
            }
            catch (Exception e)
            {
                return new KeyValuePair<HttpStatusCode, T>(HttpStatusCode.InternalServerError, default);
            }
        }

        public static async Task<KeyValuePair<HttpStatusCode, T>> DeleteRequest<T>(string link)
        {
            try
            {
                HttpClient client = new HttpClient();
                var result = await client.DeleteAsync(link);
                return new KeyValuePair<HttpStatusCode, T>(result.StatusCode, default);
            }
            catch (Exception e)
            {
                return new KeyValuePair<HttpStatusCode, T>(HttpStatusCode.InternalServerError, default);
            }
        }
    }
}
