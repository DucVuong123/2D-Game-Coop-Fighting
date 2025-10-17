using UnityEngine;

public abstract class EyeBotBase : MonoBehaviour
{
    [Header("Vision Settings")]
    public Transform eyePoint;
    public float frontRange = 20f;
    public float backRange = 15f;
    public LayerMask playerMask;
    public LayerMask obstacleMask;

    [SerializeField]protected bool faceRight = true;
    protected Transform detectedPlayer;
    protected bool isPlayerDetected = false;

    protected virtual void Update()
    {
        ScanForPlayer();
    }

    void ScanForPlayer()
    {
        if (!eyePoint) eyePoint = transform;
        Vector2 origin = eyePoint.position;
        Vector2 dirFront = faceRight ? Vector2.right : Vector2.left;
        Vector2 dirBack = -dirFront;

        int rayMask = playerMask.value | obstacleMask.value;

        if (SeenAlongDir(origin, dirFront, frontRange, rayMask, out RaycastHit2D hitF))
        {
            if (!isPlayerDetected) OnPlayerDetected(hitF.collider.transform);
            isPlayerDetected = true;
            detectedPlayer = hitF.collider.transform;
            return;
        }

                if (backRange > 0 && SeenAlongDir(origin, dirBack, backRange, rayMask, out RaycastHit2D hitB))
        {
            Flip();
            if (!isPlayerDetected) OnPlayerDetected(hitB.collider.transform);
            isPlayerDetected = true;
            detectedPlayer = hitB.collider.transform;
            return;
        }

        if (isPlayerDetected)
        {
            OnPlayerLost();
            isPlayerDetected = false;
            detectedPlayer = null;
        }
    }

    protected abstract void OnPlayerDetected(Transform player);
    protected abstract void OnPlayerLost();

    protected void Flip()
    {
        faceRight = !faceRight;
        Vector3 local = transform.localScale;
        local.x *= -1;
        transform.localScale = local;
    }

    bool SeenAlongDir(Vector2 origin, Vector2 dir, float dist, int mask, out RaycastHit2D hitPlayer)
    {
        hitPlayer = default;
        RaycastHit2D[] hits = Physics2D.RaycastAll(origin, dir, dist, mask);
        if (hits.Length == 0) return false;

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));
        var first = hits[0];
        int firstLayer = first.collider.gameObject.layer;

        if ((obstacleMask.value & (1 << firstLayer)) != 0) return false;
        if ((playerMask.value & (1 << firstLayer)) != 0)
        {
            hitPlayer = first;
            return true;
        }
        return false;
    }
}
