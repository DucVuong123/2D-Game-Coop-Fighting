using System.Collections;
using UnityEngine;

public class Sungmay : EyeBotBase
{
    [Header("Shooting Settings")]
    public Transform[] firePoints;       // Mảng các điểm bắn
    public GameObject bulletPrefab;      // Prefab đạn
    public float bulletSpeed = 10f;      // Tốc độ đạn
    public float shootInterval = 0.2f;   // Thời gian giữa các viên đạn
    public float damage = 1f;            // Sát thương mỗi viên
    public int bulletsBeforePause = 10;  // Số viên bắn trước khi tạm dừng
    public float pauseDuration = 5f;     // Thời gian tạm dừng (giây)

    private bool isShooting = false;
    private Transform shootTarget;

    protected override void OnPlayerDetected(Transform player)
    {
        if (player == null) return;

        shootTarget = player;
        FlipTowardsPlayer(player);

        if (!isShooting)
            StartCoroutine(ShootRoutine());
    }

    protected override void OnPlayerLost()
    {
        shootTarget = null;
        isShooting = false;
        StopAllCoroutines();
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;
        int count = 0;

        while (shootTarget != null)
        {
            FlipTowardsPlayer(shootTarget);
            ShootOnce(shootTarget);
            count++;

            if (count >= bulletsBeforePause)
            {
                count = 0;
                yield return new WaitForSeconds(pauseDuration); // tạm dừng
            }
            else
            {
                yield return new WaitForSeconds(shootInterval);
            }
        }

        isShooting = false;
    }

    private void FlipTowardsPlayer(Transform target)
    {
        if (!target) return;

        bool shouldFaceRight = target.position.x > transform.position.x;
        if (faceRight != shouldFaceRight)
        {
            faceRight = shouldFaceRight;
            Vector3 local = transform.localScale;
            local.x = Mathf.Sign(local.x) * Mathf.Max(Mathf.Abs(local.x), 0.1f);
            transform.localScale = local;
        }
    }

    public void ShootOnce(Transform target)
{
    if (firePoints == null || firePoints.Length == 0 || !bulletPrefab || target == null)
        return;

    Transform chosenFirePoint = firePoints[Random.Range(0, firePoints.Length)];

    GameObject bullet = Instantiate(bulletPrefab, chosenFirePoint.position, Quaternion.identity);
    BulletBotcanh bb = bullet.GetComponent<BulletBotcanh>();

    if (bb != null)
    {
        // Xác định hướng X
        float dirX = target.position.x > chosenFirePoint.position.x ? 1f : -1f;

        // Thêm offset Y ngẫu nhiên (-0.3 đến 0.3)
        float dirY = Random.Range(-0.3f, 0.3f);

        // Gán vào bullet
        bb.dir = dirX;
        bb.dame = damage;
        bb.speed = bulletSpeed;
        bb.dirY = dirY; // cần thêm biến dirY trong BulletBotcanh

        Destroy(bullet, 5f);
    }
}
}
