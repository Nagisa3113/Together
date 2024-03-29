﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [ExecuteInEditMode]
public class MovableObject : MonoBehaviour
{
    public Vector3 tf1;
    public Vector3 tf2;

    Vector3 t1;
    Vector3 t2;

    //[SerializeField]
    float speed = 1f;

    float i = 0;
    int t = 1;

    // Start is called before the first frame update
    void Start()
    {
        t1 = this.transform.position + tf1;
        t2 = this.transform.position + tf2;
        if (GameFacade.Instance != null)
        {
            GameFacade.Instance.movableObjects.Add(this);
        }

        StartCoroutine("Move");
    }

    void Update()
    {
        Debug.DrawLine(this.transform.position + tf1, this.transform.position + tf2);
    }


    public void Reset()
    {
        StopAllCoroutines();
        i = 0;
        StartCoroutine(Move());
    }


    public IEnumerator Move()
    {
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        Animator animator = null;
        try
        {
            animator = GetComponent<Animator>();
        }
        catch (UnityException e)
        {
            Debug.Log(e);
        }

        while (true)
        {
            t = i > 1 ? -1 : i < 0 ? 1 : t;
            if (animator != null)
            {
                sprite.flipX = t > 0;
            }

            i += t * Time.deltaTime * speed / Vector3.Distance(t1, t2);
            this.transform.position = Vector3.Lerp(t1, t2, Mathf.Clamp01(i));
            yield return 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            t = -t;
        }
    }
}