namespace Kamba.Server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Start(2323, "d:\\");
        }
    }
}
