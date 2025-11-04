using UnityEngine;

public class DuKichSpecialSkill : PlayerSpecialSkillBase
{
   [SerializeField] private GrenadeController grenade_Prefab;
   [SerializeField] private Transform grenade_Spawn_Pos;
    [Header("Grass Check")]
    [SerializeField] private Transform grass_Check_Pos;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask grass_Layer;
    [SerializeField] private bool IsInGrass;
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
        CheckInGrass();

        if(IsInGrass)
            EnterPassiveSkill();
        else
            EndPassiveSkill();

    }
    protected override void EndSpecialSkill()
    {
       
    }

    protected override void EnterSpecialSkill()
    {
        GrenadeController grenadeInstance = Instantiate(grenade_Prefab, grenade_Spawn_Pos.position, grenade_Prefab.transform.rotation);
        grenadeInstance.Throw(new Vector2(1,0.2f) * Mathf.Sign(transform.localScale.x), 500);
    }

    protected override void EnterPassiveSkill()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
    }

    protected override void EndPassiveSkill()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
    }
    private void CheckInGrass()
    {
        Collider2D hit = Physics2D.OverlapCircle(grass_Check_Pos.position, radius, grass_Layer);
        IsInGrass = (hit != null);
    }
    protected override void UseSkillWithKey()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            OnPointerDownSkill();

        }
        if (Input.GetKeyUp(KeyCode.K))
        {
            OnPointerUpSkill();
        }

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(grass_Check_Pos.position, radius);
    }
}
