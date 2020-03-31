using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionBlock : MonoBehaviour
{
    Transform block;

    private void Awake()
    {
        block = transform.GetChild(0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //if (Input.GetKey(KeyCode.Space))
        //{
        block.transform.Rotate(0, 0, 2f * Time.deltaTime);
        //}
    }

}
