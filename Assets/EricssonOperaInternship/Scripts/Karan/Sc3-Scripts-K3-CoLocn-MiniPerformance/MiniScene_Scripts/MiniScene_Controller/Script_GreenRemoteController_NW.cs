using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class Script_GreenRemoteController_NW : NetworkBehaviour
{
    public GameObject sceneCamera;
    
    public NetworkObject remoteBall;

    private Vector3 originalPosition;
    private Vector3 floatingPosition;
    private Vector3 onTablePosition;

    private Quaternion originalRotation;

    private int prevState;  // 1 for in, 0 for out
    private int currentState;  // 1 for in, 0 for out

    [SerializeField]
    private bool InZoneFlag;

    private Vector3 scaleChange;

    public bool remoteStateAuthFlag;

    private bool ViewerPanelFlag;

    [Networked(OnChanged = nameof(ViewerPanelNWfunc))]
    public NetworkBool ViewerPanelFlagNW { get; set; }

    private int RemoteTakenInt;

    [Networked(OnChanged = nameof(ViewerPanelNWfunc))]
    public int RemoteTakenIntNW { get; set; }

    private int localInt;

    [Networked(OnChanged = nameof(ChangeLocalIntFunc))]
    public int IntNW { get; set; }

    private bool localBool;

    [Networked(OnChanged = nameof(ChangeLocalBoolFunc))]
    public NetworkBool boolNW { get; set; }

    [Networked]
    public float floatNW { get; set; }

    private void Awake()
    {
        sceneCamera = GameObject.Find("CenterEyeAnchor");
        remoteBall = GameObject.Find("Remote-Controller-NW").GetComponent<NetworkObject>();
        //remoteBall = GameObject.Find("Remote-Controller-NW-PSX").GetComponent<NetworkObject>();
    }

    private void Start()
    {
        originalPosition = new Vector3(-1.5f, 0, 1.5f);
        onTablePosition = new Vector3(-1.5f, .9f, 1.5f);
        floatingPosition = new Vector3(-1.5f, 1.35f, 1.5f);

        remoteBall.transform.position = onTablePosition;

        prevState = 0;
        currentState = 1;

        //targetPosition = new Vector3(0, 1.7f, 0);
        //targetRotation = Quaternion.identity;

        scaleChange = new Vector3(-0.01f, -0.01f, -0.01f);

        remoteStateAuthFlag = false;

        ViewerPanelFlagNW = false;
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        prevState = currentState;

        CheckInZoneFunc(sceneCamera);

        if (InZoneFlag == true && Runner.IsSharedModeMasterClient)
        {
            currentState = 1;
            ForDebuggerFunc(currentState, prevState, "In Zone"); // current 1, prev 0

            remoteBall.RequestStateAuthority();

            if (remoteBall.HasStateAuthority)
            {
                if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0)
                {
                    getRCfunc();
                    ForDebuggerFunc(currentState, prevState, "In Zone, Got Remote.");
                }
                else
                {
                    RCfuncInZoneFloatingPosition();
                }
            }
        }

        else
        {
            RCfuncOnTablePosition();
            currentState = 0;
            ForDebuggerFunc(currentState, prevState, "Out of Zone, Released RemoteBall."); // current 0, prev 1             
        }
    }

    void getRCfunc()
    {
        RemoteTakenIntNW = 1;
        remoteBall.transform.position = OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch) + new Vector3(0.02f, 0, 0.14f);

        /*if (remoteBall.transform.localScale.y > 0.02f && remoteBall.transform.localScale.y <= 0.1f)
        {
            remoteBall.transform.localScale -= scaleChange;
        }*/
        //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.LTouch);
    }

    void RCfuncInZoneFloatingPosition()
    {
        remoteBall.transform.position = floatingPosition;
    }

    void RCfuncOnTablePosition()
    {
        RemoteTakenIntNW = 0;
        remoteBall.transform.position = onTablePosition;
        remoteBall.ReleaseStateAuthority();
    }


    private void CheckInZoneFunc(GameObject cam)
    {
        float xPosMax = -0.2f;
        float xPosMin = -4.0f;
        float zPosMax = 4.0f;
        float zPosMin = 0.2f;

        float camPosX = cam.transform.position.x;
        float camPosZ = cam.transform.position.z;

        bool internalBool;

        if (camPosX > xPosMin && camPosX < xPosMax && camPosZ > zPosMin && camPosZ < zPosMax)
        {
            InZoneFlag = true;
        }
        else
        {
            InZoneFlag = false;
        }
    }

    private void ForDebuggerFunc(int current, int past, string msg)
    {
        if (current != past)
        {
            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage(msg);
        }
    }


    private void viewerPanelShowHideFunc(bool flagRecv)
    {
        if (flagRecv)
        {
            //ViewerPanelObj.SetActive(true);
        }
        else
        {
            //ViewerPanelObj.SetActive(false);
        }
    }


    private static void ViewerPanelNWfunc(Changed<Script_GreenRemoteController_NW> changedVariable)
    {
        changedVariable.Behaviour.ViewerPanelFlag = changedVariable.Behaviour.ViewerPanelFlagNW;

        //changedVariable.Behaviour.viewerPanelShowHideFunc(changedVariable.Behaviour.ViewerPanelFlag);
    }

    private static void RemoteTakenNWfunc(Changed<Script_GreenRemoteController_NW> changedVariable)
    {
        changedVariable.Behaviour.RemoteTakenInt = changedVariable.Behaviour.RemoteTakenIntNW;
    }


    private static void ChangeLocalIntFunc(Changed<Script_GreenRemoteController_NW> changedVariable)
    {
        changedVariable.Behaviour.localInt = changedVariable.Behaviour.IntNW;
    }

    private static void ChangeLocalBoolFunc(Changed<Script_GreenRemoteController_NW> changedVariable)
    {
        changedVariable.Behaviour.localBool = changedVariable.Behaviour.boolNW;
    }

}


