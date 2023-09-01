using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ExperienceManager : NetworkBehaviour
{
    public GameObject portal;
    private bool showPortal;

    private float dissolveValueLocal;

    [Networked(OnChanged = nameof(ManagePortal))]
    private float dissolveValueNetworked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        dissolveValueNetworked = 0f;
        dissolveValueLocal = 0f;
    }

    public override void FixedUpdateNetwork()
    {


        dissolveValueLocal = Mathf.PingPong(Time.time, 1);
        //Debug.Log("dissolveValueLocal is " + dissolveValueLocal);

        if (showPortal)
        {
            //activate dissolve shader using dissolveValueNetworked
        }

        else
        {
            //deactivate dissolve shader using dissolveValueNetworked
        }
    }

    public void ShowPortalButtonPressed()
    {
        if (!showPortal)
        {
            showPortal = true;
        }
    }

    public void HidePortalButtonPressed()
    {
        if (showPortal)
        {
            showPortal = false;
        }
    }

    private void PortalShader()
    {

        if (showPortal)
        {
            //activate dissolve shader using dissolveValueLocal
        }

        else
        {
            //deactivate dissolve shader using dissolveValueLocal
        }
    }

    public static void ManagePortal(Changed<ExperienceManager> changeVariable)
    {
        changeVariable.Behaviour.dissolveValueLocal = changeVariable.Behaviour.dissolveValueNetworked;
        changeVariable.Behaviour.PortalShader();
    }
}
