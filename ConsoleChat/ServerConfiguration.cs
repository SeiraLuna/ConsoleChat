using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ConsoleChat
{
    public class ServerConfiguration : IConfigurable
    {
        public IPAddress ServerIPAddress;
        public int ServerConnectionPort;
        //public TcpListener ServerListener;

        public ServerConfiguration()
        {
            this.ServerIPAddress = IPAddress.Parse("127.0.0.1");
            this.ServerConnectionPort = 80;
        }

        public dynamic Initialize()
        {
            return new TcpListener(this.ServerIPAddress, this.ServerConnectionPort);
        }

        public IConfigurable Load()
        {
            using (var streamreader = new StreamReader(new FileStream(@".\Configuration.txt", FileMode.Open,FileAccess.Read)))
            {
                var settings = new List<string> ();
                while (!streamreader.EndOfStream)
                {
                    settings.Add(streamreader.ReadLine());
                }
                this.ServerIPAddress = IPAddress.Parse(settings[0].Substring(settings[0].IndexOf(":") + 1));
                this.ServerConnectionPort = Convert.ToInt16(settings[1].Substring(settings[1].IndexOf(":") + 1));
            }
            Console.WriteLine($"Loaded Server Configuration: \r\nIP Address: {this.ServerIPAddress} \r\nPort: {this.ServerConnectionPort}");
            return this;
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.ServerIPAddress.ToString();
        }
    }
}