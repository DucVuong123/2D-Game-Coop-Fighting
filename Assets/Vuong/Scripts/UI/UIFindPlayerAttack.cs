using UnityEngine;
using PurrNet;
public class UIFindPlayerAttack : NetworkBehaviour
{
    private PlayerAttackBase m_Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        Invoke(nameof(Find_Player), 0);

    }
    private void Awake()
    {
       
    }

    private void Find_Player() => m_Player = FindObjectOfType<PlayerAttackBase>();
    public void OnPointerDownAttack()
    {
        m_Player.OnPointerDownAttack();
    }
    public void OnPointerUpAttack()
    {
        m_Player.OnPointerUpAttack();
    }
}
