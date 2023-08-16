using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableVREnvironment : MonoBehaviour
{
    public string playerTag;
    public string grabbableTag;
    public GameObject[] insidePortalGameObjects;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            Vector3 targetVelocity = other.GetComponent<VelocityEstimator>().GetVelocityEstimate();

            float angle = Vector3.Angle(transform.forward, targetVelocity);

            if (angle < 90)
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
            }
        }


        if (other.CompareTag(grabbableTag))
        {
            SetLayerRecursively(other.gameObject, 6);
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
