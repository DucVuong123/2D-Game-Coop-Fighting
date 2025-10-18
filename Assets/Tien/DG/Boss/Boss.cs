using System.Collections;
using UnityEngine;

public class Boss : EyeBotBase
{
    [Header("Shooting Settings")]
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float shootInterval = 0.2f;
    public float damage = 1f;

    [Header("Teleport Skill Points")]
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    [Header("Summon Skill")]
    public GameObject soldierPrefab;
    public Transform summonPoint;

    [Header("Skill Settings")]
    public float skillInterval = 5f;
    public float losePlayerThreshold = 10f;

    private bool isShooting = false;
    private Transform shootTarget;
    private float lastSeenPlayerTime = 0f;

    private ISkill[] skills;

    private void Awake()
    {
        // Khởi tạo skill trực tiếp trong code
        skills = new ISkill[]
        {
            new TeleportBossSkill { pointA = pointA, pointB = pointB, pointC = pointC },
            new SummonBossSkill { soldierPrefab = soldierPrefab, summonPoint = summonPoint }
        };
    }

    private void Start()
    {
        StartCoroutine(SkillRoutine());
    }

    #region Shooting
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

    private void ShootOnce(Transform target)
    {
        if (!firePoint || !bulletPrefab || target == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        BulletBotcanh bb = bullet.GetComponent<BulletBotcanh>();
        if (bb != null)
        {
            bb.dir = target.position.x > firePoint.position.x ? 1f : -1f;
            bb.dame = damage;
            bb.speed = bulletSpeed;
            Destroy(bullet, 5f);
        }
    }
    #endregion

    #region Skills
    private IEnumerator SkillRoutine()
    {
        while (true)
        {
            if (shootTarget != null)
                lastSeenPlayerTime = Time.time;

            if (skills != null && skills.Length > 0)
            {
                int index = Random.Range(0, skills.Length);
                ISkill chosenSkill = skills[index];

                if (chosenSkill is TeleportBossSkill tp)
                {
                    if (shootTarget == null || Time.time - lastSeenPlayerTime > losePlayerThreshold)
                        tp.UseSkill(transform, null);
                    else
                        tp.UseSkill(transform, shootTarget);
                }
                else
                {
                    chosenSkill.UseSkill(transform, shootTarget);
                }
            }

            yield return new WaitForSeconds(skillInterval);
        }
    }
    #endregion
}

// ==================== Skill Classes ====================


[System.Serializable]
public class TeleportBossSkill : ISkill
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public void UseSkill(Transform user, Transform target)
    {
        if (target == null)
            user.position = pointC.position;
        else
            user.position = Random.value > 0.5f ? pointA.position : pointB.position;
    }
}

[System.Serializable]
public class SummonBossSkill : ISkill
{
    public GameObject soldierPrefab;
    public Transform summonPoint;

    public void UseSkill(Transform user, Transform target)
    {
        if (soldierPrefab != null && summonPoint != null)
        {
            for (int i = 0; i < 3; i++)
                Object.Instantiate(soldierPrefab, summonPoint.position, Quaternion.identity);
        }
    }
}
