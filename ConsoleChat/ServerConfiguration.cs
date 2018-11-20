using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ConsoleChat
{
    public class ServerConfiguration : IConfigurable
    {
        private IPAddress _iPAddress;
        private int _port;
        private IPEndPoint _iPEndPoint;
        private int _bufferSize;
        //public TcpListener ServerListener;

        public ServerConfiguration()
        {
            this._iPAddress = IPAddress.Parse("127.0.0.1");
            this._port = 8008;
            this._iPEndPoint = new IPEndPoint(_iPAddress, _port);
            this._bufferSize = 1024;
        }

        public ServerConfiguration(string ipAddress, int port)
        {
            this._iPAddress = IPAddress.Parse(ipAddress);
            this._port = port;
        }

        public IPEndPoint IPEndPoint()
        {
            return _iPEndPoint;
        }

        public int BufferSize()
        {
            return _bufferSize;
        }

        public dynamic Initialize()
        {
            return this;
            //return new TcpListener(this.ServerIPAddress, this.ServerConnectionPort);
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
                this._iPAddress = IPAddress.Parse(settings[0].Substring(settings[0].IndexOf(":") + 1));
                this._port = Convert.ToInt16(settings[1].Substring(settings[1].IndexOf(":") + 1));
            }
            Console.WriteLine($"Loaded Server Configuration: \r\nIP Address: {this._iPAddress} \r\nPort: {this._port}");
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
            return this._iPAddress.ToString();
        }
    }
}