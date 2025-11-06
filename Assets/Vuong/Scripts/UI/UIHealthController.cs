using UnityEngine;
using UnityEngine.UI;

public class UIHealthController : MonoBehaviour
{
    [SerializeField] private Image fillImg;
     private HealthSystem health;
    // Start is called before the first frame update
    void Start()
    {
        health = transform.root.GetComponent<HealthSystem>();
        health.OnHealthChange += Health_OnHealthChange;
    }

    private void Health_OnHealthChange(object sender, System.EventArgs e)
    {
        fillImg.fillAmount = health.healthAmount / health.healthAmountMax;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
