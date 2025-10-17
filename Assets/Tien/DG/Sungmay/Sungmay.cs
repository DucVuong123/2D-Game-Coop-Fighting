using UnityEngine;

public class Sungmay : EyeBotBase
{
    [Header("Shooting Settings")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    public float bulletSpeed = 10f;

    private float shootTimer = 0f;
    private bool firstShotDone = false;
    private IShootingBehavior shootingBehavior;

    private void Start()
    {
        faceRight = false;   // ✅ Mặc định quay sang trái
    Vector3 scale = transform.localScale;
    scale.x = Mathf.Abs(scale.x) * (faceRight ? 1 : -1);
    transform.localScale = scale;

        shootingBehavior = new RapidFireShot();
    }

    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (!firstShotDone)
            {
                // BẮN NGAY PHÁT ĐẦU
                shootingBehavior?.Shoot(firePoint, detectedPlayer, bulletPrefab, bulletSpeed);
                firstShotDone = true;
                shootTimer = fireRate; 
            }
            else
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0f)
                {
                    shootingBehavior?.Shoot(firePoint, detectedPlayer, bulletPrefab, bulletSpeed);
                    shootTimer = fireRate;
                }
            }
        }
        else
        {
            firstShotDone = false; // Reset khi mất dấu
            shootTimer = 0f;
        }
    }

    public void SetShootingBehavior(IShootingBehavior behavior)
    {
        shootingBehavior = behavior;
    }

    protected override void OnPlayerDetected(Transform player)
    {
        if (player.position.x > transform.position.x && faceRight)
            Flip();
        else if (player.position.x < transform.position.x && !faceRight)
            Flip();
    }

    protected override void OnPlayerLost()
    {
        Debug.Log("🚫 Lính mất dấu player!");
    }
}
