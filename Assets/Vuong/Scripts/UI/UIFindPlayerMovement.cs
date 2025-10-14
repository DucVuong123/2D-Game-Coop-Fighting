using UnityEngine;

public class UIFindPlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private PlayerBase m_Player;
    private void Awake()
    {
        m_Player = FindObjectOfType<PlayerBase>();
    }
    public void OnPointerDownRight()
    {
        m_Player.player_Movement.OnPointerDownRight();
    }
    public void OnPointerDownLeft()
    {
        m_Player.player_Movement.OnPointerDownLeft();
    }
    public void OnPointerUpRight()
    {
        m_Player.player_Movement.OnPointerUpRight();
    }
    public void OnPointerUpLeft()
    {
        m_Player.player_Movement.OnPointerUpLeft();
    }

    public void OnPointerDownCheckRun()
    {
        m_Player.player_Movement.OnPointerDownCheckRun();
    }
    public void OnPointDownJump()
    {
        m_Player.player_Movement.OnPointDownJump();
    }
}
