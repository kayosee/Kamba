using System.Configuration;

namespace Kamba.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var port = ConfigurationManager.AppSettings["port"];
            var folder = ConfigurationManager.AppSettings["folder"];
            Server server = new Server();
            server.Start(int.Parse(port), folder);
        }
    }
}
