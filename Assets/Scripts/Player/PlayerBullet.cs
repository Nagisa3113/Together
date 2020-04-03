using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerBullet : MonoBehaviour
{
    public Vector3 line;
    float speed = 8f;
    Rigidbody2D rg;
    SpriteRenderer sr;
    CircleCollider2D col;

    Light2D light2D;

    Transform self, target;

    public bool isActive;

    void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        col = GetComponent<CircleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        self = this.transform.parent.transform;
        light2D = self.GetComponentInChildren<Light2D>();
    }

    void Start()
    {
        target = PlayerManager.Instance.GetAnotherPlayerTransform(self);
        sr.enabled = false;
        col.enabled = false;
        isActive = false;
    }

    public void Spawn()
    {
        this.transform.position = self.position;
        rg.velocity = (target.position - self.position).normalized * speed;

        sr.enabled = true;
        col.enabled = true;
        isActive = true;
    }

    void DeActive()
    {
        sr.enabled = false;
        col.enabled = false;
        rg.velocity = Vector2.zero;
        isActive = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!isActive) return;

        if (Vector2.Distance(this.transform.position, target.position) < 0.3f ||
            Mathf.Min(
                Vector2.Distance(this.transform.position, self.position),
                Vector2.Distance(this.transform.position, target.position))
            > light2D.pointLightOuterRadius)
        {
            DeActive();
        }

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Stone"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.transform.CompareTag("Apple"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        if (collision.transform.CompareTag("Insect"))
        {
            Destroy(collision.gameObject);
        }

        this.DeActive();
    }


}
