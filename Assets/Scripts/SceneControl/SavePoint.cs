using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class SavePoint : MonoBehaviour, IInteractive
{
    public int index;

    public Vector3 t1;
    public Vector3 t2;

    public void Interactive()
    {
        // var gm = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        // if (this.index > gm.lastSave.index)
        // {
        //     gm.lastSave = this;
        // }
    }

    void Update()
    {
        Debug.DrawLine(this.transform.position + t1, this.transform.position + t2);
    }



}
