using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class AnmiRequest : BaseRequest
{

    bool anmTag;

    public override void Awake()
    {
        actionCode = ActionCode.Join;
        base.Awake();
    }
    //this.remotePlayerAnim = remotePlayerTransform.GetComponent<Animator>();

    // if (move != 0)
    // {
    //     animator.SetBool("walk", true);
    // }
    // else if (!(moveLeft || moveRight))
    // {
    //     animator.SetBool("walk", false);
    // }
    // if (m_Grounded)
    // {
    //     animator.SetBool("jump", false);
    // }
    // else
    // {
    //     animator.SetBool("jump", true);
    // }

    public override void OnResponse(string data)
    {
        // remotePlayerAnim.SetFloat("Forward", forward);
    }

}
