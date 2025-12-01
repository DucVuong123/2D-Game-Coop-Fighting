using PurrNet;
using UnityEditor.PackageManager;
using UnityEngine;

public class TestNetwork : NetworkBehaviour
{
   [SerializeField] private GameObject boBinh_Prefab;
   [SerializeField] private GameObject duKich_Prefab;
   [SerializeField] private Transform spawn_Pos;
    [SerializeField] private bool isSpawnBoBinh;
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        GameObject spawnInstance = null;
        if (isSpawnBoBinh)
            spawnInstance= Instantiate(boBinh_Prefab, spawn_Pos.position, boBinh_Prefab.transform.rotation);
        else
            spawnInstance= Instantiate(duKich_Prefab, spawn_Pos.position, duKich_Prefab.transform.rotation);
        GiveOwnership(localPlayer);

    }
    private void Update()
    {


    }


}
