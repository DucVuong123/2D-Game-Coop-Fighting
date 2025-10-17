using UnityEngine;

public class BulletBotcanh : MonoBehaviour
{
    public float dame;
    public float dir;
    [SerializeField] public float speed;

    void Start()
    {
        Destroy(gameObject, 20); // Tự huỷ nếu không trúng gì sau 20s
    }
    void Update()
{
    transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
}

   private void OnTriggerEnter2D(Collider2D collision)
{
    Debug.Log("Trigger với: " + collision.name);

    if (collision.CompareTag("Player"))
    {
        Debug.Log("Đạn trúng Player!");

        if (collision.TryGetComponent(out IHitable hit))
        {
            hit.OnHit(dame);
        }

        Destroy(gameObject);
    }
}


}
