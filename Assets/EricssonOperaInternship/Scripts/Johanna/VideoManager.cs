using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class VideoManager : NetworkBehaviour
{
    public GameObject firstVideo;
    public GameObject secondVideo;
    public GameObject thirdVideo;
    private Transform videoContainer;

    public SlidesManager slidesManager;

    private GameObject firstVideoInstance;
    private GameObject secondVideoInstance;
    private GameObject thirdVideoInstance;

    [Networked(OnChanged = nameof(ManageFirstVideo))]
    public NetworkBool spawnFirstVideoNW { get; set; }

    [Networked(OnChanged = nameof(ManageSecondVideo))]
    public NetworkBool spawnSecondVideoNW { get; set; }

    [Networked(OnChanged = nameof(ManageThirdVideo))]
    public NetworkBool spawnThirdVideoNW { get; set; }

    private void Start()
    {
        videoContainer = GameObject.Find("Video Container").transform;

        //spawnFirstVideoNW = false;
        //spawnSecondVideoNW = false;
        //Debug.Log(spawnFirstVideoNW);
        //Debug.Log(spawnSecondVideoNW);
    }

    public void PlayFirstVideoButtonPressed()
    {
        if (!spawnFirstVideoNW && slidesManager.slidesActive)
        {
            spawnFirstVideoNW = true;
            Debug.Log(spawnFirstVideoNW);
        }
    }

    public void PlaySecondVideoButtonPressed()
    {
        if (!spawnSecondVideoNW && slidesManager.slidesActive)
        {
            spawnSecondVideoNW = true;
            Debug.Log(spawnSecondVideoNW);
        }
    }

    public void PlayThirdVideoButtonPressed()
    {
        if (!spawnThirdVideoNW && slidesManager.slidesActive)
        {
            spawnThirdVideoNW = true;
            Debug.Log(spawnThirdVideoNW);
        }
    }

    public void StopFirstVideoButtonPressed()
    {
        if (spawnFirstVideoNW)
        {
            spawnFirstVideoNW = false;
            Debug.Log(spawnFirstVideoNW);
        }
    }

    public void StopSecondVideoButtonPressed()
    {
        if (spawnSecondVideoNW)
        {
            spawnSecondVideoNW = false;
            Debug.Log(spawnSecondVideoNW);
        }
    }

    public void StopThirdVideoButtonPressed()
    {
        if (spawnThirdVideoNW)
        {
            spawnThirdVideoNW = false;
            Debug.Log(spawnThirdVideoNW);
        }
    }


    public void PlayFirstVideo()
    {
        firstVideoInstance = Instantiate(firstVideo);
        firstVideoInstance.transform.parent = videoContainer;
        firstVideoInstance.transform.localPosition = Vector3.zero;
        firstVideoInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void StopFirstVideo()
    {
        Destroy(firstVideoInstance);
    }

    public void PlaySecondVideo()
    {
        secondVideoInstance = Instantiate(secondVideo);
        secondVideoInstance.transform.parent = videoContainer;
        secondVideoInstance.transform.localPosition = Vector3.zero;
        secondVideoInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    public void StopSecondVideo()
    {
        Destroy(secondVideoInstance);
    }

    public void PlayThirdVideo()
    {
        thirdVideoInstance = Instantiate(thirdVideo);
        thirdVideoInstance.transform.parent = videoContainer;
        thirdVideoInstance.transform.localPosition = Vector3.zero;
        thirdVideoInstance.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    public void StopThirdVideo()
    {
        Destroy(thirdVideoInstance);
    }


    public static void ManageFirstVideo(Changed<VideoManager> changeVariable)
    {

        if (changeVariable.Behaviour.spawnFirstVideoNW)
        {
            Debug.Log("Playing First Video");
            changeVariable.Behaviour.PlayFirstVideo();
        }

        else
        {
            Debug.Log("Stopping First Video");
            changeVariable.Behaviour.StopFirstVideo();
        }
    }

    public static void ManageSecondVideo(Changed<VideoManager> changeVariable)
    {

        if (changeVariable.Behaviour.spawnSecondVideoNW)
        {
            Debug.Log("Playing Second Video");
            changeVariable.Behaviour.PlaySecondVideo();
        }

        else
        {
            Debug.Log("Stopping Second Video");
            changeVariable.Behaviour.StopSecondVideo();
        }
    }

    public static void ManageThirdVideo(Changed<VideoManager> changeVariable)
    {

        if (changeVariable.Behaviour.spawnThirdVideoNW)
        {
            Debug.Log("Playing Third Video");
            changeVariable.Behaviour.PlayThirdVideo();
        }

        else
        {
            Debug.Log("Stopping Third Video");
            changeVariable.Behaviour.StopThirdVideo();
        }
    }

}
