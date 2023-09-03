using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;


//namespace Fusion.XR.Shared.Rig
//{
    /**
     * 
     * Networked VR user
     * 
     * Handle the synchronisation of the various rig parts: headset, left hand, right hand, and playarea (represented here by the NetworkRig)
     * Use the local HardwareRig rig parts position info when this network rig is associated with the local user 
     * 
     * Note: this class (and all network classes in this sample) is heavily focused on shared mode, hence not using InputAuthority, and cannot be used in Host or Server topologies 
     * 
     **/

    [RequireComponent(typeof(NetworkTransform))]
    // We ensure to run after the NetworkTransform or NetworkRigidbody, to be able to override the interpolation target behavior in Render()
    [OrderAfter(typeof(NetworkTransform), typeof(NetworkRigidbody))]
    public class Script_NetworkRig : NetworkBehaviour
    {
        public Script_HardwareRig hardwareRig;
        //public NetworkHand leftHand;
        //public NetworkHand rightHand;
        public Script_NetworkHeadset headset;

        [HideInInspector]
        public NetworkTransform networkTransform;

        protected virtual void Awake()
        {
            networkTransform = GetComponent<NetworkTransform>();
        }

        // As we are in shared topology, having the StateAuthority means we are the local user
        public virtual bool IsLocalNetworkRig => Object.HasStateAuthority;

        public override void Spawned()
        {
            base.Spawned();
            if (IsLocalNetworkRig)
            {
                hardwareRig = FindObjectOfType<Script_HardwareRig>();
                if (hardwareRig == null) Debug.LogError("Missing HardwareRig in the scene");
            }
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();

            // update the rig at each network tick
            if (GetInput<RigInput>(out var input))
            {
                ApplyInputToRigParts(input);

                //ApplyInputToHandPoses(input);

            }
        }

        protected virtual void ApplyInputToRigParts(RigInput input)
        {
            //transform.position = input.playAreaPosition;
            //transform.rotation = input.playAreaRotation;
            //leftHand.transform.position = input.leftHandPosition;
            //leftHand.transform.rotation = input.leftHandRotation;
            //rightHand.transform.position = input.rightHandPosition;
            //rightHand.transform.rotation = input.rightHandRotation;
            headset.transform.position = input.headsetPosition;
            headset.transform.rotation = input.headsetRotation;
        }
        protected virtual void ApplyInputToHandPoses(RigInput input)
        {
            // we update the hand pose info. It will trigger on network hands OnHandCommandChange on all clients, and update the hand representation accordingly
            //leftHand.HandCommand = input.leftHandCommand;
            //rightHand.HandCommand = input.rightHandCommand;
        }

        public override void Render()
        {
            base.Render();
            if (IsLocalNetworkRig)
            {
                // Extrapolate for local user :
                // we want to have the visual at the good position as soon as possible, so we force the visuals to follow the most fresh hardware positions
                // To update the visual object, and not the actual networked position, we move the interpolation targets
                //networkTransform.InterpolationTarget.position = hardwareRig.transform.position;
                //networkTransform.InterpolationTarget.rotation = hardwareRig.transform.rotation;
                //leftHand.networkTransform.InterpolationTarget.position = hardwareRig.leftHand.transform.position;
                //leftHand.networkTransform.InterpolationTarget.rotation = hardwareRig.leftHand.transform.rotation;
                //rightHand.networkTransform.InterpolationTarget.position = hardwareRig.rightHand.transform.position;
                //rightHand.networkTransform.InterpolationTarget.rotation = hardwareRig.rightHand.transform.rotation;
                headset.networkTransform.InterpolationTarget.position = hardwareRig.headset.transform.position;
                headset.networkTransform.InterpolationTarget.rotation = hardwareRig.headset.transform.rotation;
            }
        }
    }
//}

