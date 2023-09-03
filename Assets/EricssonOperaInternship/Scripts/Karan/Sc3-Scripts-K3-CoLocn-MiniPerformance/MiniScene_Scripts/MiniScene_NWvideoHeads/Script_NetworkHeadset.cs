using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace Fusion.XR.Shared.Rig
//{
    /**
     * 
     * Network VR user headset
     * Position synchronization is handled in the NetworkRig
     * 
     **/

    [RequireComponent(typeof(NetworkTransform))]
    [OrderAfter(typeof(Script_NetworkRig), typeof(NetworkTransform), typeof(NetworkRigidbody))]
    public class Script_NetworkHeadset : NetworkBehaviour
    {
        [HideInInspector]
        public NetworkTransform networkTransform;

        private void Awake()
        {
            if (networkTransform == null) networkTransform = GetComponent<NetworkTransform>();
        }
    }
//}