using PurrNet;

using UnityEngine;
using UnityEngine.UI;

public class TestNetwork : NetworkBehaviour
{
   [SerializeField] private GameObject boBinh_Prefab;
   [SerializeField] private GameObject duKich_Prefab;
   [SerializeField] private Transform spawn_Pos;
    [SerializeField] private bool isSpawnBoBinh;
    [SerializeField] private Text textDebug;
    private void Awake()
    {
       
    }


    protected override void OnSpawned()
    {
        base.OnSpawned();
        textDebug.text = "Conected";
        if (PlayerPrefs.GetInt("Character")==1)
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
