using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePassthrough : MonoBehaviour
{
    public PortalAudioManager audioManager;

    public EnableVREnvironment enableVREnvironment;

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
            Vector3 playerVelocity = other.GetComponent<VelocityEstimator>().GetVelocityEstimate();

            float playerAngle = Vector3.Angle(transform.forward, playerVelocity);

            if (playerAngle < 90)
            {
                foreach (var item in insidePortalGameObjects)
                {
                    SetLayerRecursively(item, insidePortalLayerIndex);
                }

                /*foreach (var item in outsidePortalGameObjects)
                {
                    SetLayerRecursively(item, defaultLayerIndex);
                }*/

                audioManager.StopAmbientLoop();
            }
        }

        if (other.CompareTag(grabbableTag))
        {
            SetLayerRecursively(other.gameObject, defaultLayerIndex);
            insidePortalGameObjects.Remove(other.gameObject);
            enableVREnvironment.insidePortalGameObjects.Remove(other.gameObject);
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
