using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using TMPro;

public class Script_ZoneHandler : NetworkBehaviour
{

    public TMP_Text hudUItext;

    private int myLocalInt;

    [Networked(OnChanged = nameof(ChangeLocalInt))]
    public int IntNW { get; set; }

    private bool myLocalBool;

    [Networked(OnChanged = nameof(ChangeLocalBool))]
    public NetworkBool boolNW { get; set; }

    private void Awake()
    {
        hudUItext = GameObject.Find("HUD_Text_Object").GetComponent<TextMeshProUGUI>();
    }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (GetInput(out NetworkInputData nwInputData))
        {
            /*if (nwInputData.nidBool)
            {
                boolNW = nwInputData.nidBool;
                
                hudUItext.text = "NW int: " + myLocalInt + ", NW bool: " + myLocalBool;
            }*/
        }
        else
        {
            hudUItext.text = "No input";
        }
    }

    private static void ChangeLocalInt(Changed<Script_ZoneHandler> changedVariable)
    {
        changedVariable.Behaviour.myLocalInt = changedVariable.Behaviour.IntNW;
    }

    private static void ChangeLocalBool(Changed<Script_ZoneHandler> changedVariable)
    {
        changedVariable.Behaviour.myLocalBool = changedVariable.Behaviour.boolNW;
    }
}
