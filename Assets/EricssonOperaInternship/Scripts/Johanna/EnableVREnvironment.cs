using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableVREnvironment : MonoBehaviour
{
    public PortalAudioManager audioManager;

    public int defaultLayerIndex;
    public int insidePortalLayerIndex;
    public int outsidePortalLayerIndex;
    public string playerTag;
    public string grabbableTag;
    public List<GameObject> insidePortalGameObjects = new List<GameObject>();
    //public GameObject[] outsidePortalGameObjects;

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
                        SetLayerRecursively(item, defaultLayerIndex);
                    }

                }

                /*foreach (var item in outsidePortalGameObjects)
                {
                    SetLayerRecursively(item, outsidePortalLayerIndex);
                }*/

                audioManager.PlayAmbientLoop();
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
