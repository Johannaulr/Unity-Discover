using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using Discover.Colocation.Test;
using System;
using Fusion;
using Fusion.Sockets;

public class PlayerJoinedObjectScript : MonoBehaviour, INetworkRunnerCallbacks
{

    public TMP_Text textObject;
    public TMP_Text TextObjectPlayerType;

    [SerializeField] private NetworkRunner networkRunner;

    private bool hostFlag;

    private void Awake()
    {
        networkRunner.AddCallbacks(this);
        networkRunner.ProvideInput = true;
    }


    private void OnEnable()
    {
        ColocationTestBootStrapper ItIsObjectOfColocationTestBootStrapper = FindObjectOfType<ColocationTestBootStrapper>();
        //myEvent.AddListener(OnNetworkEvent);
        
        if (ItIsObjectOfColocationTestBootStrapper != null)
        {
            ItIsObjectOfColocationTestBootStrapper.OnNetworkEvent.AddListener(HandleStringEventFunction);
        }
    }

    private void OnDisable()
    {
        ColocationTestBootStrapper ItIsObjectOfColocationTestBootStrapper = FindObjectOfType<ColocationTestBootStrapper>();
        
        if (ItIsObjectOfColocationTestBootStrapper != null)
        {
            ItIsObjectOfColocationTestBootStrapper.OnNetworkEvent.RemoveListener(HandleStringEventFunction);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hostFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void HandleStringEventFunction (string messageReceived)
    {
        // Do something with the received message
        Debug.Log("Received string event: " + messageReceived);
        textObject.text = messageReceived;
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player) { }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player) { }
    public void OnInput(NetworkRunner runner, NetworkInput input) { }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
    public void OnConnectedToServer(NetworkRunner runner) 
    {
        if (networkRunner.IsSharedModeMasterClient) 
        {
            TextObjectPlayerType.text = "You are HOST";
        }
    }
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
