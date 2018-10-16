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

        public async Task<IEnumerable<Album>> GetAlbum(string id)
        {
            List<Album> result = null;
            var uri = GetUri(id);
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                
                result = Deserialize(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                result = new List<Album>();
            }
            return result;
        }

        public List<Album> Deserialize(string result)
        {
            return JsonConvert.DeserializeObject<List<Album>>(result);
        }

        public Uri GetUri(string id)
        {
            if (long.TryParse(id, out var converted))
            {
                return id != string.Empty ? new Uri(BaseUrl + $"?albumId={id}") : new Uri(BaseUrl);
            }
            else
            {
                throw new ArgumentException("Invalid input for Id, use numbers");
            }
        }
       
    }
}
