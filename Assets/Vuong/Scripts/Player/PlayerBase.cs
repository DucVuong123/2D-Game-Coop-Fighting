using UnityEngine;
using PurrNet;
using System.Globalization;
public class PlayerBase : NetworkBehaviour
{
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        enabled = isOwner;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
       
}

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
