using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlbumFinder
{
    public class AlbumFinderClient
    {
        static HttpClient client = new HttpClient();
        const string BaseUrl = "https://jsonplaceholder.typicode.com/photos";

        public static async Task<IEnumerable<Album>> GetAlbum(string id)
        {
            List<Album> result = null;
            var uri = GetUri(id);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<List<Album>>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                result = new List<Album>();
            }
            return result;
        }

        private static Uri GetUri(string id)
        {
            return id != string.Empty ? new Uri(BaseUrl + $"/?albumId={id}") : new Uri(BaseUrl);
        }
    }
}
