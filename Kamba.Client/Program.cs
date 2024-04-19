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
            tcpClient.Connect(IPAddress.Parse(host), int.Parse(port));
            Client client = new Client(tcpClient.Client);

            Console.ReadKey();
        }
    }
}
