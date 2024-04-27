using Kamba.Common;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
namespace Kamba.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = ConfigurationManager.AppSettings["host"];
            var port = ConfigurationManager.AppSettings["port"];
            var tcpClient = new TcpClient();

            Console.ReadKey();
        }
    }
}
