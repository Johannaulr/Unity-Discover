using UnityEngine;
using System.Collections;

public class PendulumScript : MonoBehaviour
{
	[SerializeField, Tooltip("The Sphere Dissolve Portal")]
	private GameObject dissolveSphere;
	[SerializeField, Tooltip("The CenterEyeCamera Transform")]
	private Transform hmdTransform;
	[SerializeField, Tooltip("The speed at which the variable X pendulums between 0 and 1."),Range(0f,1f)]
	private float animationSpeed = 1f;
	
	[SerializeField, Tooltip("Give a threshold value that affects Cutoff Height in case the portal isn't fully dissolving (Used for pingpong function only)")]
	private float portalSizeThreshold = 2f;
	
	[SerializeField, Tooltip("Value Cutoff Height in shader we want to achieve"),Range(-1f,1f)]
	private float targetCutoffValue;
	
	private float cutoffValue;
	
	private void Start()
	{
		float cutOff = dissolveSphere.gameObject.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");
	}
    private void Update()
    {
	    //animatePingPong();
	    animatePortal();
		
	    this.gameObject.transform.position = hmdTransform.position;
	    
    }
    
	private void animatePortal() 
	{	
		
		float currentCutoffValue = dissolveSphere.gameObject.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");
		
		if(targetCutoffValue >= currentCutoffValue)
		{
			dissolveSphere.gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height",Mathf.Lerp(currentCutoffValue, targetCutoffValue, animationSpeed/100));
		} 
		else 
		{
			dissolveSphere.gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height",Mathf.Lerp(currentCutoffValue, targetCutoffValue, animationSpeed/100));
		}
	}   
    
	private void animatePingPong() 
	{
		cutoffValue = Mathf.PingPong(Time.time * animationSpeed, portalSizeThreshold) - portalSizeThreshold/2;
		dissolveSphere.gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height",cutoffValue);
	}   
    
    
}