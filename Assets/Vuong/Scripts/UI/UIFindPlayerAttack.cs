using UnityEngine;

public class UIFindPlayerAttack : MonoBehaviour
{
    private PlayerAttackBase m_Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_Player = FindObjectOfType<PlayerAttackBase>();
    }
    public void OnPointerDownAttack()
    {
        m_Player.OnPointerDownAttack();
    }
    public void OnPointerUpAttack()
    {
        m_Player.OnPointerUpAttack();
    }
}
