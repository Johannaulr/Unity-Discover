using UnityEngine;

public class StartMenuController : MonoBehaviour
{
    public void ConnectAsHost()
    {
        OperaGameManager.instance.StartNetworkOpera(isHost: true);
    }

    public void ConnectAsClient()
    {
        OperaGameManager.instance.StartNetworkOpera();
    }
}
