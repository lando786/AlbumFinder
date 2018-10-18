using AlbumFinder.Enums;
using AlbumFinder.Models;
using System.Collections.Generic;

namespace AlbumFinder
{
    public class AlbumResponse
    {
        public List<Album> Albums { get; set; }
        public ResponseCode Response { get; set; }
    }
}
