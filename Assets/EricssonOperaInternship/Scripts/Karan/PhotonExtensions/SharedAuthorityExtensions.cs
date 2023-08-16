using Fusion;
using System.Threading.Tasks;
using UnityEngine;

public static class SharedAuthorityExtensions
{
    /**
    * Request state authority and wait for it to be received
    * Relevant in shared topology only
    * TAKEN FROM:   https://doc.photonengine.com/fusion/current/technical-samples/fusion-vr-shared#grabbing_synchronization
    */
    public static async Task<bool> WaitForStateAuthority(this NetworkObject o, float maxWaitTime = 8)
    {
        float waitStartTime = Time.time;
        o.RequestStateAuthority();
        while (!o.HasStateAuthority && (Time.time - waitStartTime) < maxWaitTime)
        {
            await Task.Delay(1);
        }
        return o.HasStateAuthority;
    }
}