using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using System.Xml.Linq;
using UnityEngine;
using PurrNet;
public abstract class PlayerMovementBase : NetworkBehaviour
{
    protected PlayerInfo info;
    protected Rigidbody2D rb;
    protected int click_Down_Run_Check = 0;
    private int dir;

    protected bool isRuning = false;
    protected bool isWalking_Right = false;
    protected bool isWalking_Left = false;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    public bool IsGrounded;
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        enabled = isOwner;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        info = GetComponent<PlayerInfo>();
    }
   protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CheckGrounded();
        MoveWithKey();
        Move();
    }
    private void MoveWithKey()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnPointerDownRight();
            OnPointerDownCheckRun();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            OnPointerUpRight();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnPointerDownLeft();
            OnPointerDownCheckRun();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnPointerUpLeft();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnPointDownJump();
        }
    }
    protected virtual void Move()
    {
        if (isRuning)
        {
            rb.linearVelocity = new Vector2(info.RunSpeed * dir, rb.linearVelocity.y);
        }
        else if (!isRightAndLeftDown() && isRightOrLeftDown())
        {
            rb.linearVelocity = new Vector2(info.WalkSpeed * dir, rb.linearVelocity.y);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * dir, transform.localScale.y, transform.localScale.z);
        }
    }
    private void CheckGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(groundCheck.position, radius, groundLayer);
        IsGrounded = (hit != null);
    }
    public void OnPointDownJump()
    {
        if(IsGrounded)
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, info.JumpForce);
    }
    public void OnPointerDownCheckRun()
    {
        if(!isRightAndLeftDown())
        {
            click_Down_Run_Check++;
            if (click_Down_Run_Check == 2)
            {
                isRuning = true;
                CancelInvoke(nameof(OnEndTimeRun));
                click_Down_Run_Check = 0;
            }
            else
                Invoke(nameof(OnEndTimeRun), 0.2f);
        }    
    }
    public void OnPointerUpRight()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        isRuning = false;
        isWalking_Right = false;
    }
    public void OnPointerUpLeft()
    {
        rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
        isRuning = false;
        isWalking_Left = false;
    }

    public void OnPointerDownRight()
    {
        isWalking_Right = true;
        dir = 1;
    }
    public void OnPointerDownLeft()
    {
        isWalking_Left = true;
        dir = -1;
    }
    private void OnEndTimeRun()
    {
        click_Down_Run_Check = 0;
    }
    public bool isRightAndLeftDown() => isWalking_Left && isWalking_Right;
    public bool isRightOrLeftDown() => isWalking_Left || isWalking_Right;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, radius);
    }
   
}
