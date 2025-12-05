using UnityEngine;
using PurrNet;
public class UIFindPlayerClamLadder : NetworkBehaviour
{
    private PlayerLadderMovement m_Player;
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

    private void Find_Player() => m_Player = FindObjectOfType<PlayerLadderMovement>();

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
