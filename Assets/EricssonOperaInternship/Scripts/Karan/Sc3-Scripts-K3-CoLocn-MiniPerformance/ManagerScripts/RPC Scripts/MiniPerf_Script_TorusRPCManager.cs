using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MiniPerf_Script_TorusRPCManager : NetworkBehaviour
{
    //public GameObject TorusNetworkObject;

    private void Awake()
    {
        //PassThruComponent = Object.GetComponent<OVRPassthroughLayer>();
    }

    private void Start()
    {
        //TorusNetworkObject.SetActive(false);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Function to be able to claim ownership
    /// for this object before sending RPCs
    /// </summary>
    public async void RequestStateAuthority()
    {
        await MiniPerf_Script_SharedAuthorityExtensions.WaitForStateAuthority(Object);   // what does this line return ?
    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_ShowTorus()
    {
        gameObject.SetActive(true);
    }
}

