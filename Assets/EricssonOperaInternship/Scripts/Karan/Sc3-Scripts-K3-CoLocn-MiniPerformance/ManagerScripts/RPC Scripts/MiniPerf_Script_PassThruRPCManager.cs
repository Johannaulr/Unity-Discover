using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MiniPerf_Script_PassThruRPCManager : NetworkBehaviour
{
    public GameObject ovrCameraRig;
    private OVRPassthroughLayer PassThruComponent;

    private void Awake()
    {
        PassThruComponent = ovrCameraRig.GetComponent<OVRPassthroughLayer>();
    }

    //private bool musicStarted = false;
    //private bool stAuth = false;

    /// <summary>
    /// Function to be able to claim ownership
    /// for this object before sending RPCs
    /// </summary>
    public async void RequestStateAuthority()
    {
        await MiniPerf_Script_SharedAuthorityExtensions.WaitForStateAuthority(Object);   // what does this line return ?
    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_PassThruDark()
    {
        PassThruComponent.textureOpacity = 0.05f;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_PassThruNormal()
    {
        PassThruComponent.textureOpacity = 1.0f;
    }
}

