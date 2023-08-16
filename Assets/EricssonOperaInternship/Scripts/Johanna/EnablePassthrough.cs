using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePassthrough : MonoBehaviour
{
    public string targetTag;
    public GameObject[] insidePortalGameObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Vector3 targetVelocity = other.GetComponent<VelocityEstimator>().GetVelocityEstimate();

            float angle = Vector3.Angle(transform.forward, targetVelocity);

            if (angle < 90)
            {
                foreach (var item in insidePortalGameObjects)
                {
                    SetLayerRecursively(item, 6);
                }
            }
        }
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
