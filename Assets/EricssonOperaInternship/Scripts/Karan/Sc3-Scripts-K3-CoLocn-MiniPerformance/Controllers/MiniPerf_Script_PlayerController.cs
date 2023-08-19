using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerf_Script_PlayerController : MonoBehaviour
{
    private bool audioPlayFlag;
    private bool passThruDarkFlag;
    // Start is called before the first frame update
    void Start()
    {
        audioPlayFlag = false;
        passThruDarkFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            audioPlayFlag = !audioPlayFlag;

            if (audioPlayFlag)
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.PlayAudio);
            }
            else
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.StopAudio);
            }
        }

        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            passThruDarkFlag = !passThruDarkFlag;

            if (passThruDarkFlag)
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.DarkPassThru);
            }
            else
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes.NormalPassThru);
            }
        }

        if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            Script_TorusAnimation.startTorusAnimationFlag = true;
        }
    }
}
