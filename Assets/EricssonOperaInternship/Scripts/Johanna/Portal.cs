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
        if (other.CompareTag(targetTag))
        {
            Vector3 targetVelocity = other.GetComponent<VelocityEstimator>().GetVelocityEstimate();

            float angle = Vector3.Angle(transform.forward, targetVelocity);

            if(angle < 90)
            {
                foreach (var item in insidePortalGameObjects)
                {
                    if (item.name == "UI Layer")
                    {
                        if (item.layer == 6)
                        {

                        SetLayerRecursively(item, 5);

                        }

                        else
                        {

                        SetLayerRecursively(item, 6);

                        }
                    }

                    else
                    {
                        item.layer = newLayer;
                    }

                }
            }
        }
    }

    private void SetLayerRecursively(GameObject obj, int uILayer)
    {
        obj.layer = uILayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, uILayer);
        }
    }
}
