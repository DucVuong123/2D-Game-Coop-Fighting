using UnityEngine;

public class PlayerLadderMovement : MonoBehaviour
{
    [Header("Ladder check")]
    public Vector2 boxSize = new Vector2(0.5f, 0.1f); 
    public float boxOffsetY = -0.5f; 
    public LayerMask LadderLayer; 
    public bool isHitLadder=false;
    [Header("Other")]
    public float clamp_Speed = 8;
    public bool isClaming=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckHitLadder();
    }
    public void OnPointerDownClamp()
    {
        if(isHitLadder)
        {
            isClaming=true;
        }    
    }    
    void CheckHitLadder()
    {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.up * boxOffsetY;
        isHitLadder = Physics2D.OverlapBox(boxCenter, boxSize, 0f, LadderLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 boxCenter = (Vector2)transform.position + Vector2.up * boxOffsetY;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(boxCenter, boxSize);
    }
}
