using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Script_ZoneControlleNW_Spawner : NetworkBehaviour
{

    public NetworkRunner nwRun;
    public NetworkObject ZoneControllerPrefab;

    private bool spawnedOnce;

    void Start()
    {
        spawnedOnce = false;
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (nwRun.GetPlayerUserId() != null && spawnedOnce == false)
        {
            nwRun.Spawn(ZoneControllerPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            MiniPerf_Script_SceneManager.instance.DebugLogMessage("Zone Controller Spawned");
            spawnedOnce = true;
        }
    }
}
