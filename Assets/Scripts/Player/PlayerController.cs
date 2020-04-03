using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    #region param
    //public event Action<RaycastHit2D> onControllerCollidedEvent;
    public event Action<Collider2D> onTriggerEnterEvent;
    public event Action<Collider2D> onTriggerExitEvent;

    public bool isLocalPlayer = false;

    [Header("跳跃速度")]
    // [SerializeField] 
    float m_JumpVelcoity = 13f;
    [Header("移动速度")]
    // [SerializeField] 
    float m_MoveSpeed = 5f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D m_Rigidbody2D;
    Animator animator;
    BoxCollider2D boxCollider2D;
    PlayerLight playerLight;
    PlayerBullet playerBullet;


    bool m_Grounded;
    public bool m_FacingRight = true;
    public float gravity;

    //Animator
    public bool anim_walk;

    //CheckGround
    Vector3 offset = new Vector3(0, -0.7f, 0);
    float circle = 0.24f;
    public LayerMask layerMask;

    #endregion

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerLight = GetComponentInChildren<PlayerLight>();
        playerBullet = GetComponentInChildren<PlayerBullet>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        gravity = m_Rigidbody2D.gravityScale;

        DontDestroyOnLoad(this.gameObject);

        onTriggerEnterEvent += (collider2D) =>
        {
            collider2D.GetComponent<IInteractive>()?.Interactive();
        };

    }

    void Update()
    {
        animator.SetBool("jump", !m_Grounded);
        animator.SetBool("walk", anim_walk);

        Vector3 sc = this.transform.localScale;
        sc.x = m_FacingRight ? Mathf.Abs(sc.x) : -Mathf.Abs(sc.x);
        this.transform.localScale = sc;
    }


    void FixedUpdate()
    {

        CheckGround();

        if (isLocalPlayer)
        {
            Shoot(InputHandler.Instance.Shoot);
            Move(InputHandler.Instance.HorizontalAxis.Value);
            Jump(InputHandler.Instance.Jump);
        }
        else if (!GameFacade.Instance.IsConnected)
        {
            Shoot(_InputHandler.Instance.Shoot);
            Move(_InputHandler.Instance.HorizontalAxis.Value);
            Jump(_InputHandler.Instance.Jump);
        }
    }


    void CheckGround()
    {
        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(this.transform.position + offset, circle, layerMask);

        m_Grounded = false;
        m_Rigidbody2D.gravityScale = gravity;
        foreach (Collider2D c in collider2Ds)
        {
            if (c != this.boxCollider2D)
            {
                m_Grounded = true;
                m_Rigidbody2D.gravityScale = 0;
            }
        }

        Debug.DrawLine(this.transform.position + offset,
        this.transform.position + offset + new Vector3(0, circle, 0));
    }



    void Shoot(bool shoot)
    {
        if (shoot && playerBullet.isActive == false)
        {
            InputHandler.Instance.RefreshShootBuffer();
            playerBullet.Spawn();
            animator.SetTrigger("shoot");
            if (GameFacade.Instance.IsStartGame)
            {
                GameFacade.Instance.gameObject.GetComponent<ShootReqest>().SendRequest();
            }
        }
    }

    void Jump(bool jump)
    {
        if (m_Grounded && jump)
        {
            InputHandler.Instance.RefreshJumpBuffer();
            m_Grounded = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpVelcoity);
        }
    }

    void Move(float move)
    {
        anim_walk = move != 0;
        m_Rigidbody2D.velocity = new Vector2(move * m_MoveSpeed, m_Rigidbody2D.velocity.y);

        if (move > 0) m_FacingRight = true;
        else if (move < 0) m_FacingRight = false;
    }




    public void RemoteShoot()
    {
        playerBullet.Spawn();
        animator.SetTrigger("shoot");
    }



    #region MonoBehavior

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Trampoline") && Mathf.Approximately(collision.contacts[0].normal.y, 1))
        {
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpVelcoity * 1.5f);
        }

        if (collision.collider.CompareTag("Death") || collision.collider.CompareTag("Insect"))
        {
            //TODO 
            //PlayerManager.Instance.GameOver();
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pedal"))
        {
            if (m_Grounded && (int)collision.contacts[0].normal.y == 1)
                this.transform.parent = collision.transform;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Pedal"))
        {
            this.transform.parent = null;
        }
    }


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        onTriggerEnterEvent?.Invoke(collider2D);
    }

    public void OnTriggerExit2D(Collider2D collider2D)
    {
        onTriggerExitEvent?.Invoke(collider2D);
    }

    #endregion

}
