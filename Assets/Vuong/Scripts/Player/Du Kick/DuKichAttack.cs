using PurrNet;
using UnityEngine;
public class DuKichAttack : PlayerAttackBase
{
    [Header("Raycast Settings")]
    public float checkDistance = 1f;  
    public LayerMask Enemy_Layer;     
    public Transform rayOrigin;       

    private RaycastHit2D hitInfo;
    public override void OnPointerDownAttack()
    {
        isAttack = true;
    }

    public override void OnPointerUpAttack()
    {
        isAttack = false;
    }
    public override void AttackWithKey()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnPointerDownAttack();

        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            OnPointerUpAttack();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    [ObserversRpc(bufferLast:true)]
    protected override void TriggerAttackAffterAttackTime()
    {
        hitInfo = Physics2D.Raycast(rayOrigin.position, Vector2.right, checkDistance, Enemy_Layer);

        // debug hiển thị tia trong Scene view
        if (hitInfo.collider != null)
        {
           if(hitInfo.collider.TryGetComponent(out IHitable Enemy))
            {
                Enemy.OnHit(info.Damge);
            }    
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector2 start = rayOrigin.position;
        Vector2 end = start + Vector2.right * checkDistance;
        Gizmos.DrawLine(start, end);
    }
}
