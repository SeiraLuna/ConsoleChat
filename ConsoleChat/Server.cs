using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text;

namespace ConsoleChat
{
    partial class Server
    {
        private ServerConfiguration _config;
        private Socket _socket;
        private byte[] _buffer;
        private List<Socket> _clientList;

        public Server(IConfigurable serverConfiguration)
        {
            this._config = (ServerConfiguration)serverConfiguration;
            this._socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this._clientList = new List<Socket>();
            this._buffer = new byte[_config.BufferSize()];
        }

        public void Initialize()
        {
            _socket.Bind(_config.IPEndPoint());
            _socket.Listen(100);
            Console.WriteLine($"Host initilized at {_socket.LocalEndPoint.ToString().Substring(0, _socket.LocalEndPoint.ToString().IndexOf(":"))} on port " +
                              $"{_socket.LocalEndPoint.ToString().Substring(_socket.LocalEndPoint.ToString().IndexOf(":") + 1)}. ");
            _socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Console.WriteLine("Awaiting connection...");
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            var client = _socket.EndAccept(asyncResult);
            Console.WriteLine("Client connected.");
            _clientList.Add(client);
            Console.WriteLine($"{_clientList[0].ToString()} added to connections.");
            _buffer = new byte[_config.BufferSize()];
            client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceivedCallback), client);
            _socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
            Console.WriteLine("Awaiting data...");
        }

        private void ReceivedCallback(IAsyncResult asyncResult)
        {            
            var client = (Socket)asyncResult.AsyncState;
            var received = new byte[client.EndReceive(asyncResult)];
            Array.Copy(_buffer, received, received.Length);
            Console.WriteLine(received.Length);

            Read(Encoding.ASCII.GetString(received));

            _buffer = new byte[_config.BufferSize()];
            client.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, new AsyncCallback(ReceivedCallback), client);
        }

        public void Read (string message)
        {
            Console.WriteLine(message);
        }

        //public async void Listen()
        //{
        //        Client = await Host.AcceptTcpClientAsync(); 
        //        using (var sr = new StreamReader(Client.GetStream())) // currently throws out of meomry exception. need to stop
        //        {                                                      //from reading if null.
        //            var message = await sr.ReadLineAsync();
        //            Read(message);
        //    }

        //}

        //public void ListenLocal()
        //{
        //    var message = Console.ReadLine();
        //    Send(message);
        //}

        //public void Send(string message)
        //{
        //    Console.WriteLine(message);
            
            /*using (var sw = new StreamWriter(Client.GetStream()))
            {
                sw.WriteLine(message);
            }*/
       // }
    }
}