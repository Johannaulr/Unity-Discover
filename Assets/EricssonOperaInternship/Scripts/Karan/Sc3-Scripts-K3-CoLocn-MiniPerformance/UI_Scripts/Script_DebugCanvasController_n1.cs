using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_DebugCanvasController_n1 : MonoBehaviour
{

    public GameObject DebugUICanvas;
    private bool showDebugUI;
    // Start is called before the first frame update
    void Start()
    {
        showDebugUI = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            showDebugUI = !showDebugUI;
            CheckDebugUIFunction(showDebugUI);
        }
    }

    private void CheckDebugUIFunction(bool showUIFlag)
    {
        if (showUIFlag)
        {
            DebugUICanvas.SetActive(true);
        }
        else
        {
            DebugUICanvas.SetActive(false);
        }
    }
}

