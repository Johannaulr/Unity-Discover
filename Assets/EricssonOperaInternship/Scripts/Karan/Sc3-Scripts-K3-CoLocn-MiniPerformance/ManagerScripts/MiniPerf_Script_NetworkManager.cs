using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

using Discover.Networking;
using Discover.Colocation;

public class MiniPerf_Script_NetworkManager : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner m_networkRunner;
    [SerializeField] private NetworkSceneManagerDefault m_sceneManager;
    [SerializeField] private NetworkObject m_colocationPrefab;

    public UnityEvent OnConnectionStarted;
    public UnityEvent<string> OnNetworkEvent;

    public GameObject JoiningUIPanel;

    private void Awake()
    {
        m_networkRunner.AddCallbacks(this);
        m_networkRunner.ProvideInput = true;
    }


    public async void StartConnection(bool isHost)
    {
        OnConnectionStarted?.Invoke();
        OnNetworkEvent?.Invoke("Connecting to Photon...");
        ColocationDriverNetObj.OnColocationCompletedCallback += OnColocationReady;
        await Connect(isHost);
    }

    private async Task Connect(bool isHost)
    {
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = OperaGameManager.instance.networkSessionName,
            Scene = SceneManager.GetActiveScene().buildIndex,
            SceneManager = m_sceneManager
        };
        _ = await m_networkRunner.StartGame(args);
    }

    private void OnColocationReady(bool success)
    {
        if (success)
        {
            OnNetworkEvent?.Invoke("Colocation Ready");
            OperaGameManager.instance.DebugLogMessage("Colocation Ready");
        }
        else
        {
            OnNetworkEvent?.Invoke("Joined Remotely");
            OperaGameManager.instance.DebugLogMessage("Joined Remotely");
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        OperaGameManager.instance.DebugLogMessage("Player Joined");

        JoiningUIPanel.SetActive(false);
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        OperaGameManager.instance.DebugLogMessage("Player Left");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        OperaGameManager.instance.DebugLogMessage("Connected to Server");
        OnNetworkEvent?.Invoke("Connected To Photon");
        if (m_networkRunner.IsMasterClient())
        {
            OperaGameManager.instance.SetHostInFusionServer(true);
            Debug.Log("Spawn Colocation Prefab");
            OperaGameManager.instance.DebugLogMessage("Spawn Colocation Prefab");
            _ = m_networkRunner.Spawn(m_colocationPrefab);
        }
    }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {

    }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token)
    {

    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {

    }
    public void OnDisconnectedFromServer(NetworkRunner runner)
    {

    }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {

    }
    public void OnInput(NetworkRunner runner, NetworkInput input)
    {

    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {

    }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data)
    {

    }
    public void OnSceneLoadDone(NetworkRunner runner)
    {

    }
    public void OnSceneLoadStart(NetworkRunner runner)
    {

    }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {

    }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {

    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {

    }

}
