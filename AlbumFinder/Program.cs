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
            Console.Write("Enter Album Id (Blank will be all):");
            var id = Console.ReadLine();
            var res = client.GetAlbum(id);
            PrintResults(res);
            
            Console.Write("Done");
            Console.ReadLine();
        }

        private static void PrintResults(Task<IEnumerable<Album>> res)
        {
            if (res.Result.Any())
            {
                foreach (var album in res.Result)
                {
                    Console.WriteLine($"[{ album.id}] {album.title} ");
                }
            }
            else
            {
                Console.WriteLine("Album Id not found");
            }
        }
    }
}
