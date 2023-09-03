using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class MiniPerf_Script_AudioRPCManager_n1 : NetworkBehaviour
{
    public AudioSource audioSource;

    private bool musicStarted = false;
    //private bool stAuth = false;

    /// <summary>
    /// Function to be able to claim ownership
    /// for this object before sending RPCs
    /// </summary>
    public async void RequestStateAuthority()
    {
        await MiniPerf_Script_SharedAuthorityExtensions_n1.WaitForStateAuthority(Object);   // what does this line return ?
    }


    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_PlayAudioClip()
    {
        if (audioSource != null && !musicStarted)
        {
            audioSource.volume = 0.9f;
            audioSource.Play();
            musicStarted = true;
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_StopAudioClip()
    {
        if (audioSource != null && musicStarted)
        {
            audioSource.Stop();
            musicStarted = false;
        }
    }
}

