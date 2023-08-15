using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using TMPro;

public class MusicPlayerObjectScript : NetworkBehaviour
{
    private string HostText;
    private bool MusicStartedFlag;
    PlayerJoinedObjectScript pjos;

    AudioSource audSo;
    public AudioClip TrackToPlay;

    public TMP_Text DebuggerTextObject;
    private void Awake()
    {
        pjos = FindObjectOfType<PlayerJoinedObjectScript>();
    }

    // Start is called before the first frame update
    void Start()
    {
        HostText = pjos.TextObjectPlayerType.text;
        MusicStartedFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (HostText == "You are HOST")
        {
            if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) && MusicStartedFlag == false)
            {
                RPC_PlayAudioClip();
                DebuggerTextObject.text = "Pressed Right Index Trigger";
            }
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_PlayAudioClip()
    {
        audSo = GetComponent<AudioSource>();
        audSo.PlayOneShot(TrackToPlay, 0.66f);
        MusicStartedFlag = true;
    }
}
