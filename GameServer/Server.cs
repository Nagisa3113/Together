using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Reflection;
using Common;

namespace GameServer
{
    class Server
    {
        public static int clientNum = 0;

        private IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList = new List<Client>();

        private RequestHandle requestHandle;

        public Server() { }
        public Server(string ipStr, int port)
        {
            //controllerManager = new ControllerManager(this);
            SetIpAndPort(ipStr, port);
            requestHandle = new RequestHandle();
        }

        public void SetIpAndPort(string ipStr, int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallBack, null);
        }
        private void AcceptCallBack(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            client.Start();
            clientList.Add(client);
            clientNum++;
            // Console.WriteLine(client.clientSocket != null);
            // Console.WriteLine(client.clientSocket.Connected);

            Console.WriteLine("Connected Client Num = " + clientNum);
            if (clientNum <= 2)
                serverSocket.BeginAccept(AcceptCallBack, null);
        }
        public void RemoveClient(Client client)
        {
            lock (clientList)
            {
                clientList.Remove(client);
            }
        }
        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            Console.WriteLine("Send Response To " + client.GetHashCode() + " Data=  " + data);
            client.Send(actionCode, data);
        }

        public void BroadcastMessage(Client excludeClient, ActionCode actionCode, string data)
        {
            foreach (Client client in clientList)
            {
                if (client != excludeClient)
                {
                    this.SendResponse(client, actionCode, data);
                }
            }
        }

        public void BroadcastMessage(ActionCode actionCode, string data)
        {
            foreach (Client client in clientList)
            {
                this.SendResponse(client, actionCode, data);
            }
        }



        /// <summary>
        /// 处理客户端发来的请求
        /// </summary>
        /// <param name="requestCode">用来指定用那个Handle处理，这里不区别</param>
        /// <param name="actionCode">动作指令</param>
        /// <param name="data">参数</param>
        /// <param name="client"></param>
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            //BaseController controller;
            //bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            //if (isGet == false)
            //{
            //    Console.WriteLine("无法得到[" + requestCode + "]所对应的Controller,无法处理请求"); return;
            //}
            string methodName = Enum.GetName(typeof(ActionCode), actionCode);
            MethodInfo mi = requestHandle.GetType().GetMethod(methodName);
            if (mi == null)
            {
                Console.WriteLine("[警告]在RequestHandle中没有对应的处理方法:[" + methodName + "]"); return;
            }
            //Console.WriteLine("mi name = " + mi.ToString());
            object[] parameters = new object[] { data, client, this };
            object o = mi.Invoke(requestHandle, parameters);
            if (o == null || string.IsNullOrEmpty(o as string))
            {
                return;
            }
            this.SendResponse(client, actionCode, o as string);
        }
    }

}