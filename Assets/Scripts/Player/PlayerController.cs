using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{

    #region param
    public event Action<RaycastHit2D> onControllerCollidedEvent;
    public event Action<Collider2D> onTriggerEnterEvent;
    public event Action<Collider2D> onTriggerExitEvent;

    public bool isLocalPlayer = false;

    [Header("跳跃速度")]
    [SerializeField] float m_JumpVelcoity = 15f;
    [Header("移动速度")]
    [SerializeField] float m_MoveSpeed = 10f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D m_Rigidbody2D;

    Animator animator;

    bool m_Grounded;

    bool m_FacingRight = true;
    //TODO
    public float gravity;

    PlayerLight playerLight;
    PlayerBullet playerBullet;

    #endregion

    void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        playerLight = GetComponentInChildren<PlayerLight>();
        playerBullet = GetComponentInChildren<PlayerBullet>();
        gravity = m_Rigidbody2D.gravityScale;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        m_Rigidbody2D.gravityScale = gravity;
    }

    void FixedUpdate()
    {
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


    public void RemoteShoot()
    {
        playerBullet.Spawn();
        animator.SetTrigger("shoot");
    }

    void Shoot(bool shoot)
    {
        if (shoot && playerBullet.isActive == false)
        {
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
            m_Grounded = false;
            m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_JumpVelcoity);
        }
    }

    void Move(float move)
    {
        animator.SetBool("walk",Mathf.Abs(move)>0);
        m_Rigidbody2D.velocity = new Vector2(move * m_MoveSpeed, m_Rigidbody2D.velocity.y);

        if (move > 0 && !m_FacingRight || move < 0 && m_FacingRight)
        {
            m_FacingRight = !m_FacingRight;
            Vector3 sc=this.transform.localScale;
            sc.x*=-1;
            this.transform.localScale=sc;
            //spriteRenderer.flipX = !spriteRenderer.flipX;
        }

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

        if (Vector2.Angle(collision.contacts[0].normal, Vector2.up) < 45)
        {
            m_Rigidbody2D.gravityScale = 0;
        }
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var c in collision.contacts)
        {
            if ((int)c.normal.y == 1)
            {
                m_Grounded = true;
            }
        }

        if (collision.gameObject.CompareTag("Pedal"))
        {
            if (m_Grounded && (int)collision.contacts[0].normal.y == 1)
                this.transform.parent = collision.transform;
        }

        if (Vector2.Angle(collision.contacts[0].normal, Vector2.up) < 45)
        {
            m_Rigidbody2D.gravityScale = 0;
            m_Grounded = true;

        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        m_Grounded = false;
        if (collision.gameObject.CompareTag("Pedal"))
        {
            this.transform.parent = null;
        }
        m_Rigidbody2D.gravityScale = gravity;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnterEvent?.Invoke(collision);
        collision.GetComponent<IInteractive>()?.Interactive();
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (onTriggerExitEvent != null)
            onTriggerExitEvent(col);
    }

    #endregion

}
