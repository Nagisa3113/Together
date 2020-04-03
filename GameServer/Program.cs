﻿using System;

namespace GameServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 6688);
            Console.WriteLine("等待客户端连接");
            server.Start();

            Console.ReadKey();
        }
    }
}
