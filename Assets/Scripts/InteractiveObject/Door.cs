using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Door : MonoBehaviour
{
    public RayColor doorColor;

    public Light2D[] light2Ds;

    void Update()
    {
        if (doorColor == RayColor.NONE)
        {
            int i = 0;
            foreach (Light2D l in light2Ds)
            {
                if (l.enabled == true)
                {
                    i++;
                }
            }

            if (i == light2Ds.Length)
            {
                this.gameObject.SetActive(false);
            }
        }

    }

    public void RayDoor(RayColor rayColor)
    {
        if (rayColor == doorColor)
        {
            Destroy(this.gameObject);
        }
    }


}
