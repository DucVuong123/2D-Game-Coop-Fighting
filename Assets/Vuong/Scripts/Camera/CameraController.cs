using System.Linq;
using UnityEngine;
using PurrNet;
public class CameraController : NetworkBehaviour
{
    public PlayerBase[] player = new PlayerBase[10];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        
    }
   protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        Invoke(nameof(DelayFindPlayer), 1f);
        networkManager.onNetworkStarted += NetworkManager_onNetworkStarted;
      
    }

    private void NetworkManager_onNetworkStarted(NetworkManager manager, bool asServer)
    {
        DelayFindPlayer();
    }

    private void DelayFindPlayer()
    {
        player = FindObjectsOfType<PlayerBase>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CameraFolowPlayer();
    }
    private void CameraFolowPlayer()
    {
        if (player[0] != null && player[1] != null)
        {
            Vector2 pos = (player[0].transform.position + player[1].transform.position) / 2f;
            transform.position = new Vector3(pos.x, pos.y, -10);
        }
        else if (player[0] == null  || player[1] == null)
        {
            if (player[0]!=null)
                transform.position = new Vector3(player[0].transform.position.x, player[0].transform.position.y, -10);
            else if(player[1] != null)
                transform.position = new Vector3(player[1].transform.position.x, player[1].transform.position.y, -10);
        }
    }    
}
