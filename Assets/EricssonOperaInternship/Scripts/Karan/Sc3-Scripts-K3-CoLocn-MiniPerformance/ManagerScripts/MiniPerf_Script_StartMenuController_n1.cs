using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerf_Script_StartMenuController_n1 : MonoBehaviour
{
    public void ConnectAsHost()
    {
        MiniPerf_Script_SceneManager_n1.instance.StartNetworkMiniPerf(isHost: true);
    }

    public void ConnectAsClient()
    {
        MiniPerf_Script_SceneManager_n1.instance.StartNetworkMiniPerf();
    }
}



