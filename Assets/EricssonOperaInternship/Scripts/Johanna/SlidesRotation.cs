using System;
using System.Collections;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;


public class SlidesRotation : NetworkBehaviour, INetworkRunnerCallbacks
{
    public Transform target;

    public float localRotationY;

    [Networked(OnChanged = nameof(NetworkRotationChanged))]
    public float networkedRotationY { get; set; }

    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        //networkedRotationY = 0;
        localRotationY = 0;

        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger))
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

        }

        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger))
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

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) { networkedRotationY = 0; }
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
}
