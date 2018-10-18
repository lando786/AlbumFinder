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
        private bool _shouldRun = true;
        public App(IAlbumFinderService client)
        {
            _client = client;
        }
        public void Run()
        {

            while (_shouldRun)
            {
                Console.Write("Enter Album Id (Blank will be all) or 'exit' to quit:");
                var id = Console.ReadLine().ToLower();
                if (id == "exit")
                {
                    _shouldRun = false;
                    return;
                }
                var res = _client.GetAlbum(id);
                PrintResults(res);
                Console.WriteLine("*******");
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
