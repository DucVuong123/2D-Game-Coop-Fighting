using UnityEngine;

public class Linhgacbot : EyeBotBase
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float bulletSpeed = 10f;

    private float shootTimer = 0f;

    protected override void Update()
    {
        base.Update(); // gọi logic nhìn từ class cha

        if (isPlayerDetected)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= fireRate)
            {
                ShootAtPlayer();
                shootTimer = 0f;
            }
        }
    }

    protected override void OnPlayerDetected(Transform player)
    {
        Debug.Log("👀 Lính thấy player!");

        // ✅ Quay đầu về phía Player
        if (player.position.x > transform.position.x && faceRight)
            Flip();
        else if (player.position.x < transform.position.x && !faceRight)
            Flip();
    }

    protected override void OnPlayerLost()
    {
        Debug.Log("🚫 Lính mất dấu player!");
    }

    void ShootAtPlayer()
    {
        if (bulletPrefab == null || firePoint == null || detectedPlayer == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        float direction = detectedPlayer.position.x > firePoint.position.x ? 1f : -1f;
        rb.linearVelocity = new Vector2(direction * bulletSpeed, 0);
        Destroy(bullet, 40f / bulletSpeed);
    }
}
