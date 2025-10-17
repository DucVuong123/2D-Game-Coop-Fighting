using UnityEngine; 
public interface IShootingBehavior
{
    void Shoot(Transform firePoint, Transform target, GameObject bulletPrefab, float bulletSpeed);
}