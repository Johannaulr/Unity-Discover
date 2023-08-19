using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerf_Script_StartMenuController : MonoBehaviour
{
    public void ConnectAsHost()
    {
        MiniPerf_Script_SceneManager.instance.StartNetworkMiniPerf(isHost: true);
    }

    public void ConnectAsClient()
    {
        MiniPerf_Script_SceneManager.instance.StartNetworkMiniPerf();
    }
}



