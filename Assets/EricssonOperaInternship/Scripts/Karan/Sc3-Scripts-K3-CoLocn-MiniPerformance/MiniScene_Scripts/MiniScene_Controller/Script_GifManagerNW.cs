using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Fusion;
using Fusion.Sockets;

public class Script_GifManagerNW : NetworkBehaviour
{
    public List<GameObject> VideoQuadList = new List<GameObject>();

    private List<string> GifVidsList = new List<string>();

    //private string path1 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-1.mp4";
    //private string path2 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-2.mp4";
    //private string path3 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-3.mp4";

    [SerializeField]
    private List<VideoClip> videoArray = new List<VideoClip>();

    [Networked]
    public int randomVidIntNW { get; set; }


    [Networked]
    public bool VideoHeadFlagNW { get; set; }
    

    // Start is called before the first frame update
    void Start()
    {
        if (this.enabled)
        {
            VideoHeadFlagNW = false;

            if (VideoHeadFlagNW == false)
            {
                this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
                //gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = IsVisible;
            }

            //GifVidsList.Add(path1);
            //GifVidsList.Add(path2);
            //GifVidsList.Add(path3);

            randomVidIntNW = Random.Range(0, videoArray.Count);

            Debug.Log("randomVidIntNW issssssssssssssssss : " + randomVidIntNW);

            for (var k = 0; k < VideoQuadList.Count; k++)
            {
                VideoQuadList[k].GetComponent<VideoPlayer>().clip = videoArray[randomVidIntNW];
                //Debug.Log(VideoQuadList[k].GetComponent<UnityEngine.Video.VideoPlayer>().url);

                VideoQuadList[k].GetComponent<VideoPlayer>().Play();
            }

            //VideoQuadList[0].GetComponent<UnityEngine.Video.VideoPlayer>().Play();
        }
        
    }

    // Update is called once per frame
    

    public override void FixedUpdateNetwork()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.Y))
        {
            if (VideoHeadFlagNW == false)
            {
                VideoHeadFlagNW = true;
                this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
                //RPC_ChangeVideo(randomVidIntNW);
            }

            randomVidIntNW++;

            if (randomVidIntNW > 2)
            {
                randomVidIntNW = 0;
            }

            for (int k = 0; k < VideoQuadList.Count; k++)
            {
                VideoQuadList[k].GetComponent<VideoPlayer>().clip = videoArray[randomVidIntNW];
            }

            //RPC_ChangeVideo(randomVidIntNW);

            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("YYY");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    private void RPC_ChangeVideo(int ValRecv)
    {
        //Runner.Spawn(SpherePrefabToSpawn, position: new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(1.5f, 1.8f), UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);

        for (int k = 0; k < VideoQuadList.Count; k++)
        {
            VideoQuadList[k].GetComponent<VideoPlayer>().clip = videoArray[ValRecv];
        }
    }
}

