using AlbumFinder.Constants;
using AlbumFinder.Enums;
using AlbumFinder.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumFinder.Services
{
    public interface IAlbumFinderService
    {
        Task<AlbumResponse> GetAlbum(string id);
    }
    public class AlbumFinderClient : IAlbumFinderService
    {
        private IWebClient _client;
        
        public AlbumFinderClient(IWebClient webClient)
        {
            _client = webClient;
        }
        public async Task<AlbumResponse> GetAlbum(string id)
        {
            var result = new List<Album>();
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
                return trimmed != string.Empty ? new Uri(AppConstants.PhotosEndpoint + $"?albumId={trimmed}") : new Uri(AppConstants.PhotosEndpoint);
            }
            else
            {
                throw new ArgumentException(ErrorMessages.InvalidInputErrorMessage);
            }
        }
    }
}
