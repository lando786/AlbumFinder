using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace AlbumFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var unity = CreateUnityContainerAndRegisterComponents();
            var app = unity.Resolve<App>();
            app.Run();
        }

        private static UnityContainer CreateUnityContainerAndRegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IAlbumFinderClient, AlbumFinderClient>();
            container.RegisterType<IWebClient, WebClient>();
            return container;
        }

       
    }
    public class App
    {
        private IAlbumFinderClient _client;
        public App(IAlbumFinderClient client)
        {
            _client = client;
        }
        public void Run()
        {

            while (true)
            {
                Console.Write("Enter Album Id (Blank will be all) or 'exit' to quit:");
                var id = Console.ReadLine();
                if (id == "exit")
                {
                    break;
                }
                var res = _client.GetAlbum(id);
                PrintResults(res);
                Console.Write("*******");
            }
        }
        private static void PrintResults(Task<AlbumResponse> res)
        {

            if (res.Result.Response == ResponseCode.Ok)
            {
                foreach (var album in res.Result.Albums)
                {
                    Console.WriteLine($"[{ album.Id}] {album.Title} ");
                }
            }
            else
            {
                Console.WriteLine(res.Result.Response.GetDescription());
            }

        }
    }
}
