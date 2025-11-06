using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
public class HealthSystem : MonoBehaviour,IHitable
{
    public event EventHandler OnHealthChange;
    public event EventHandler OnHealthReduce;
    public event EventHandler OnDead;

    [SerializeField] public float healthAmount;
    [SerializeField] public float healthAmountMax;
    private void Awake()
    {
        healthAmount = healthAmountMax;
    }
    private void Start()
    {
        OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Destroy(gameObject);
    }

    public void Damge(float damgeAmount)
    {
        healthAmount -= damgeAmount;
        if (healthAmount <= 0)
        {
            healthAmount = 0;
            OnDead?.Invoke(this, EventArgs.Empty);
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
        OnHealthReduce?.Invoke(this, EventArgs.Empty);
    }
    public void IncreasedBlood(float HealthAmount)
    {
        healthAmount += HealthAmount;
        if (healthAmount >= healthAmountMax)
        {
            healthAmount = healthAmountMax;
        }
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }
    public float GetHealthNormalized()
    {
        return (float)healthAmount / healthAmountMax;
    }

    public void OnHit(float Dame)
    {
        VfxAsset.Instance.SpawnVfx(VfxAsset.Instance.blood_Hit, transform.position, 3);
        Damge(Dame);
    }
}
