using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float dame;
    [SerializeField] private float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IHitable hit))
        {
            hit.OnHit(dame);
            Destroy(gameObject);
        }
    }
}
