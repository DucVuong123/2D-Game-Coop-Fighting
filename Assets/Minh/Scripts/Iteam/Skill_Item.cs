using UnityEngine;

public class Skill_Item : Iteam_Base
{
    protected override void OnReciveItem(PlayerMovementBase player)
    {
        player.gameObject.GetComponent<PlayerSpecialSkillBase>().OnIncreaseSkill(Number_Increast_Index);
        Destroy(this.gameObject, 0.05f);
    }

    protected override void Update()
    {
        base.Update();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerMovementBase>(out PlayerMovementBase player))
        {
            OnReciveItem(player);
        }
    }
}
