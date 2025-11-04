using UnityEngine;

public abstract class PlayerAttackBase : MonoBehaviour
{
    protected PlayerInfo info;
    public bool isAttack;
    protected float time_Attack;
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
