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
    StartSomething
}


public class MiniPerf_Script_SceneManager_n1 : MonoBehaviour
{
    //this line makes it Singleton
    public static MiniPerf_Script_SceneManager_n1 instance;


    public string networkSessionName = "XRminiPerformance";
    public MiniPerf_Script_NetworkManager_n1 mainNetworkManager;
    public MiniPerf_Script_AudioRPCManager audioRpcController;
    public MiniPerf_Script_PassThruRPCManager PassThruRPCController;

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

            case myRPCs.StartSomething:
                if (isHostInFusionServer)
                {
                    
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;

            default:
                break;
        }
    }

    public void DebugLogMessage(string text)
    {
        MiniPerf_Script_EventsManager.Send_OnLogMessage(text);
        Debug.Log(text);
    }


}
