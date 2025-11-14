using PurrNet;
using UnityEngine;

public class TestNetwork : NetworkBehaviour
{
   [SerializeField] private Color color;
   [SerializeField] private SyncVar<int> Health = new();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            SetColor(color);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            TakeDamge();
        }
    }
    [ObserversRpc(bufferLast:(true))]
    private void SetColor(Color color)
    {
        GetComponent<SpriteRenderer>().color =color;
    }
   
    private void TakeDamge()
    {
        Health.value -= 20;
    }
}
