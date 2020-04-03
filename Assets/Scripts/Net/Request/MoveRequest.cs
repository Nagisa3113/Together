using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class MoveRequest : BaseRequest
{
    Transform localPlayerTransform;
    int syncRate = 30;
    Transform remotePlayerTransform;
    bool isSyncRemotePlayer = false;
    Vector2 pos;
    //  Vector3 rotation;

    PlayerController localPlayer;
    PlayerController remotePlayer;


    bool m_FacingRight;
    bool anim_walk;

    public override void Awake()
    {
        actionCode = ActionCode.Move;
        base.Awake();
    }

    void Start()
    {
        InvokeRepeating("SyncLocalPlayer", 1f, 1f / syncRate);
    }
    void FixedUpdate()
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
        this.localPlayer = localPlayerTransform.GetComponent<PlayerController>();
        return this;
    }
    public MoveRequest SetRemotePlayer(Transform remotePlayerTransform)
    {
        this.remotePlayerTransform = remotePlayerTransform;
        this.remotePlayer = remotePlayerTransform.GetComponent<PlayerController>();
        return this;
    }


    void SyncLocalPlayer()
    {
        SendRequest(localPlayerTransform.position.x, localPlayerTransform.position.y,
        localPlayer.m_FacingRight, localPlayer.anim_walk);
        // localPlayerTransform.eulerAngles.x, localPlayerTransform.eulerAngles.y, localPlayerTransform.eulerAngles.z);
    }
    void SyncRemotePlayer()
    {
        remotePlayerTransform.position = pos;
        // remotePlayerTransform.eulerAngles = rotation;
        remotePlayer.m_FacingRight = this.m_FacingRight;
        remotePlayer.anim_walk = this.anim_walk;

    }

    void SendRequest(float x, float y, bool dir, bool anim_walk)
    {
        string data = string.Format("{0},{1}|{2}|{3}", x, y, dir, anim_walk);
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split('|');
        pos = Extension.ParseVector2(strs[0]);
        this.m_FacingRight = Boolean.Parse(strs[1]);
        this.anim_walk = Boolean.Parse(strs[2]);

        // rotation = Extension.ParseVector3(strs[1]);
        isSyncRemotePlayer = true;
    }

}
