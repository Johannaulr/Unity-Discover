using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class VideoManager : NetworkBehaviour
{
    public NetworkObject firstVideo;
    public NetworkObject secondVideo;
    private Transform videoContainer;

    private void Start()
    {
        videoContainer = GameObject.Find("Video Container").transform;
    }

    public void PlayFirstVideo()
    {
        Runner.Spawn(firstVideo);

        //firstVideo.transform.SetParent(videoContainer, false);
    }

    public void StopFirstVideo()
    {
        firstVideo.RequestStateAuthority();
        Runner.Despawn(firstVideo,true);
        Debug.Log("Despawning firstVideo");
    }

    public void PlaySecondVideo()
    {
        Runner.Spawn(secondVideo);
        //secondVideo.transform.SetParent(videoContainer, false);
    }

    public void StopSecondVideo()
    {
        secondVideo.RequestStateAuthority();
        Runner.Despawn(secondVideo, false);
        //Runner.Despawn(secondVideo);
        Debug.Log("Despawning secondVideo");
    }

}
