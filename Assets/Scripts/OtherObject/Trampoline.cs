using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class Trampoline : MonoBehaviour
{
    public Transform target;
    public float timerCounter = 6f;
    public Vector3 tf1;
    public Vector3 tf2;

    Vector3 t1;
    Vector3 t2;
    float i = 0;
    int t = 1;
    [SerializeField] float speed = 1f;

    private void Start()
    {
        t1 = this.transform.position + tf1;
        t2 = this.transform.position + tf2;
        StartCoroutine(Move());
    }

    void Update()
    {
        Debug.DrawLine(this.transform.position + tf1, this.transform.position + tf2);
    }


    public IEnumerator Move()
    {
        while (true)
        {
            t = i > 1 ? -1 : i < 0 ? 1 : t;

            i += t * Time.deltaTime * speed / Vector3.Distance(t1, t2);
            // this.transform.position = V3Lerp(t1, t2, Mathf.Clamp01(i));
            this.transform.position = Vector3.Lerp(t1, t2, Mathf.Clamp01(i));
            yield return 0;
        }
    }
}