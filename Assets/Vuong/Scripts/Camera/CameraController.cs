using UnityEngine;

public class CameraController : MonoBehaviour
{
    private PlayerBase player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }
    void Start()
    {
        player = FindObjectOfType<PlayerBase>();
    }

    // Update is called once per frame
    void Update()
    {
        CameraFolowPlayer();
    }
    private void CameraFolowPlayer()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
    }    
}
