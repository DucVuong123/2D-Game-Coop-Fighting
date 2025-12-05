using PurrNet;
using UnityEngine;

public class BoBinhSpecialSkill : PlayerSpecialSkillBase
{
    private float old_Attack;
    private GameObject  skill_Vfx;
    protected override void Start()
    {
        base.Start();
        old_Attack = info.AttackSpeed;
    }
    [ObserversRpc]
    protected override void EndSpecialSkill()
    {
        info.AttackSpeed = old_Attack;
        is_Use_Skill = false;
    }
    [ObserversRpc(bufferLast:true)]
    protected override void EnterSpecialSkill()
    {
        if(!is_Use_Skill)
        {
            VfxAsset.Instance.SpawnVfx(VfxAsset.Instance.bo_Binh_Skill,new Vector2(transform.position.x, transform.position.y-1.3f) ,time_End_Skill,transform);
            info.AttackSpeed = 0.1f;
            GetComponent<PlayerAttackBase>().UpdateTimeAttack();
            is_Use_Skill = true;
        }    

    }

    protected override void EnterPassiveSkill()
    {
       
    }

    protected override void EndPassiveSkill()
    {
       
    }

    protected override void UseSkillWithKey()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnPointerDownSkill();

        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            OnPointerUpSkill();
        }
       
    }
}
