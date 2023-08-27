using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class VideoManager : NetworkBehaviour
{
    public GameObject firstVideo;
    public GameObject secondVideo;
    private Transform videoContainer;

    private bool firstVideoSpawned;
    private bool secondVideoSpawned;

    private void Start()
    {
        videoContainer = GameObject.Find("Video Container").transform;
        firstVideoSpawned = false;
        secondVideoSpawned = false;
    }

    public void PlayFirstVideo()
    {
        if (!firstVideoSpawned)
        {
            Runner.Spawn(firstVideo);
            firstVideo.transform.SetParent(videoContainer);
            firstVideoSpawned = true;
            Debug.Log("FirstVideoSpawned: " + firstVideoSpawned);
        }
    }

    public void StopFirstVideo()
    {
        if (firstVideoSpawned)
        {
            Runner.Despawn(GameObject.Find("Video Canvas 1(Clone)").GetComponent<NetworkObject>(), false);
            Destroy(GameObject.Find("Video Canvas 1"));
            Debug.Log("Despawning firstVideo");
            firstVideoSpawned = false;
            Debug.Log("FirstVideoSpawned: " + firstVideoSpawned);
        }

    }

    public void PlaySecondVideo()
    {
        if (!secondVideoSpawned)
        {
            Runner.Spawn(secondVideo);
            secondVideo.transform.SetParent(videoContainer);
            secondVideoSpawned = true;
            Debug.Log("SecondVideoSpawned: " + secondVideoSpawned);
        }
    }

    public void StopSecondVideo()
    {
        if (secondVideo)
        {
            Runner.Despawn(GameObject.Find("Video Canvas 2(Clone)").GetComponent<NetworkObject>(), false);
            Destroy(GameObject.Find("Video Canvas 2"));
            Debug.Log("Despawning secondVideo");
            secondVideoSpawned = false;
            Debug.Log("SecondVideoSpawned: " + secondVideoSpawned);
        }

    }

}
