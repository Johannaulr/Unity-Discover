using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TorusAnimation : MonoBehaviour
{
    public static bool startTorusAnimationFlag;
    public static bool DisplayedOnDebuggerFlag;
    private float PosY;
    private float yMoveStepSize;
    private float PosYFinal;
    // Start is called before the first frame update
    void Start()
    {
        startTorusAnimationFlag = false;
        DisplayedOnDebuggerFlag = false;
        PosY = transform.position.y;
        yMoveStepSize = 0.4f;
        PosYFinal = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTorusAnimationFlag && PosY <= PosYFinal)
        {
            StartTorusAnimationFn();
        }

        if (DisplayedOnDebuggerFlag)
        {
            MiniPerf_Script_SceneManager.instance.DebugLogMessage($"Torus Anim Start");
            DisplayedOnDebuggerFlag = false;
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, PosY, transform.position.z);
    }

    void StartTorusAnimationFn()
    {
         PosY += Time.deltaTime * yMoveStepSize;
    }
}
