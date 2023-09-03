using Fusion.Sockets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Fusion;

//namespace Fusion.XR.Shared.Rig
//{
    public enum RigPart
    {
        None,
        Headset,
        LeftController,
        RightController,
        Undefined
    }

    // Include all rig parameters in an network input structure
    public struct RigInput : INetworkInput
    {
        public Vector3 playAreaPosition;
        public Quaternion playAreaRotation;
        public Vector3 leftHandPosition;
        public Quaternion leftHandRotation;
        public Vector3 rightHandPosition;
        public Quaternion rightHandRotation;
        public Vector3 headsetPosition;
        public Quaternion headsetRotation;
        //public HandCommand leftHandCommand;
        //public HandCommand rightHandCommand;
    }

    /**
     * 
     * Hardware rig gives access to the various rig parts: head, left hand, right hand, and the play area, represented by the hardware rig itself
     *  
     * Can be moved, either instantanesously, or with a camera fade
     * 
     **/

    public class Script_HardwareRig : MonoBehaviour, INetworkRunnerCallbacks
    {
        //public HardwareHand leftHand;
        //public HardwareHand rightHand;
        
        public Script_HardwareHeadset headset;

        public NetworkRunner runner;

        //[Serializable]
        //public class TeleportEvent : UnityEvent<Vector3, Vector3> { }
        //public TeleportEvent onTeleport = new TeleportEvent();

        protected virtual void Start()
        {
            if (runner == null)
            {
                Debug.LogWarning("Runner has to be set in the inspector to forward the input");
            }
            if (runner) runner.AddCallbacks(this);
        }

        #region Locomotion
        // Update the hardware rig rotation. This will trigger a Riginput network update
        public virtual void Rotate(float angle)
        {
            transform.RotateAround(headset.transform.position, transform.up, angle);
        }

        #endregion

        #region INetworkRunnerCallbacks

        // Prepare the input, that will be read by NetworkRig in the FixedUpdateNetwork
        public virtual void OnInput(NetworkRunner runner, NetworkInput input)
        {
            RigInput rigInput = PrepareRigInput();
            input.Set(rigInput);
        }

        protected virtual RigInput PrepareRigInput()
        {
            RigInput rigInput = new RigInput();
            //rigInput.playAreaPosition = transform.position;
            //rigInput.playAreaRotation = transform.rotation;
            //rigInput.leftHandPosition = leftHand.transform.position;
            //rigInput.leftHandRotation = leftHand.transform.rotation;
            //rigInput.rightHandPosition = rightHand.transform.position;
            //rigInput.rightHandRotation = rightHand.transform.rotation;
            rigInput.headsetPosition = headset.transform.position;
            rigInput.headsetRotation = headset.transform.rotation;
            //rigInput.leftHandCommand = leftHand.handCommand;
            //rigInput.rightHandCommand = rightHand.handCommand;
            return rigInput;
        }

        #endregion

        #region INetworkRunnerCallbacks (unused)
        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }


        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

        public void OnConnectedToServer(NetworkRunner runner) { }

        public void OnDisconnectedFromServer(NetworkRunner runner) { }

        public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

        public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }

        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }

        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }

        public void OnSceneLoadDone(NetworkRunner runner) { }

        public void OnSceneLoadStart(NetworkRunner runner) { }
        #endregion
    }
//}
