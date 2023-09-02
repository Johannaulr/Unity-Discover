using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PortalManager : NetworkBehaviour
{
    public GameObject portal;

    [Networked]
    private bool showPortal { get; set; }
    [Networked]
    private float targetCutoffValuePortal { get; set; }
    [SerializeField, Tooltip("The speed at which the variable X pendulums between 0 and 1."), Range(0f, 1f)]
    private float animationSpeedPortal = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void FixedUpdateNetwork()
    {

        float currentCutoffValuePortal = portal.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");

        //dissolveValueLocal = Mathf.PingPong(Time.time, 1);
        //Debug.Log("dissolveValueLocal is " + dissolveValueLocal);

        if (showPortal && currentCutoffValuePortal < targetCutoffValuePortal)
        {
            AnimatePortal(currentCutoffValuePortal);
        }

        if (!showPortal && currentCutoffValuePortal > targetCutoffValuePortal)
        {
            AnimatePortal(currentCutoffValuePortal);
        }

        else
        {
            return;
        }
    }

    private void AnimatePortal(float currentCutoffValuePortal)
    {

        if (Mathf.Abs(targetCutoffValuePortal) - Mathf.Abs(currentCutoffValuePortal) > 0.5f)
        {
            portal.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", Mathf.Lerp(currentCutoffValuePortal, targetCutoffValuePortal, animationSpeedPortal / 100));

        }
        else
        {
            portal.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", Mathf.Lerp(currentCutoffValuePortal, targetCutoffValuePortal, animationSpeedPortal / 50));
        }
    }

    public void ShowPortalButtonPressed()
    {
        if (!showPortal)
        {
            targetCutoffValuePortal = 1;
            showPortal = true;
        }
    }

    public void HidePortalButtonPressed()
    {
        if (showPortal)
        {
            targetCutoffValuePortal = -1;
            showPortal = false;
        }
    }
}
