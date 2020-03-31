using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    Vector3 tf;
    Rigidbody2D rg;
    SpriteRenderer sr;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        tf = this.transform.position;

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ReSet(Random.Range(0.8f, 2.0f)));
    }

    IEnumerator ReSet(float s)
    {
        sr.enabled = false;
        this.transform.position = tf;
        rg.gravityScale = 0;
        rg.velocity = Vector2.zero;
        yield return new WaitForSeconds(s);
        sr.enabled = true;
        rg.gravityScale = 1;

    }

}
