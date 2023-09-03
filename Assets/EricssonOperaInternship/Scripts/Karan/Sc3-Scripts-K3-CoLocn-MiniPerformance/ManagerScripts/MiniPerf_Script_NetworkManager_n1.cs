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

public class MiniPerf_Script_NetworkManager_n1 : MonoBehaviour, INetworkRunnerCallbacks
{
    [SerializeField] private NetworkRunner m_networkRunner;
    [SerializeField] private NetworkSceneManagerDefault m_sceneManager;
    [SerializeField] private NetworkObject m_colocationPrefab;

    public UnityEvent OnConnectionStarted;
    public UnityEvent<string> OnNetworkEvent;

    public GameObject JoiningUIPanel;

    //Script_LocalInputHandler localInputHandler;
    
    //public NetworkPlayer PlayerPF;

    public NetworkObject userPrefab;
    public GameObject sceneCamera;

    private void Awake()
    {
        m_networkRunner.AddCallbacks(this);
        m_networkRunner.ProvideInput = true;
    }

    public async void StartConnection(bool isHost)
    {
        OnConnectionStarted?.Invoke();
        OnNetworkEvent?.Invoke("Connecting to Photon...");
        MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Connecting to Photon...");
        ColocationDriverNetObj.OnColocationCompletedCallback += OnColocationReady;
        await Connect(isHost);
    }

    private async Task Connect(bool isHost)
    {
        var args = new StartGameArgs()
        {
            GameMode = GameMode.Shared,
            SessionName = MiniPerf_Script_SceneManager_n1.instance.networkSessionName,
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
            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Colocation Ready");
        }
        else
        {
            OnNetworkEvent?.Invoke("Joined Remotely");
            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Joined Remotely");
        }
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Player Joined");

        JoiningUIPanel.SetActive(false);

        if (player == runner.LocalPlayer)
        {
            //NetworkObject networkPlayerObject = m_networkRunner.Spawn(userPrefab, Vector3.zero, Quaternion.identity);
            NetworkObject networkPlayerObject = runner.Spawn(userPrefab, position: transform.position - new Vector3(-0.1f,-0.2f,-0.5f), rotation: transform.rotation, player, (runner, obj) => {});
            networkPlayerObject.GetComponent<Script_GifManagerNW>().enabled = true;
            //networkPlayerObject.transform.position = sceneCamera.transform.position;

            /*if (localInputHandler == null)
            {
                //localInputHandler = GameObject.Find("Zone-Manager-Object-NW-Prefab(Clone)").GetComponent<Script_LocalInputHandler>();
                //localInputHandler = networkPlayerObject.GetComponent<Script_LocalInputHandler>();
            }*/
        }

        //cubeTest.transform.SetParent(CubeContainerObject.transform);
        //CubeContainerObject.SetActive(false);

        MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Player Prefab Spawned");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Player Left");
    }

    public void OnConnectedToServer(NetworkRunner runner)
    {
        MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Connected to Photon");
        OnNetworkEvent?.Invoke("Connected To Photon");
        if (m_networkRunner.IsMasterClient())
        {
            MiniPerf_Script_SceneManager_n1.instance.SetHostInFusionServer(true);
            Debug.Log("Spawn Colocation Prefab");
            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("Spawn Colocation Prefab");
            _ = m_networkRunner.Spawn(m_colocationPrefab);
        }
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        /*
        if (localInputHandler != null) 
        {
            input.Set(localInputHandler.GetInputForNetwork());
        }

        var nwData = new NetworkInputData();

        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0.0f)
        {
            nwData.nidInt = 2023;
        }
        else
        {
            nwData.nidInt = 0;
        }

        input.Set(nwData);
        */
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
