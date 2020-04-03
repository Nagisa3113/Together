using Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class RequestHandle
    {
        public RequestHandle() { }

        public string Move(string data, Client client, Server server)
        {
            server.BroadcastMessage(client, ActionCode.Move, data);
            return null;
        }


        public string Join(string data, Client client, Server server)
        {
            Console.WriteLine("第" + Server.clientNum + "位玩家加入");
            if (Server.clientNum == 1)
            {
                server.BroadcastMessage(ActionCode.Join,
                Enum.GetName(typeof(ReturnCode), ReturnCode.Fail));
            }
            else
            {
                server.BroadcastMessage(ActionCode.Join,
                Enum.GetName(typeof(ReturnCode), ReturnCode.Success));
            }
            return null;

        }

        public string Shoot(string data, Client client, Server server)
        {
            server.BroadcastMessage(client, ActionCode.Shoot, data);
            return null;
        }



    }
}