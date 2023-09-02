using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class Script_ZoneControllerNW_Spawner : NetworkBehaviour
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
            nwRun.Spawn(ZoneControllerPrefab, Vector3.zero, Quaternion.identity);
            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Zone Controller Spawned... YAAAAY");
            spawnedOnce = true;
        }
    }
}
