using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager_Object_Script : MonoBehaviour
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
        if (OVRInput.GetUp(OVRInput.RawButton.X))
        {
            showDebugUI = !showDebugUI;
            CheckDebugUIFunction(showDebugUI);
        }
    }

    private void CheckDebugUIFunction(bool showValue)
    {
        if (showValue)
        {
            DebugUICanvas.SetActive(true);
        }
        else
        {
            DebugUICanvas.SetActive(false);
        }
    }
}
