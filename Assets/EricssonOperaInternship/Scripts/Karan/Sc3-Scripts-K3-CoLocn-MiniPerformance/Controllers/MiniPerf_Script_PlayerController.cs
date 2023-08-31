using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniPerf_Script_PlayerController : MonoBehaviour
{
    private bool audioPlayFlag;
    private bool passThruDarkFlag;

    private bool profileUIVisibleFlag;

    //public GameObject ProfileMenuObject;
    //public GameObject ExperimentDescriptionObject;
    public GameObject CameraObject;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayFlag = false;
        passThruDarkFlag = false;
        profileUIVisibleFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.A))
        {
            audioPlayFlag = !audioPlayFlag;

            if (audioPlayFlag)
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes_n1.PlayAudio);
            }
            else
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes_n1.StopAudio);
            }
        }

        if (OVRInput.GetUp(OVRInput.RawButton.B))
        {
            passThruDarkFlag = !passThruDarkFlag;

            if (passThruDarkFlag)
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes_n1.DarkPassThru);
            }
            else
            {
                MiniPerf_Script_SceneManager.instance.SendRPC(MiniPerfRPCtypes_n1.NormalPassThru);
            }
        }

        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0)
        {
            //profileUIVisibleFlag = true;
            //ProfileMenuObject.SetActive(profileUIVisibleFlag);
            //ExperimentDescriptionObject.SetActive(profileUIVisibleFlag);
            //ProfileMenuObject.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) + new Vector3(0.2f, 0, 0.15f);
            //ProfileMenuObject.transform.position = CameraObject.transform.position + new Vector3(0, -0.25f, .8f);
            //ExperimentDescriptionObject.transform.position = CameraObject.transform.position + new Vector3(0, 0.051f, 0.8f);

        }
        else if(OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) == 0)
        {
            //profileUIVisibleFlag = false;
            //ProfileMenuObject.SetActive(profileUIVisibleFlag);
            //ExperimentDescriptionObject.SetActive(profileUIVisibleFlag);
        }
    }
}
