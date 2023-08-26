using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Script_sphere_spawn_manager_nw : NetworkBehaviour
{
    public NetworkObject Sphere_NW_Prefab;
    private bool SphereSpawnedFlag;

    // Start is called before the first frame update
    void Start()
    {
        SphereSpawnedFlag = false;
    }


    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (OVRInput.GetUp(OVRInput.RawButton.Y) && SphereSpawnedFlag == false && Runner.IsSharedModeMasterClient)
        {
            //Debug.Log("Sphere Spawn Please");
            MiniPerf_Script_SceneManager.instance.DebugLogMessage("Spawn NW Sphere");
            Runner.Spawn(Sphere_NW_Prefab, position: new Vector3(0, 1.5f, 0), rotation: Quaternion.identity);
            SphereSpawnedFlag = true;
        }
    }
}

