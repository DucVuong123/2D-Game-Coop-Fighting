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

    public override void AttackWithKey()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            OnPointerDownAttack();

        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            OnPointerUpAttack();
        }
    }

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
        //if (!isShooting)
        //{
        //    StartCoroutine(BurstFire());
        //}
        BoBinhSpecialSkill skill = GetComponent<BoBinhSpecialSkill>();
        if (!skill.is_Use_Skill)
            BurstFire();
        else
            SkillFire();
}
    private void SkillFire()
    {
        BulletController bulletInstance1 = Instantiate(
                   bullet_Prefab,
                   bullet_Spawn_Pos.position,
                   bullet_Prefab.transform.rotation
               );
        bulletInstance1.dame = info.Damge;
        bulletInstance1.dir = Mathf.Sign(transform.localScale.x);
    }
    private void BurstFire()
{
        //isShooting = true;

        //do
        //{
        //    // Bắn 1 loạt
        //    for (int i = 0; i < burstCount; i++)
        //    {
        //        BulletController bulletInstance = Instantiate(
        //            bullet_Prefab,
        //            bullet_Spawn_Pos.position,
        //            bullet_Prefab.transform.rotation
        //        );

        //        bulletInstance.dame = info.Damge;
        //        bulletInstance.dir = Mathf.Sign(transform.localScale.x);

        //        yield return new WaitForSeconds(burstInterval);
        //    }

        //    // Nếu vẫn giữ thì nghỉ rồi bắn tiếp
        //    if (isAttack)
        //        yield return new WaitForSeconds(restTime);

        //} while (isAttack); // Nếu không giữ nữa thì thoát

        //isShooting = false;

        Vector3 leftOffset = bullet_Spawn_Pos.right * -0.8f;
        Vector3 rightOffset = bullet_Spawn_Pos.right * 0.8f;
        BulletController bulletInstance1 = Instantiate(
                   bullet_Prefab,
                   bullet_Spawn_Pos.position + leftOffset,
                   bullet_Prefab.transform.rotation
               );
        BulletController bulletInstance2 = Instantiate(
                 bullet_Prefab,
                 bullet_Spawn_Pos.position,
                 bullet_Prefab.transform.rotation
             );
        BulletController bulletInstance3 = Instantiate(
                 bullet_Prefab,
                 bullet_Spawn_Pos.position + rightOffset,
                 bullet_Prefab.transform.rotation
             );

        bulletInstance1.dame = info.Damge;
        bulletInstance1.dir = Mathf.Sign(transform.localScale.x);

        bulletInstance2.dame = info.Damge;
        bulletInstance2.dir = Mathf.Sign(transform.localScale.x);

        bulletInstance3.dame = info.Damge;
        bulletInstance3.dir = Mathf.Sign(transform.localScale.x);
    }
}
