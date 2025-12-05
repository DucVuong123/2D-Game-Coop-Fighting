using UnityEngine;
using System;
using PurrNet;
public abstract class PlayerSpecialSkillBase : NetworkBehaviour
{
    public event EventHandler OnEnterSkill;

    protected PlayerInfo info;
   [SerializeField] protected float time_End_Skill;
   public bool is_Use_Skill = false;
    public int skill_Amount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        enabled = isOwner;
    }
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
    public virtual void OnIncreaseSkill(int amount)
    {
        skill_Amount+=amount;
        OnEnterSkill?.Invoke(this, EventArgs.Empty);
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
