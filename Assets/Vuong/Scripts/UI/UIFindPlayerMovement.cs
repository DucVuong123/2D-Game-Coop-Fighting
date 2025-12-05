using UnityEngine;
using PurrNet;
public class UIFindPlayerMovement : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is create
    private PlayerMovementBase m_Player;
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        Invoke(nameof(Delay_FindPlayer), 0.2f);

    }

    private void Delay_FindPlayer() => m_Player = FindObjectOfType<PlayerMovementBase>();


    public void OnPointerDownRight()
    {
        m_Player.OnPointerDownRight();
    }
    public void OnPointerDownLeft()
    {
        m_Player.OnPointerDownLeft();
    }
    public void OnPointerUpRight()
    {
        m_Player.OnPointerUpRight();
    }
    public void OnPointerUpLeft()
    {
        m_Player.OnPointerUpLeft();
    }

    public void OnPointerDownCheckRun()
    {
        m_Player.OnPointerDownCheckRun();
    }
    public void OnPointDownJump()
    {
        m_Player.OnPointDownJump();
    }
}
