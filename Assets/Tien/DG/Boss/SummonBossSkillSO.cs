using UnityEngine;


[CreateAssetMenu(fileName = "SummonSkill", menuName = "Boss/SummonSkill")]
public class SummonBossSkillSO : BossSkillSO
{
    public GameObject soldierPrefab;
    public Transform summonPoint;

    public override void UseSkill(Transform user, Transform target)
    {
        if (soldierPrefab != null && summonPoint != null)
        {
            for (int i = 0; i < 3; i++)
                Object.Instantiate(soldierPrefab, summonPoint.position, Quaternion.identity);
        }
    }
}
