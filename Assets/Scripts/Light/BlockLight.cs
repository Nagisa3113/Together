using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;


public class BlockLight : MonoBehaviour, ILight2D, IInteractive
{
    public Transform rotatPivot;
    Light2D light2D;

    void Awake()
    {
        light2D = GetComponentInChildren<Light2D>();
        light2D.enabled = false;
    }

    public void Interactive()
    {
        if (light2D.enabled == false) Lighten();
        else Flip();
    }

    void Flip()
    {
        float x = rotatPivot.transform.position.x - light2D.gameObject.transform.position.x;
        light2D.gameObject.transform.position += new Vector3(2 * x, 0, 0);
        light2D.transform.localScale = new Vector3(-light2D.transform.localScale.x,
         light2D.transform.localScale.y, light2D.transform.localScale.z);
    }

    public void Lighten()
    {
        light2D.enabled = true;
    }

    public void UnLighten()
    {
        light2D.enabled = false;
    }


}
