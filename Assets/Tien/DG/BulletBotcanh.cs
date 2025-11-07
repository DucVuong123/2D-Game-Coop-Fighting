using UnityEngine;

public class BulletBotcanh : MonoBehaviour
{
    public float dame;
    public float dir;
    [SerializeField] public float speed;
    public float dirY = 0f; // chiều Y ngẫu nhiên
    [SerializeField] private string targetLayerName = "Player"; // tên Layer mục tiêu

    void Start()
    {
        Destroy(gameObject, 20f); // Tự hủy sau 20 giây
    }

    void Update()
    {
        // Di chuyển theo cả X và Y
        Vector2 move = new Vector2(dir, dirY).normalized * speed * Time.deltaTime;
        transform.Translate(move);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Chỉ xử lý khi đối tượng va chạm thuộc layer Player
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetLayerName))
        {
            if (collision.TryGetComponent(out IHitable hit))
            {
                hit.OnHit(dame);
            }

            Destroy(gameObject);
        }
    }
}
