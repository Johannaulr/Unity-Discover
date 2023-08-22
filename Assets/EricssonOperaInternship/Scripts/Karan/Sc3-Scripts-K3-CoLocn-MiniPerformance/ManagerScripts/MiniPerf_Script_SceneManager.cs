using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Discover.Colocation.Test;

[Serializable]
public enum MiniPerfRPCtypes
{
    PlayAudio,
    StopAudio,
    DarkPassThru,
    NormalPassThru,
    StartTorus
}


public class MiniPerf_Script_SceneManager : MonoBehaviour
{
    //this line makes it Singleton
    public static MiniPerf_Script_SceneManager instance;


    public string networkSessionName = "XRminiPerformance";
    public MiniPerf_Script_NetworkManager mainNetworkManager;
    public MiniPerf_Script_AudioRPCManager audioRpcController;
    public MiniPerf_Script_PassThruRPCManager PassThruRPCController;

    public MiniPerf_Script_TorusRPCManager TorusRPCController;

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
            TorusRPCController.RequestStateAuthority();
        }
    }

    public void SendRPC(MiniPerfRPCtypes rpcType)
    {
        switch (rpcType)
        {
            case MiniPerfRPCtypes.PlayAudio:
                if (isHostInFusionServer)
                {
                    audioRpcController.RPC_PlayAudioClip();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;
            
            case MiniPerfRPCtypes.StopAudio:
                if (isHostInFusionServer)
                {
                    audioRpcController.RPC_StopAudioClip();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;
            
            case MiniPerfRPCtypes.DarkPassThru:
                if (isHostInFusionServer)
                {
                    PassThruRPCController.RPC_PassThruDark();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;
            
            case MiniPerfRPCtypes.NormalPassThru:
                if (isHostInFusionServer)
                {
                    PassThruRPCController.RPC_PassThruNormal();
                    DebugLogMessage($"Host triggered RPC {rpcType}");
                }
                break;

            case MiniPerfRPCtypes.StartTorus:
                if (isHostInFusionServer)
                {
                    TorusRPCController.RPC_ShowTorus();
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
