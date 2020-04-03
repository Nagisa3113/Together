using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class JoinRequest : BaseRequest
{
    public override void Awake()
    {
        actionCode = ActionCode.Join;
        base.Awake();
    }

    void Start()
    {
        base.SendRequest("Join");//可以省略
    }

    public override void OnResponse(string data)
    {
        Debug.Log(data);

        ReturnCode returnCode =
        (ReturnCode)Enum.Parse(typeof(ReturnCode), data);

        if (returnCode == ReturnCode.Fail)
        {
            Debug.Log("Current Player is Red");
            Debug.Log("已经连接第一名玩家");
            PlayerManager.Instance.p1.isLocalPlayer = false;
            PlayerManager.Instance.p2.isLocalPlayer = true;

        }
        else if (returnCode == ReturnCode.Success)
        {
            Debug.Log("Current Player is Blue");
            Debug.Log("已经连接第二名玩家");

            GameFacade.Instance.EnterPlayingSync();
        }
        else
        {
            Debug.LogError("没有得到Request Code");
        }
    }

}
