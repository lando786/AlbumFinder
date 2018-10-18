using AlbumFinder.Classes;
using AlbumFinder.Services;
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
            container.RegisterType<IAlbumFinderService, AlbumFinderClient>();
            container.RegisterType<IWebClient, WebClient>();
            return container;
        }

       
    }
}
