using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System;
using Common;

public class ClientManager : BaseManager
{
    const string IP = "127.0.0.1";
    const int PORT = 6688;

    Socket clientSocket;
    Message msg = new Message();

    public ClientManager(GameFacade facade) : base(facade)
    {
        ConnectToServer();
    }

    public void ConnectToServer()
    {
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, PORT);
            Start();
            GameFacade.Instance.IsConnected = true;
            Debug.Log("成功连接至服务器");
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法连接到服务器端，请检查您的网络！！" + e);
        }
    }


    void Start()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
    }
    void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMessage(count, OnProcessDataCallback);

            Start();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
    void OnProcessDataCallback(ActionCode actionCode, string data)
    {
        facade.HandleReponse(actionCode, data);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        Debug.Log(bytes);
        clientSocket.Send(bytes);

    }
    public override void OnDestroy()
    {
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            Debug.LogWarning("无法关闭跟服务器端的连接！！" + e);
        }
    }
}
