using Kamba.Common;
namespace Kamba.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Connect("127.0.0.1", 2323);
            var file = new FileReadRequest(8888,9999, "\\FileSyncServer.exe_240416_080857.dmp", 0,512);

            var packets=file.ToPackets();
            foreach(var packet in packets)
            {
                client.Client.Send(packet.Serialize());
            }
            Console.ReadKey();
        }
    }
}
