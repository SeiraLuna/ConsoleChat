using System.Net;
using System.Net.Sockets;

namespace ConsoleChat
{
    public class ClientConfiguartion :IConfigurable
    {
        public string ServerIPAddress;
        public int ServerConnectionPort;

        public void ClientConfiguration()
        {
            this.ServerIPAddress = "127.0.0.1";
            this.ServerConnectionPort = 80;
        }

        public dynamic Initialize()
        {
            return new TcpClient("127.0.0.1", ServerConnectionPort);
        }

        public IConfigurable Load()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}