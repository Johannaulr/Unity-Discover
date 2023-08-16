using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;
using Fusion.Sockets;
using TMPro;

public class NetworkMusicPlayerObject_Script : NetworkBehaviour
{
    GameObject DebuggerTextObjScene;
    private TMP_Text DebuggerTextObjectTMPcomp;

    [SerializeField]
    private NetworkPrefabRef SpherePrefabToSpawn;

    public UnityEvent TriggerAudioStart;
    public UnityEvent TriggerAudioStop;

    //public AudioPlayerObject_New_Script audioObject;

    private bool playMusicFlag;

    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        if (Runner.IsPlayer && Runner.LocalPlayer != null)
        {
            DebuggerTextObjScene = GameObject.Find("DebuggerTextObj");

            // Check if the object was found
            if (DebuggerTextObjScene != null)
            {
                // Try to get the TextMeshProUGUI component
                DebuggerTextObjectTMPcomp = DebuggerTextObjScene.GetComponent<TextMeshProUGUI>();

                // Check if the component was found
                if (DebuggerTextObjectTMPcomp != null)
                {
                    // Now you can use DebuggerTextObject
                    DebuggerTextObjectTMPcomp.text = "Found and accessed!";
                }
                else
                {
                    Debug.LogError("TextMeshProUGUI component not found on the DebuggerTextObj.");
                }
            }
            else
            {
                Debug.LogError("DebuggerTextObjScene not found in the scene.");
            }
        }
        playMusicFlag = true;
        //DebuggerTextObject = GameObject.Find("DebuggerTextObj").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (!Runner.IsSharedModeMasterClient)
            return;

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

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_PlayAudio()
    {
        TriggerAudioStart.Invoke();
        DebuggerTextObjectTMPcomp.text = "RPC Call Play Music";
        //audioObject.PlayAudioTrack();

        //Runner.Spawn(SpherePrefabToSpawn, position: new Vector3(2.0f, 1.5f, 2.0f), Quaternion.identity);
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_StopAudio()
    {
        TriggerAudioStop.Invoke();
        DebuggerTextObjectTMPcomp.text = "RPC Call STOP";
        //audioObject.StopAudioTrack();
    }

}
