using UnityEngine;

public class SingleShot : IShootingBehavior
{
    public void Shoot(Transform firePoint, Transform target, GameObject bulletPrefab, float bulletSpeed)
{
    GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
    float direction = target.position.x > firePoint.position.x ? 1f : -1f;

    bullet.GetComponent<BulletController>().dir = direction; 
}

}
