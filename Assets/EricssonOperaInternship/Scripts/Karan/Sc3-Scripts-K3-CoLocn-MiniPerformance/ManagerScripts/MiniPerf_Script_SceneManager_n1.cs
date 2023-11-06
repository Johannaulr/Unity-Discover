using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Discover.Colocation.Test;

[Serializable]
public enum myRPCs
{
    PlayAudio,
    StopAudio,
    DarkPassThru,
    NormalPassThru,
    PassThruDimmer,
    PassThruBrighter
}


public class MiniPerf_Script_SceneManager_n1 : MonoBehaviour
{
    //this line makes it Singleton
    public static MiniPerf_Script_SceneManager_n1 instance;


    public string networkSessionName = "XRminiPerformance";
    public MiniPerf_Script_NetworkManager_n1 mainNetworkManager;
    public MiniPerf_Script_AudioRPCManager_n1 audioRpcController;
    public MiniPerf_Script_PassThruRPCManager_n1 PassThruRPCController;

    private bool isHostInFusionServer = false;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }


    public void StartNetworkMiniPerf(bool isHost = false)
    {
        // Connect as host or client
        mainNetworkManager.StartConnection(isHost);
    }

    public void SetHostInFusionServer(bool isHost)
    {
        isHostInFusionServer = isHost;

        if (isHostInFusionServer)
        {
            audioRpcController.RequestStateAuthority();
            PassThruRPCController.RequestStateAuthority();
        }
    }

    public void SendRPC(myRPCs rpcType)
    {
        switch (rpcType)
        {
            case myRPCs.PlayAudio:
                if (isHostInFusionServer)
                {
                    audioRpcController.RPC_PlayAudioClip();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;
            
            case myRPCs.StopAudio:
                if (isHostInFusionServer)
                {
                    audioRpcController.RPC_StopAudioClip();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;
            
            case myRPCs.DarkPassThru:
                if (isHostInFusionServer)
                {
                    PassThruRPCController.RPC_PassThruDark();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;
            
            case myRPCs.NormalPassThru:
                if (isHostInFusionServer)
                {
                    PassThruRPCController.RPC_PassThruNormal();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;

            case myRPCs.PassThruDimmer:
                if (isHostInFusionServer)
                {
                    float valSend = 1.0f;
                    float valDecrease = 0.95f / 7.0f;
                    for (var k = 0; k < 8; k++)
                    {
                        PassThruRPCController.RPC_PassThruDimmer(valSend);
                        StartCoroutine(CoroutinePassthruDelay(3));
                        valSend -= valDecrease;
                    }
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;

            case myRPCs.PassThruBrighter:
                if (isHostInFusionServer)
                {
                    float valSend = 0.05f;
                    float valIncrease = .95f / 7.0f;
                    for (var k = 0; k < 8; k++)
                    {
                        PassThruRPCController.RPC_PassThruBrighter(valSend);
                        StartCoroutine(CoroutinePassthruDelay(3));
                        valSend += valIncrease;
                    }
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;

            default:
                break;
        }
    }

    IEnumerator CoroutinePassthruDelay(float delayRecv)
    {
        //yield on a new YieldInstruction that waits for delay received seconds.
        yield return new WaitForSeconds(delayRecv);
    }

    public void DebugLogMessage(string text)
    {
        MiniPerf_Script_EventsManager.Send_OnLogMessage(text);
        Debug.Log(text);
    }


}
