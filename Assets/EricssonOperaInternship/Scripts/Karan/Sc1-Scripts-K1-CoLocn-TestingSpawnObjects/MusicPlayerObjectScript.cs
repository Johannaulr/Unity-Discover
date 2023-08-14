using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class MusicPlayerObjectScript : NetworkBehaviour
{
    private string HostText;
    private bool MusicStartedFlag;
    PlayerJoinedObjectScript pjos;

    AudioSource audSo;
    public AudioClip TrackToPlay;
    private void Awake()
    {
        pjos = FindObjectOfType<PlayerJoinedObjectScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HostText = pjos.TextObjectPlayerType.text;
        MusicStartedFlag = false;
        audSo = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (HostText == "You are HOST")
        {
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && MusicStartedFlag == false)
            {
                RPC_PlayAudioClip();
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_PlayAudioClip()
    {
        audSo.PlayOneShot(TrackToPlay, 1.0f);
        MusicStartedFlag = true;
    }
}
