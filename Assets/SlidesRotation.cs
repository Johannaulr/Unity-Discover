using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;


public class SlidesRotation : NetworkBehaviour
{
    public Transform target;

    public float localRotationY;

    [Networked(OnChanged = nameof(NetworkRotationChanged))]
    public float networkedRotationY { get; set; }

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        networkedRotationY = 0;
        localRotationY = 0;

        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger))
        {
            networkedRotationY = rectTransform.rotation.y;
        }

    }

    public void ChangeRotation()
    {
        transform.rotation = Quaternion.Euler(0, localRotationY, 0);
    }

    public static void NetworkRotationChanged(Changed<SlidesRotation> changed)
    {
        changed.Behaviour.localRotationY = changed.Behaviour.networkedRotationY;
        changed.Behaviour.ChangeRotation();
    }
}
