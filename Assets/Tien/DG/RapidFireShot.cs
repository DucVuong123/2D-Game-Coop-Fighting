using System.Collections;
using UnityEngine;

public class RapidFireShot : IShootingBehavior
{
   private bool isFiring = false;  // Đảm bảo không chạy nhiều lần cùng lúc

    public void Shoot(Transform firePoint, Transform target, GameObject bulletPrefab, float bulletSpeed)
    {
        if (!isFiring)
        {
            MonoBehaviour owner = firePoint.GetComponentInParent<MonoBehaviour>();
            owner.StartCoroutine(FireBurst(firePoint, target, bulletPrefab, bulletSpeed));
        }
    }

    private IEnumerator FireBurst(Transform firePoint, Transform target, GameObject bulletPrefab, float bulletSpeed)
    {
        isFiring = true;

        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

            float direction = target.position.x > firePoint.position.x ? 1f : -1f;
            BulletBotcanh bulletComp = bullet.GetComponent<BulletBotcanh>();
            bulletComp.dir = direction;
            bulletComp.speed = bulletSpeed; // đảm bảo speed có gán

            yield return new WaitForSeconds(0.2f); // delay giữa đạn
        }

        yield return new WaitForSeconds(2f); // nghỉ sau loạt bắn

        isFiring = false;
    }
}
