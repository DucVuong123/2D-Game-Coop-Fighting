using UnityEngine;
using System.Collections; // <-- cần để dùng Coroutine

public class BoBinhAttack : PlayerAttackBase
{
    [SerializeField] private BulletController bullet_Prefab;
    [SerializeField] private Transform bullet_Spawn_Pos;

    [SerializeField] private float burstInterval = 0.2f; // Thời gian giữa mỗi viên
    [SerializeField] private int burstCount = 4;         // Số viên mỗi lần bắn loạt
    [SerializeField] private float restTime = 1f;        // Thời gian nghỉ sau khi bắn 3 viên

    private bool isShooting = false;

    public override void OnPointerDownAttack()
    {
        isAttack = true;
    }

    public override void OnPointerUpAttack()
    {
        isAttack = false;
    }

    protected override void TriggerAttackAffterAttackTime()
{
    if (!isShooting)
    {
        StartCoroutine(BurstFire());
    }
}

private IEnumerator BurstFire()
{
    isShooting = true;

    do
    {
        // Bắn 1 loạt
        for (int i = 0; i < burstCount; i++)
        {
            BulletController bulletInstance = Instantiate(
                bullet_Prefab,
                bullet_Spawn_Pos.position,
                bullet_Prefab.transform.rotation
            );

            bulletInstance.dame = info.Damge;
            bulletInstance.dir = Mathf.Sign(transform.localScale.x);

            yield return new WaitForSeconds(burstInterval);
        }

        // Nếu vẫn giữ thì nghỉ rồi bắn tiếp
        if (isAttack)
            yield return new WaitForSeconds(restTime);

    } while (isAttack); // Nếu không giữ nữa thì thoát

    isShooting = false;
}
}
