using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleChat
{
    class Program
    {
        static void Main(string[] args)
        {
            var Server = new Server(new ServerConfiguration().Load());
            Server.Initialize();

            Console.ReadKey();
        }
    }
}
