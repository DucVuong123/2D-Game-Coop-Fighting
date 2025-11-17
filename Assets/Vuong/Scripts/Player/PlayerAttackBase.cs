using System.Globalization;
using UnityEngine;
using PurrNet;
public abstract class PlayerAttackBase : NetworkBehaviour
{
    protected PlayerInfo info;
    public bool isAttack;
    protected float time_Attack;
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
        AttackWithKey();
        if (isAttack)
        {
            if (time_Attack <= 0)
            {
                TriggerAttackAffterAttackTime();
                UpdateTimeAttack();
            }
            else
            {
                time_Attack -= Time.deltaTime;
            }
        }     
    }
    public abstract void AttackWithKey();
    public abstract void OnPointerDownAttack();
    public abstract void OnPointerUpAttack();
    protected virtual void TriggerAttackAffterAttackTime()
    { 
    
    }
    public void UpdateTimeAttack()
    {
        time_Attack = info.AttackSpeed;
    }    
}
