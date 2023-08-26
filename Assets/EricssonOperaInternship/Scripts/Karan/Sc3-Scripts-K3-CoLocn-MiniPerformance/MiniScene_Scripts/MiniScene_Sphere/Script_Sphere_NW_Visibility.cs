using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class Script_Sphere_NW_Visibility : NetworkBehaviour
{
    private bool IsVisible;

    [Networked(OnChanged = nameof(ShowHideFn))]
    public NetworkBool ShowSphereNW { get; set; }

    public override void FixedUpdateNetwork()
    {
        base.FixedUpdateNetwork();

        if (OVRInput.GetUp(OVRInput.RawButton.X) && Runner.IsSharedModeMasterClient)
        {
            ShowSphereNW = !ShowSphereNW;
        }
    }

    public void changeVisibility()
    {
        gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = IsVisible;
    }


    public static void ShowHideFn(Changed<Script_Sphere_NW_Visibility> changeVariable)
    {
        changeVariable.Behaviour.IsVisible = changeVariable.Behaviour.ShowSphereNW;

        changeVariable.Behaviour.changeVisibility();
    }

}
