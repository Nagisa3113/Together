using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    Rigidbody2D rg;
    float speed = 6f;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int x = (int)collision.contacts[0].normal.x;

            rg.velocity = new Vector2(speed * x, 0);
        }
    }

}
