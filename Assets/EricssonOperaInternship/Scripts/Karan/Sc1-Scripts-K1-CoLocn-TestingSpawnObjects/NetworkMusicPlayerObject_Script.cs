using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;
using Fusion.Sockets;
using TMPro;

public class NetworkMusicPlayerObject_Script : NetworkBehaviour
{
    
    private TMP_Text DebuggerTextObject;

    [SerializeField]
    private NetworkPrefabRef SpherePrefabToSpawn;

    //public UnityEvent TriggerAudioStart;
    //public UnityEvent TriggerAudioStop;

    public AudioPlayerObject_New_Script audioObject;

    private bool playMusicFlag;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        playMusicFlag = true;
        DebuggerTextObject = GameObject.Find("DebuggerTextObj").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            //DebuggerTextObject.text = "Pressed A";

            if (playMusicFlag)
            {
                //TriggerAudioStart.Invoke();

                RPC_PlayAudio();
                playMusicFlag = false;
            }
            
        }

        if (OVRInput.GetDown(OVRInput.RawButton.B))
        {
            //DebuggerTextObject.text = "Pressed A";

            //TriggerAudioStop.Invoke();


            
                RPC_StopAudio();
                playMusicFlag = true;
            
        }

    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority)]
    private void RPC_PlayAudio()
    {
        //TriggerAudioStart.Invoke();
        //DebuggerTextObject.text = "RPC called";
        //audioObject.PlayAudioTrack();

        Runner.Spawn(SpherePrefabToSpawn, position: new Vector3(2.0f, 1.5f, 2.0f), Quaternion.identity);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority)]
    private void RPC_StopAudio()
    {
        //TriggerAudioStop.Invoke();

        audioObject.StopAudioTrack();
    }

}
