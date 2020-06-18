using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class SavePoint : MonoBehaviour, IInteractive
{
    public int index;

    public Vector3 t1;
    public Vector3 t2;

    public Vector3 P1Pos
    {
        get
        {
            return this.transform.position + t1;
        }
    }
    public Vector3 P2Pos
    {
        get
        {
            return this.transform.position + t2;
        }
    }

    private void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void Interactive()
    {
        if (this.index > GameFacade.Instance.lastSave.index)
        {
            GameFacade.Instance.lastSave = this;
        }
    }

    void Update()
    {
        Debug.DrawLine(this.transform.position + t1, this.transform.position + t2);
    }



}
