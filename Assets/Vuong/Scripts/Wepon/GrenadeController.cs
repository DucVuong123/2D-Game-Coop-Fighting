using System.Collections;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    //[Header("Throw Settings")]
/*    public float throwForce = 8f;      */         // lực ném
    //public Vector2 throwDirection = Vector2.right; // hướng ném mặc định

    [Header("Explosion Settings")]
    public float fuseTime = 2f;                 // thời gian chờ trước khi nổ
    public float explosionRadius = 2.5f;        // bán kính nổ
    //public float explosionForce = 500f;         // lực đẩy vật thể
    public float maxDamage = 50f;               // sát thương tối đa ở tâm vụ nổ
    public LayerMask damageMask;                // layer chịu ảnh hưởng (Enemy, Player...)

    private Rigidbody2D rb;
    private bool hasExploded = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(FuseAndExplode());
    }

    public void Throw(Vector2 direction, float force)
    {
        rb.AddForce(direction.normalized * force, ForceMode2D.Force);
    }

    IEnumerator FuseAndExplode()
    {
        yield return new WaitForSeconds(fuseTime);
        Explode();
    }

    void Explode()
    {
        if (hasExploded) return;
        hasExploded = true;

        // Tìm tất cả collider trong vùng nổ
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageMask);

        foreach (Collider2D hit in hits)
        {
            // Nếu đối tượng có Rigidbody2D thì đẩy nó ra xa
            //Rigidbody2D targetRb = hit.attachedRigidbody;
            //if (targetRb != null)
            //{
            //    Vector2 dir = (targetRb.position - (Vector2)transform.position).normalized;
            //    float dist = Vector2.Distance(transform.position, targetRb.position);
            //    float ratio = Mathf.Clamp01(1f - dist / explosionRadius);
            //    targetRb.AddForce(dir * explosionForce * ratio);
            //}

            // Nếu đối tượng có script nhận damage
            if(hit.TryGetComponent(out IHitable enemy))
            {
                float dist = Vector2.Distance(transform.position, hit.transform.position);
                float ratio = Mathf.Clamp01(1f - dist / explosionRadius);
                float damage = ratio * maxDamage;
                enemy.OnHit(damage);
            }
        }

        Destroy(gameObject); // xóa ngay sau khi nổ
    }

    // Vẽ bán kính nổ trong Scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
