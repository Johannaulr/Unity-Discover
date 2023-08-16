using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AudioRPCManager : NetworkBehaviour
{
    public AudioSource audioSource;

    private bool musicStarted = false;

    /// <summary>
    /// Function to be able to claim ownership
    /// for this object before sending RPCs
    /// </summary>
    public async void RequestStateAuthority()
    {
        _ = await SharedAuthorityExtensions.WaitForStateAuthority(Object);
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_PlayAudioClip()
    {
        if(audioSource != null && !musicStarted)
        {
            audioSource.volume = 0.66f;
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
