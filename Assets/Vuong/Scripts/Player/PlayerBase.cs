using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public PlayerMovementBase player_Movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        player_Movement = GetComponent<PlayerMovementBase>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
