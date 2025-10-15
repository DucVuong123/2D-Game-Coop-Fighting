using UnityEngine;

public class BoBinhAttack : PlayerAttackBase
{
   [SerializeField] private BulletController bullet_Prefab;
   [SerializeField] private Transform bullet_Spawn_Pos;
    public override void OnPointerDownAttack()
    {
        isAttack = true;
    }

    public override void OnPointerUpAttack()
    {
        isAttack = false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
    protected override void TriggerAttackAffterAttackTime()
    {
        BulletController bulletInstance = Instantiate(bullet_Prefab, bullet_Spawn_Pos.position, bullet_Prefab.transform.rotation);
        bulletInstance.dame = info.Damge;
    }
}
