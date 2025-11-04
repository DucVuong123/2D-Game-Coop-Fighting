using UnityEngine;
using System;
public abstract class PlayerSpecialSkillBase : MonoBehaviour
{
    public event EventHandler OnEnterSkill;

    protected PlayerInfo info;
   [SerializeField] protected float time_End_Skill;
    public int skill_Amount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        info = GetComponent<PlayerInfo>();
    }
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        UseSkillWithKey();
    }
    public virtual void OnPointerDownSkill()
    {
        skill_Amount--;
        if(skill_Amount>=0)
        {
            EnterSpecialSkill();
            Invoke(nameof(EndSpecialSkill), time_End_Skill);
            OnEnterSkill?.Invoke(this, EventArgs.Empty);
        }    
    }
    public virtual void OnPointerUpSkill()
    {

    }
    protected abstract void UseSkillWithKey();
    protected abstract void EnterSpecialSkill();
    protected abstract void EndSpecialSkill();

    protected abstract void EnterPassiveSkill();
    protected abstract void EndPassiveSkill();
}
