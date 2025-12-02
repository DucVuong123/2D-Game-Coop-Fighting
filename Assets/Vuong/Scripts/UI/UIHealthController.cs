using UnityEngine;
using UnityEngine.UI;
using PurrNet;
public class UIHealthController : NetworkBehaviour
{
    [SerializeField] private Image fillImg;
     private HealthSystem health;
    // Start is called before the first frame update
    protected override void OnSpawned()
    {
        base.OnSpawned();
        health = transform.parent.parent.GetComponent<HealthSystem>();
        fillImg.fillAmount = health.healthAmount.value / health.healthAmountMax;
        health.healthAmount.onChanged += HealthAmount_onChanged; ;
    }

    private void HealthAmount_onChanged(float obj)
    {
        fillImg.fillAmount = obj / health.healthAmountMax;
    }

    void Start()
    {
      
    }

    private void Health_OnHealthChange(object sender, System.EventArgs e)
    {
        fillImg.fillAmount = health.healthAmount.value / health.healthAmountMax;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
