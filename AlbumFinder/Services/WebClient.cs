using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AlbumFinder.Services
{
    public interface IWebClient
    {
        Task<HttpResponseMessage> GetAsync(Uri uri);
    }
    public class WebClient : IWebClient
    {
        private HttpClient client = new HttpClient();
        public Task<HttpResponseMessage> GetAsync(Uri uri)
        {
            return client.GetAsync(uri);
        }
    }
}
