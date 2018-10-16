using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AlbumFinder
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
