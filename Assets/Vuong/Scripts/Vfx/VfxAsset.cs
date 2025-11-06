using UnityEngine;

public class VfxAsset : MonoBehaviour
{
    public static VfxAsset Instance;
    [Header("Vfx")]
    public GameObject grenade_Explo;
    public GameObject blood_Hit;
    public GameObject bo_Binh_Skill;
   
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void SpawnVfx(GameObject vfx, Vector2 pos, float TimeDestroy, Transform parent)
    {
        GameObject vfxInstance = Instantiate(vfx, pos, vfx.transform.rotation);
        if (parent != null)
            vfxInstance.transform.SetParent(parent);
        Destroy(vfxInstance, TimeDestroy);

    }
    public GameObject SpawnVfx(GameObject vfx, Vector2 pos, float TimeDestroy = 1, bool isDestroy = true)
    {
        GameObject vfxInstance = Instantiate(vfx, pos, vfx.transform.rotation);
        if (isDestroy)
            Destroy(vfxInstance, TimeDestroy);
        return vfxInstance;

    }
    public void SpawnVfx(GameObject vfx, Vector2 pos, float TimeDestroy)
    {
        GameObject vfxInstance = Instantiate(vfx, pos, vfx.transform.rotation);
        Destroy(vfxInstance, TimeDestroy);

    }
}
