using UnityEngine;

public class BulletBotcanh : MonoBehaviour
{
     public float dame;
    public float dir;
    [SerializeField] public float speed;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
   public float dirY = 0f; // chiều Y ngẫu nhiên

    private void Update()
    {
        // Di chuyển theo cả X và Y
        Vector2 move = new Vector2(dir, dirY).normalized * speed * Time.deltaTime;
        transform.Translate(move);
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
