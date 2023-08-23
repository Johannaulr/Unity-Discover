using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerf_Script_PlayerController : MonoBehaviour
{
    private bool audioPlayFlag;
    private bool passThruDarkFlag;
    private bool torusFlag;
    private bool torusActivationFlag;

    public GameObject cubeContainer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayFlag = false;
        passThruDarkFlag = false;
        torusFlag = false;
        torusActivationFlag = true;
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

            if (torusActivationFlag)
            {
                torusFlag = true;
                torusActivationFlag = false;
            }

            if (torusFlag)
            {
                cubeContainer.SetActive(true);

                Script_CubeAnimation childScript = cubeContainer.GetComponentInChildren<Script_CubeAnimation>();

                if (childScript != null)
                {
                    childScript.SetAnimFlag(1);
                    MiniPerf_Script_SceneManager.instance.DebugLogMessage($"Anim PLEASE Start");
                }
                else
                {
                    Debug.LogWarning("ChildScript not found on any child GameObject.");
                }
            }
        }
    }
}
