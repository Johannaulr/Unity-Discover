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
    SpawnCube,
    DestroyCube
}


public class MiniPerf_Script_SceneManager : MonoBehaviour
{
    //this line makes it Singleton
    public static MiniPerf_Script_SceneManager instance;


    public string networkSessionName = "XRminiPerformance";
    public MiniPerf_Script_NetworkManager mainNetworkManager;
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
            
            case MiniPerfRPCtypes.SpawnCube:
                break;
            
            case MiniPerfRPCtypes.DestroyCube:
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
