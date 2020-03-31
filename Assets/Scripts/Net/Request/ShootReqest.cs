using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class ShootReqest : BaseRequest
{

    private Transform remotePlayerTransform;

    private bool isSyncRemotePlayer = false;

    public override void Awake()
    {
        actionCode = ActionCode.Shoot;
        base.Awake();
    }

    private void FixedUpdate()
    {
        if (isSyncRemotePlayer)
        {
            SyncRemotePlayer();
            isSyncRemotePlayer = false;
        }
    }

    public ShootReqest SetRemotePlayer(Transform remotePlayerTransform)
    {
        this.remotePlayerTransform = remotePlayerTransform;
        return this;
    }

    private void SyncRemotePlayer()
    {
        remotePlayerTransform.GetComponent<PlayerController>().RemoteShoot();
    }

    public override void SendRequest()
    {
        base.SendRequest("shoot");//可以省略
    }
    public override void OnResponse(string data)
    {
        isSyncRemotePlayer = true;
    }
}
