using UnityEngine;

public abstract class Iteam_Base : MonoBehaviour
{
    [SerializeField] protected int Number_Increast_Index;
    [SerializeField] protected float roate_item_speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.Rotate(new Vector3(0, roate_item_speed * Time.deltaTime, 0));
    }

    protected abstract void OnReciveItem(PlayerMovementBase player);
}
