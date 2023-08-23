using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Script_CubeAnimation : NetworkBehaviour
{
    private bool startCubeAnimationFlag;

    public bool DisplayedOnDebuggerFlag;
    private float PosY;
    private float yMoveStepSize;
    private float PosYFinal;


    [Networked(OnChanged = nameof(NetworkAnimationFlagChanged))]
    public NetworkBool NetworkedAnimationStartFlag { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        startCubeAnimationFlag = false;
        NetworkedAnimationStartFlag = false;
        DisplayedOnDebuggerFlag = false;
        PosY = transform.position.y;
        yMoveStepSize = 0.8f;
        PosYFinal = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {

        if (DisplayedOnDebuggerFlag)
        {
            MiniPerf_Script_SceneManager.instance.DebugLogMessage($"Anim Start");
            DisplayedOnDebuggerFlag = false;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (startCubeAnimationFlag && PosY <= PosYFinal)
        {
            StartAnimationFn();

            transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
        }
    }

    private static void NetworkAnimationFlagChanged(Changed<Script_CubeAnimation> changed)
    {
        changed.Behaviour.startCubeAnimationFlag = changed.Behaviour.NetworkedAnimationStartFlag;

        MiniPerf_Script_SceneManager.instance.DebugLogMessage($"startCubeAnimationFlag : {changed.Behaviour.startCubeAnimationFlag}");
    }

    void StartAnimationFn()
    {
        PosY += Time.deltaTime * yMoveStepSize;
    }

    public void SetAnimFlag(int val) 
    {
        if (val == 1) 
        {
            NetworkedAnimationStartFlag = true;
        }

        MiniPerf_Script_SceneManager.instance.DebugLogMessage($"Player Sent Val : {val}");
    }
}
