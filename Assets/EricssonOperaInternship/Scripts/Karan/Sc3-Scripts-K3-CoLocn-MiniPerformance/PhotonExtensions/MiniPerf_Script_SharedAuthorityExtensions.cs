using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public static class MiniPerf_Script_SharedAuthorityExtensions
{
    /**
    * Request state authority and wait for it to be received
    * Relevant in shared topology only
    * TAKEN FROM:   https://doc.photonengine.com/fusion/current/technical-samples/fusion-vr-shared#grabbing_synchronization
    */
    public static async Task<bool> WaitForStateAuthority(this NetworkObject netObject, float maxWaitTime = 8)
    {
        float waitStartTime = Time.time;
        netObject.RequestStateAuthority();
        while (!netObject.HasStateAuthority && (Time.time - waitStartTime) < maxWaitTime)
        {
            await Task.Delay(1);
        }
        return netObject.HasStateAuthority;
    }
}
