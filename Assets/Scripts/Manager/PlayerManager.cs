using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class PlayerManager : BaseManager
{
    public PlayerManager(GameFacade facade) : base(facade)
    {
        Instance = this;
        p1 = GameObject.Find("Player1").GetComponent<PlayerController>();
        p2 = GameObject.Find("Player2").GetComponent<PlayerController>();

        p1.isLocalPlayer = true;
        p2.isLocalPlayer = false;
        if (facade.IsConnected)
        {
            SendJoinRequest();
        }
    }

    GameObject currentRoleGameObject, remoteRoleGameObject;

    public static PlayerManager Instance;

    public PlayerController p1, p2;


    public Transform GetAnotherPlayerTransform(Transform self)
    {
        if (p1.transform == self) return p2.transform;
        else return p1.transform;
    }


    public void SendJoinRequest()
    {
        facade.gameObject.AddComponent<JoinRequest>();
    }

    public void GetLocalPlayer()
    {
        currentRoleGameObject = p1.isLocalPlayer ? p1.gameObject : p2.gameObject;
        remoteRoleGameObject = p1.isLocalPlayer ? p2.gameObject : p1.gameObject;
        remoteRoleGameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        remoteRoleGameObject.GetComponent<PlayerController>().gravity = 0;
    }

    public void AddRequestScript()
    {
        // playerSyncRequest = new GameObject("PlayerSyncRequest");
        facade.gameObject.AddComponent<MoveRequest>().SetLocalPlayer(currentRoleGameObject.transform)
            .SetRemotePlayer(remoteRoleGameObject.transform);

        facade.gameObject.AddComponent<ShootReqest>().SetRemotePlayer(remoteRoleGameObject.transform);
    }

    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }


}
