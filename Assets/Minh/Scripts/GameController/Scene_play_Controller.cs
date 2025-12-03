using UnityEngine;
using PurrNet;
public class Scene_play_Controller : NetworkBehaviour
{
    protected override void OnSpawned(bool asServer)
    {
        base.OnSpawned(asServer);
        if (asServer)
            return;
        if(isServer)
        GameController.Instance.spawm_Player_Map("Map");

        GameController.Instance.spawm_Player_Map("Player");
        GiveOwnership(localPlayer);

    }
}
