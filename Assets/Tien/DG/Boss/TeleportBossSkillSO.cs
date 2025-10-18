using UnityEngine;

[CreateAssetMenu(fileName = "TeleportSkill", menuName = "Boss/TeleportSkill")]
public class TeleportBossSkillSO : BossSkillSO
{
    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public override void UseSkill(Transform user, Transform target)
    {
        if (target == null)
            user.position = pointC.position;
        else
            user.position = Random.value > 0.5f ? pointA.position : pointB.position;
    }
}
