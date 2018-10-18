using System.ComponentModel;

namespace AlbumFinder.Enums
{
    public enum ResponseCode
    {
        [Description("Ok")]
        Ok,
        [Description("Invalid input for Id, use numbers")]
        InvalidInput,
        [Description("Album id not found")]
        NotFound,
        [Description("Unknown error encountered")]
        Unknown
    }
   
}