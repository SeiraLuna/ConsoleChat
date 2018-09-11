using System.Net;
using System.Net.Sockets;

namespace ConsoleChat
{
    public class Client
    {
        private string ClientName;
        private string ServerIPAddress;
        private int ServerConnectionPort;
        private TcpClient ConnectionClient;

        public Client(IConfigurable clientConfiguration)
        {
            this.ConnectionClient = clientConfiguration.Initialize();
        }
    }
}