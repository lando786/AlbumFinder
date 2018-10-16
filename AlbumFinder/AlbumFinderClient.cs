using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlbumFinder
{
    public interface IAlbumFinderClient
    {
        Task<AlbumResponse> GetAlbum(string id);
    }
    public class AlbumFinderClient : IAlbumFinderClient
    {
        private IWebClient _client;
        private const string BaseUrl = "https://jsonplaceholder.typicode.com/photos";
        public AlbumFinderClient(IWebClient webClient)
        {
            _client = webClient;
        }
        public async Task<AlbumResponse> GetAlbum(string id)
        {
            List<Album> result = null;
            try
            {
                var uri = GetUri(id);
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    result = Deserialize(response.Content.ReadAsStringAsync().Result);
                    return result.Any()? new AlbumResponse()
                    {
                        Albums = result,
                        Response = ResponseCode.Ok
                    } : new AlbumResponse()
                         {
                             Response = ResponseCode.NotFound
                         };
                    ;
                }
                else
                {
                    return new AlbumResponse()
                    {
                        Response = ResponseCode.Unknown
                    };
                }
            }
            catch (ArgumentException e)
            {

                return new AlbumResponse()
                {
                    Response = ResponseCode.InvalidInput
                };
            }
        }

        public List<Album> Deserialize(string result)
        {
            return JsonConvert.DeserializeObject<List<Album>>(result);
        }

        public Uri GetUri(string id)
        {
            var trimmed = id.Trim();
            long converted;
            if (long.TryParse(trimmed, out converted) || trimmed == string.Empty)
            {
                return trimmed != string.Empty ? new Uri(BaseUrl + $"?albumId={trimmed}") : new Uri(BaseUrl);
            }
            else
            {
                throw new ArgumentException("Invalid input for Id, use numbers");
            }
        }
        
    }
}
