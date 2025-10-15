using UnityEngine;

public class BoBinhSpecialSkill : PlayerSpecialSkillBase
{
    private float old_Attack;
    protected override void Start()
    {
        base.Start();
        old_Attack = info.AttackSpeed;
    }
    protected override void EndSpecialSkill()
    {
        info.AttackSpeed = old_Attack;
        GetComponent<PlayerAttackBase>().UpdateTimeAttack();
    }

    protected override void EnterSpecialSkill()
    {
       info.AttackSpeed=0.2f;
    }
}
