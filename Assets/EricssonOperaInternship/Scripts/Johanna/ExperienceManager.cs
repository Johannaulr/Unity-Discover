using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PortalManager : NetworkBehaviour
{
	public GameObject portal;
	public GameObject portalFrame;

    [Networked]
	private bool showPortalDoor { get; set; }
    [Networked]
	private float targetCutoffValuePortal { get; set; }
	[Networked]
	private float targetCutoffValueFrame { get; set; }
	
    [SerializeField, Tooltip("The speed at which the variable X pendulums between 0 and 1."), Range(0f, 1f)]
    private float animationSpeedPortal = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void FixedUpdateNetwork()
    {
        if (Runner.IsConnectedToServer) {
	        float currentCutoffValuePortal = portal.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");
	        float currentCutoffValueFrame = portalFrame.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");

            //dissolveValueLocal = Mathf.PingPong(Time.time, 1);
            //Debug.Log("dissolveValueLocal is " + dissolveValueLocal);
			
	        if (showPortalDoor && currentCutoffValueFrame < targetCutoffValueFrame)
	        {
		        AnimatePortalFrame(currentCutoffValueFrame);
	        }
			
	        if (showPortalDoor && currentCutoffValuePortal < targetCutoffValuePortal)
            {
		        AnimatePortal(currentCutoffValuePortal);
            }


	        if (!showPortalDoor && currentCutoffValueFrame > targetCutoffValueFrame)
	        {
		        AnimatePortalFrame(currentCutoffValueFrame);
	        }
			
	        if (!showPortalDoor && currentCutoffValuePortal > targetCutoffValuePortal)
            {
                AnimatePortal(currentCutoffValuePortal);
            }
            

            else
            {
                return;
            }
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
    
	private void AnimatePortalFrame(float currentCutoffValuePortal)
	{

		if (Mathf.Abs(targetCutoffValuePortal) - Mathf.Abs(currentCutoffValuePortal) > 0.5f)
		{
			portalFrame.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", Mathf.Lerp(currentCutoffValuePortal, targetCutoffValueFrame, animationSpeedPortal / 120));

		}
		else
		{
			portalFrame.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", Mathf.Lerp(currentCutoffValuePortal, targetCutoffValueFrame, animationSpeedPortal / 60));
		}
	}

    public void ShowPortalButtonPressed()
    {
	    if (!showPortalDoor)
        {
		    targetCutoffValuePortal = 1;
		    targetCutoffValueFrame = 3;
		    showPortalDoor = true;
        }
    }

    public void HidePortalButtonPressed()
    {
	    if (showPortalDoor)
        {
		    targetCutoffValuePortal = -1;
		    targetCutoffValueFrame = -1;
		    showPortalDoor = false;
        }
    }
}
