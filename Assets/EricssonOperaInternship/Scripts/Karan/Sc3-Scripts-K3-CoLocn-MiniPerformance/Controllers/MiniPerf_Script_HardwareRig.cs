using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;

public class MiniPerf_Script_HardwareRig : MonoBehaviour, INetworkRunnerCallbacks
{

    public enum RigPart
    {
        None,
        Headset,
        LeftController,
        RightController,
        Undefined
    }

    public GameObject headset;
    public NetworkRunner nwRunner;

    protected virtual void Start()
    {
        if (nwRunner == null)
        {
            Debug.LogWarning("Runner has to be set in the inspector to forward the input");
        }
        if (nwRunner)
        {
            nwRunner.AddCallbacks(this);
        }
    }

    public virtual void Rotate(float angle)
    {
        transform.RotateAround(headset.transform.position, transform.up, angle);
    }



    #region INetworkRunnerCallbacks

    // Prepare the input, that will be read by NetworkRig in the FixedUpdateNetwork
    public virtual void OnInput(NetworkRunner runner, NetworkInput input)
    {
        HardwareRigNWinput rigInputSet = PrepareRigInput();
        input.Set(rigInputSet);
    }

    protected virtual HardwareRigNWinput PrepareRigInput()
    {
        HardwareRigNWinput hwRigInput = new HardwareRigNWinput();

        //rigInput.playAreaPosition = transform.position;
        //rigInput.playAreaRotation = transform.rotation;
        //rigInput.leftHandPosition = leftHand.transform.position;
        //rigInput.leftHandRotation = leftHand.transform.rotation;
        //rigInput.rightHandPosition = rightHand.transform.position;
        //rigInput.rightHandRotation = rightHand.transform.rotation;
        //rigInput.leftHandCommand = leftHand.handCommand;
        //rigInput.rightHandCommand = rightHand.handCommand;

        hwRigInput.headsetPosition = headset.transform.position;
        hwRigInput.headsetRotation = headset.transform.rotation;
        
        return hwRigInput;
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
