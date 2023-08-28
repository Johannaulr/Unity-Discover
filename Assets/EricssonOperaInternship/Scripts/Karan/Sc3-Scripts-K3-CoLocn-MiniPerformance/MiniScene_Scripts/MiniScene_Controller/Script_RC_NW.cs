using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Script_RC_NW : NetworkBehaviour
{
    //public NetworkObject VideoBoxPrefab;
    public GameObject sceneCamera;
    private bool VideoBoxSpawnedFlag;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        VideoBoxSpawnedFlag = false;
        originalPosition = transform.position;
        targetPosition = new Vector3(0, 1.7f, 0);
        targetRotation = Quaternion.identity;
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0)
        {
            getRC();

            if (OVRInput.GetUp(OVRInput.RawButton.X) && VideoBoxSpawnedFlag == false)
            {
                //Debug.Log("Sphere Spawn Please");
                targetPosition = sceneCamera.transform.position;
                targetRotation = sceneCamera.transform.rotation;

                //MiniPerf_Script_SceneManager.instance.DebugLogMessage("Spawn VideoBox");

                //Runner.Spawn(VideoBoxPrefab, position: targetPosition, rotation: targetRotation);
                VideoBoxSpawnedFlag = true;
            }
        }
        else 
        {
            releaseRC();
        }
    }

    void getRC()
    {
        // Assign Left controller's position and rotation to Remote Control Object
        transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) + new Vector3(0.03f, 0, 0.08f);
        //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
    }

    void releaseRC()
    {
        transform.position = originalPosition;
    }
}
