using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float dame;
    public float dir;
    [SerializeField] private LayerMask targetLayerName;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right* dir * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((targetLayerName.value & (1 << collision.gameObject.layer)) != 0)
        {
            
            if (collision.TryGetComponent(out IHitable hit))
            {
                hit.OnHit(dame);
            }

            Destroy(gameObject);
        }
    }
}
