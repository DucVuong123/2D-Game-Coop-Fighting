using UnityEngine;

public abstract class BossSkillSO : ScriptableObject, ISkill
{
    public abstract void UseSkill(Transform user, Transform target);
}


