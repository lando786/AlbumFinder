using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumFinder
{
    public class AlbumResponse
    {
        public List<Album> Albums { get; set; }
        public ResponseCode Response { get; set; }
    }
}
