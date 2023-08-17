using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class AudioRPCManager : NetworkBehaviour
{
    public AudioSource audioSource;

    private bool musicStarted = false;
    private bool stAuth = false;

    /// <summary>
    /// Function to be able to claim ownership
    /// for this object before sending RPCs
    /// </summary>
    public async void RequestStateAuthority()
    {
        await SharedAuthorityExtensions.WaitForStateAuthority(Object);   // what does this line return ?
    }


    // is the RequestStateAuthority() function being called ?
    // How to call the RequestStateAuthority() function ,
    // on or for this script from the HOST of the Session ?
    // Does it neeed to have a reference to the Runner of the Session ? 

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void RPC_PlayAudioClip()
    {
        //RequestStateAuthority();
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
