using UnityEngine;
using System.Collections;

/// <summary>
/// Bot bắn tự động khi phát hiện player.
/// Kế thừa EyeBotBase để detect player trước/sau.
/// Khi detect player, sẽ tự flip hướng về player trước khi bắn.
/// Bắn liên tục theo shootInterval, không bị dừng giữa chừng.
/// </summary>
public class Linhcanh : EyeBotBase
{
    [Header("Shooting Settings")]
    public Transform firePoint;         // Điểm bắn đạn
    public GameObject bulletPrefab;     // Prefab đạn
    public float bulletSpeed = 10f;     // Tốc độ đạn
    public float shootInterval = 0.2f;  // Thời gian giữa các viên đạn (giây)
    public float damage = 1f;           // Sát thương mỗi viên đạn

    private bool isShooting = false;
    private Transform shootTarget;      // Target hiện tại để bắn

    /// <summary>
    /// Khi phát hiện player
    /// </summary>
    protected override void OnPlayerDetected(Transform player)
    {
        if (player == null) return;

        // Lưu target để bắn
        shootTarget = player;

        // Flip hướng về player
        FlipTowardsPlayer(player);

        // Bắt đầu Coroutine bắn nếu chưa chạy
        if (!isShooting)
            StartCoroutine(ShootRoutine());
    }

    /// <summary>
    /// Khi mất player hoàn toàn
    /// </summary>
    protected override void OnPlayerLost()
    {
        shootTarget = null;
        isShooting = false;
        StopAllCoroutines();
    }

    /// <summary>
    /// Coroutine bắn liên tục cho đến khi player mất hoàn toàn
    /// </summary>
    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        while (shootTarget != null)
        {
            // Flip hướng trước khi bắn
            FlipTowardsPlayer(shootTarget);

            // Bắn 1 viên
            ShootOnce(shootTarget);

            // Chờ interval
            yield return new WaitForSeconds(shootInterval);
        }

        isShooting = false;
    }

    /// <summary>
    /// Flip bot về phía target, an toàn tránh scale = 0
    /// </summary>
    /// <param name="target"></param>
    private void FlipTowardsPlayer(Transform target)
    {
        if (!target) return;

        bool shouldFaceRight = target.position.x > transform.position.x;
        if (faceRight != shouldFaceRight)
        {
            faceRight = shouldFaceRight;

            Vector3 local = transform.localScale;
            local.x = Mathf.Sign(local.x) * Mathf.Max(Mathf.Abs(local.x), 0.1f); // tránh 0
            transform.localScale = local;
        }
    }

    /// <summary>
    /// Bắn 1 viên đạn về hướng target
    /// </summary>
    /// <param name="target"></param>
    public void ShootOnce(Transform target)
    {
        if (!firePoint || !bulletPrefab || target == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BulletBotcanh bb = bullet.GetComponent<BulletBotcanh>();

        if (bb != null)
        {
            float dirX = target.position.x > firePoint.position.x ? 1f : -1f;
            bb.dir = dirX;
            bb.dame = damage;
            bb.speed = bulletSpeed;

            // Giảm thời gian sống đạn để tránh quá nhiều bullet trên scene
            Destroy(bullet, 5f);
        }
    }
}
