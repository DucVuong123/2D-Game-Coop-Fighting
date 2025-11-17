using UnityEngine;
using PurrNet;
public class PlayerLadderMovement : NetworkBehaviour
{
    [Header("Overlap")]
    public Vector2 boxSize = new Vector2(0.5f, 1f); 
    public Transform pos_Check_Ladder; 
    public LayerMask whatIsLadder;
    [Header("Other")]
    public float clamp_Speed = 8;
    private Rigidbody2D rb;
    private float old_Gravity;
   [SerializeField] private bool isClimbingUp;
    [SerializeField] private bool isClimbingDown;
    private bool isHitLadder;
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        enabled = isOwner;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        old_Gravity = rb.gravityScale;
    }

    void FixedUpdate()
    {
        ClampWithKey();
        Collider2D hitInfo = Physics2D.OverlapBox(pos_Check_Ladder.position, boxSize, 0, whatIsLadder);

        if (hitInfo != null)
        {
            isHitLadder = true;
        }
        else
        {
            isHitLadder = false;
        }

        if (isHitLadder)
        {
            if(isClimbingUp || isClimbingDown)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, isClimbingUp? clamp_Speed : -clamp_Speed);
            else if((isClimbingUp && isClimbingDown) || (!isClimbingUp && !isClimbingDown))
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.gravityScale = 0;
        }
        else if(!isHitLadder)
        {
            rb.gravityScale = old_Gravity;
        }
    }
    private void ClampWithKey()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            OnPointerDownClampUp();
        }
        if (Input.GetKeyUp(KeyCode.U))
        {
            OnPointerUpClampUp();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            OnPointerDownClampDown();
        }
        if (Input.GetKeyUp(KeyCode.I))
        {
            OnPointerUpClampDown();
        }
    }
    public void OnPointerDownClampUp()
    {
        if (isHitLadder)
        {
            isClimbingUp = true;
        }
    }
    public void OnPointerUpClampUp()
    {
        isClimbingUp = false;
    }
    public void OnPointerDownClampDown()
    {
        if (isHitLadder)
        {
            isClimbingDown = true;
        }
    }
    public void OnPointerUpClampDown()
    {
        isClimbingDown = false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(pos_Check_Ladder.position, boxSize);
    }
    //[Header("Ladder check")]
    //public Vector2 boxSize = new Vector2(0.5f, 0.1f); 
    //public float boxOffsetY = -0.5f; 
    //public LayerMask LadderLayer; 
    //public bool isHitLadder=false;
    //[Header("Other")]
    //public float clamp_Speed = 8;
    //public bool isClaming=false;
    //private Rigidbody2D rb;
    //// Start is called once before the first execution of Update after the MonoBehaviour is created
    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody2D>();
    //}
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    CheckHitLadder();
    //}
    //public void OnPointerDownClamp()
    //{
    //    if(isHitLadder)
    //    {
    //        isClaming=true;
    //    }    
    //}
    //public void OnPointerUpClamp()
    //{
    //        isClaming = false;
    //}
    //void CheckHitLadder()
    //{
    //    Vector2 boxCenter = (Vector2)transform.position + Vector2.up * boxOffsetY;
    //    isHitLadder = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LadderLayer);
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Vector2 boxCenter = (Vector2)transform.position + Vector2.up * boxOffsetY;
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireCube(boxCenter, boxSize);
    //}
}
