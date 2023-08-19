using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_TorusAnimation : MonoBehaviour
{
    public static bool startTorusAnimationFlag;
    private float PosY;
    private float yMoveStepSize;
    private float PosYFinal;
    // Start is called before the first frame update
    void Start()
    {
        startTorusAnimationFlag = false;
        PosY = transform.position.y;
        yMoveStepSize = 0.5f;
        PosYFinal = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTorusAnimationFlag)
        {
            MiniPerf_Script_SceneManager.instance.DebugLogMessage($"Torus Anim Start");
            StartTorusAnimationFn();
        }
    }

    void StartTorusAnimationFn()
    {
        if(PosY <= PosYFinal)
        {
            PosY += Time.deltaTime * yMoveStepSize;
        }
    }
}
