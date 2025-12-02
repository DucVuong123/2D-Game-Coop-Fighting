using PurrNet;
using PurrNet.Transports;
using UnityEditor.PackageManager;
using UnityEngine;

public class TestNetwork : NetworkBehaviour
{
   [SerializeField] private GameObject boBinh_Prefab;
   [SerializeField] private GameObject duKich_Prefab;
   [SerializeField] private Transform spawn_Pos;
    [SerializeField] private bool isSpawnBoBinh;
    private void Awake()
    {
       
    }
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;

        if(PlayerPrefs.GetInt("Character")==1)
            isSpawnBoBinh = true;
        else
            isSpawnBoBinh = false;

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
