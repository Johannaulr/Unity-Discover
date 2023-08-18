using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerf_Script_PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.RawButton.A))
        {
            MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.PlayAudio);
        }
    }
}
