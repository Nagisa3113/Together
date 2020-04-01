using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    float rotateSpeed = 50f;
    float angle = 15f;

    public Vector3 v;
    float t = 1;

    // Update is called once per frame
    void Update()
    {

        if (v.z > angle)
        {
            t = -1;
        }
        else if (v.z < -angle)
        {
            t = 1;
        }
        v.z += rotateSpeed * t * Time.deltaTime;
        transform.localEulerAngles = v;
    }


}
