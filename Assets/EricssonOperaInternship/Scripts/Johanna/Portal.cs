using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public string targetTag;
    public GameObject[] insidePortalGameObjects;
    public int newLayer;
    private bool isInVR;

    private void Start()
    {
        isInVR = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Vector3 targetVelocity = other.GetComponent<VelocityEstimator>().GetVelocityEstimate();

            float angle = Vector3.Angle(transform.forward, targetVelocity);

            if(angle < 90)
            {
                if(!isInVR) {

                }
            }
        }
    }

    private void EnableVREnvironment()
    {
        foreach (var item in insidePortalGameObjects)
        {
            if (item.name == "UI Layer")
            {
                SetLayerRecursively(item, 5);

            }
            else
            {
                SetLayerRecursively(item, 0);
            }
        }
        isInVR = true;
    }

    private void EnablePassthrough()
    {
        isInVR = false;
    }

    private void SetLayerRecursively(GameObject obj, int targetLayer)
    {
        obj.layer = targetLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, targetLayer);
        }
    }
}
