using UnityEngine;

public class UIFindPlayerClamLadder : MonoBehaviour
{
    private PlayerLadderMovement m_Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        m_Player = FindObjectOfType<PlayerLadderMovement>();
    }
    public void OnPointerDownClamUp()
    {
        m_Player.OnPointerDownClampUp();
    }
    public void OnPointerUpClampUp()
    {
        m_Player.OnPointerUpClampUp();
    }
    public void OnPointerDownClamDown()
    {
        m_Player.OnPointerDownClampDown();
    }
    public void OnPointerUpClamDown()
    {
        m_Player.OnPointerUpClampDown();
    }
}
