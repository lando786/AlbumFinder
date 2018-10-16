using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AlbumFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new AlbumFinderClient();

            while (true)
            {
                Console.Write("Enter Album Id (Blank will be all) or 'exit' to quit:");
                var id = Console.ReadLine();
                if (id == "exit")
                {
                    break;
                }
                var res = client.GetAlbum(id);
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
