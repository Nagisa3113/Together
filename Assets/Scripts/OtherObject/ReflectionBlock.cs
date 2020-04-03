using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionBlock : MonoBehaviour
{
    Transform block;

    void Awake()
    {
        block = transform.GetChild(0);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            block.transform.Rotate(0, 0, 2f * Time.deltaTime);
        }
    }

}
