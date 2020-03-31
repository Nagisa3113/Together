using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class MoveRequest:BaseRequest
{
    private Transform localPlayerTransform;
    private int syncRate = 30;
    private Transform remotePlayerTransform;
    private bool isSyncRemotePlayer = false;
    private Vector3 pos;
    private Vector3 rotation;
    private float forward;

    public override void Awake()
    {
        actionCode = ActionCode.Move;
        base.Awake();
    }

    private void Start()
    {
        InvokeRepeating("SyncLocalPlayer", 1f, 1f / syncRate);
    }
    private void FixedUpdate()
    {
        if (isSyncRemotePlayer)
        {
            SyncRemotePlayer();
            isSyncRemotePlayer = false;
        }
    }
    public MoveRequest SetLocalPlayer(Transform localPlayerTransform)
    {
        this.localPlayerTransform = localPlayerTransform;
        return this;
    }
    public MoveRequest SetRemotePlayer(Transform remotePlayerTransform)
    {
        this.remotePlayerTransform = remotePlayerTransform;
        return this;
    }
    private void SyncLocalPlayer()
    {
        SendRequest(localPlayerTransform.position.x, localPlayerTransform.position.y, localPlayerTransform.position.z,
            localPlayerTransform.eulerAngles.x, localPlayerTransform.eulerAngles.y, localPlayerTransform.eulerAngles.z);
    }
    private void SyncRemotePlayer()
    {
        remotePlayerTransform.position = pos;
        remotePlayerTransform.eulerAngles = rotation;
       // remotePlayerAnim.SetFloat("Forward", forward);
    }

    private void SendRequest(float x, float y, float z, float rotationX, float rotationY, float rotationZ)
    {
        string data = string.Format("{0},{1},{2}|{3},{4},{5}", x, y, z, rotationX, rotationY, rotationZ);
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {   
        string[] strs = data.Split('|');
        pos = Extension.ParseVector3(strs[0]);
        rotation = Extension.ParseVector3(strs[1]);
        isSyncRemotePlayer = true;
    }

}
