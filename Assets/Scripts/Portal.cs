using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    public string targetTag;
    public GameObject[] insidePortalGameObjects;
    public int newLayer;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(targetTag))
        {
            Vector3 targetVelocity = other.GetComponent<VelocityEstimator>().GetVelocityEstimate();

            float angle = Vector3.Angle(transform.forward, targetVelocity);

            if(angle < 90)
            {
                foreach (var item in insidePortalGameObjects)
                {
                    item.layer = newLayer;
                }
            }
        }
    }
}
