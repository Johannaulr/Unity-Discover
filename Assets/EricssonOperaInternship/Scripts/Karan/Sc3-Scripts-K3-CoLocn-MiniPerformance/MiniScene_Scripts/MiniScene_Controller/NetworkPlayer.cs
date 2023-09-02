using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using TMPro;

public class NetworkPlayer : NetworkBehaviour
{

    public TMP_Text debugNWinputsText;

    [Networked]
    public float floatNW { get; set; }


    [Networked]
    public ref NetworkInputData NetworkedStructRef => ref MakeRef<NetworkInputData>();

    private void Awake()
    {
        debugNWinputsText = GameObject.Find("DebuggerNWinputs-Object").GetComponent<TextMeshProUGUI>();
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        

        //debugNWinputsText.text = " NW int: " + NetworkedStructRef.nidInt;

        
        if (GetInput(out NetworkInputData NetworkedStructRef))
        {
            debugNWinputsText.text = " NW int: " + NetworkedStructRef.nidInt;
            Debug.Log("Got input from NetworkInputData");
        }
        
    }
}
