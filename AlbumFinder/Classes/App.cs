using AlbumFinder.Enums;
using AlbumFinder.Extensions;
using AlbumFinder.Services;
using System;
using System.Threading.Tasks;

namespace AlbumFinder.Classes
{
    public class App
    {
        private IAlbumFinderService _client;
        public App(IAlbumFinderService client)
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
