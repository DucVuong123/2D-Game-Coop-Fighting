using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using PurrNet;
public class HealthSystem : NetworkBehaviour,IHitable
{
    public event EventHandler OnHealthChange;
    public event EventHandler OnHealthReduce;
    public event EventHandler OnDead;

    [SerializeField] public SyncVar<float> healthAmount=new();
    public float healthAmountMax;
    private void Awake()
    {
 
    }
    private void Start()
    {
        OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }
    [ServerRpc]
    public void Damge(float damgeAmount)
    {
        healthAmount.value -= damgeAmount;
        if (healthAmount.value <= 0)
        {
            healthAmount.value = 0;
        }
        CheckHealth();
    }
    [ObserversRpc(bufferLast: true)]
    private void CheckHealth()
    {
        VfxAsset.Instance.SpawnVfx(VfxAsset.Instance.blood_Hit, transform.position, 3);
        if (healthAmount.value <= 0)
        {
            OnDead?.Invoke(this, EventArgs.Empty);
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
        OnHealthReduce?.Invoke(this, EventArgs.Empty);
    }    
    //public void IncreasedBlood(float HealthAmount)
    //{
    //    healthAmount += HealthAmount;
    //    if (healthAmount >= healthAmountMax)
    //    {
    //        healthAmount = healthAmountMax;
    //    }
    //    OnHealthChange?.Invoke(this, EventArgs.Empty);
    //}
    //public float GetHealthNormalized()
    //{
    //    return (float)healthAmount / healthAmountMax;
    //}

    public void OnHit(float Dame)
    {

        Damge(Dame);
    }
}
