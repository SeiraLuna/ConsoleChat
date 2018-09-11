using System;
using System.IO;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ConsoleChat
{
    class Server
    {
        private TcpListener Host;
        private TcpClient Client;

        public Server(IConfigurable serverConfiguration)
        {
            this.Host = serverConfiguration.Initialize();
        }

        public Server(IConfigurable serverConfiguration, IConfigurable clientConfiguration)
        {
            this.Host = serverConfiguration.Initialize();
            this.Client = clientConfiguration.Initialize();
        }

        public void Initialize()
        {
            Host.Start();
            Console.WriteLine($"Host initilized at {Host.LocalEndpoint.ToString().Substring(0,Host.LocalEndpoint.ToString().IndexOf(":"))} on port " +
                              $"{Host.LocalEndpoint.ToString().Substring(Host.LocalEndpoint.ToString().IndexOf(":")+1)}. ");
            Client = default(TcpClient);
            while (true)
            {
                Listen();
                //ListenLocal();
            }

        }

        public void Read (string message)
        {
            Console.WriteLine(message);
        }

        public async void Listen()
        {
                Client = await Host.AcceptTcpClientAsync(); 
                using (var sr = new StreamReader(Client.GetStream())) // currently throws out of meomry exception. need to stop
                {                                                      //from reading if null.
                    var message = await sr.ReadLineAsync();
                    Read(message);
                }
        }

        public void ListenLocal()
        {
            var message = Console.ReadLine();
            Send(message);
        }

        public void Send(string message)
        {
            Console.WriteLine(message);
            /*
            using (var sw = new StreamWriter(Client.GetStream()))
            {
                sw.WriteLine(message);
            }*/
        }
    }
}