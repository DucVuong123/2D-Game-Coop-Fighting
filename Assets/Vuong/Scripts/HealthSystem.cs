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
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
        {
            SetEvent();
            return;
        }


    }
    private void Start()
    {
       
    }
    [ObserversRpc(bufferLast: true)]
    private void SetEvent()
    {
        OnDead += HealthSystem_OnDead;
    }
    
    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        Debug.Log("Dead");
        if(gameObject.GetComponent<Boss>() != null)
        {
            Debug.Log("Minhhhhhhhhhhhhh");
            GameController.Instance.OnEndMap();

            Destroy(gameObject);
            return;
        }
        if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            GameController.Instance.currentMap.GetComponent<MapController>().Instance.Increast_Point(10);

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
