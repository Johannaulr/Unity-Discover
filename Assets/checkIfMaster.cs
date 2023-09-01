using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
public class checkIfMaster : NetworkBehaviour
{

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if(!Runner.IsSharedModeMasterClient)
        {
            Destroy(this.gameObject);
        }

    }
}
