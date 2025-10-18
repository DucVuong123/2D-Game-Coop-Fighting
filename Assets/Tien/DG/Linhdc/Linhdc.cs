using System.Collections;
using UnityEngine;

public class Linhdc : EyeBotBase
{
    [Header("Shooting Settings")]
    public Transform firePoint;         
    public GameObject bulletPrefab;     
    public float bulletSpeed = 10f;     
    public float shootInterval = 0.2f;  
    public float damage = 1f;           

    [Header("Patrol Settings")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 3f;

    private bool isShooting = false;
    private Transform shootTarget;

    private LinhdcSkill patrolSkill;

    private void Start()
    {
        patrolSkill = new LinhdcSkill
        {
            pointA = pointA,
            pointB = pointB,
            moveSpeed = patrolSpeed
        };
        patrolSkill.UseSkill(transform, null); // Bắt đầu patrol
    }

    protected override void OnPlayerDetected(Transform player)
    {
        if (player == null) return;

        shootTarget = player;

        // Dừng patrol khi phát hiện player
        patrolSkill.StopSkill();
        FlipTowardsPlayer(player);

        if (!isShooting)
            StartCoroutine(ShootRoutine());
    }

    protected override void OnPlayerLost()
    {
        shootTarget = null;
        isShooting = false;
        StopAllCoroutines();

        // Tiếp tục patrol khi player rời đi
        patrolSkill.UseSkill(transform, null);
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        while (shootTarget != null)
        {
            FlipTowardsPlayer(shootTarget);
            ShootOnce(shootTarget);
            yield return new WaitForSeconds(shootInterval);
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
        if (!firePoint || !bulletPrefab || target == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BulletBotcanh bb = bullet.GetComponent<BulletBotcanh>();

        if (bb != null)
        {
            float dirX = target.position.x > firePoint.position.x ? 1f : -1f;
            bb.dir = dirX;
            bb.dame = damage;
            bb.speed = bulletSpeed;
            Destroy(bullet, 5f);
        }
    }
}
