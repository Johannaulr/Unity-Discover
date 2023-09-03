using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class Script_GifManagerNW : NetworkBehaviour
{
    public List<GameObject> VideoQuadList = new List<GameObject>();

    private List<string> GifVidsList = new List<string>();

    private string path1 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-1.mp4";
    private string path2 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-2.mp4";
    private string path3 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-3.mp4";

    [Networked]
    public int randomVidIntNW { get; set; }


    [Networked]
    public bool VideoHeadFlagNW { get; set; }
    
    private void Awake()
    {
        VideoHeadFlagNW = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (VideoHeadFlagNW == true) 
        {
            this.gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(true);
            //gameObject.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<MeshRenderer>().enabled = IsVisible;
        }

        GifVidsList.Add(path1);
        GifVidsList.Add(path2);
        GifVidsList.Add(path3);

        randomVidIntNW = Random.Range(0, GifVidsList.Count);

        for (var k = 0; k < VideoQuadList.Count; k++)
        {
            VideoQuadList[k].GetComponent<UnityEngine.Video.VideoPlayer>().url = GifVidsList[randomVidIntNW];
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
            }

            randomVidIntNW++;

            if (randomVidIntNW > GifVidsList.Count - 1)
            {
                randomVidIntNW = 0;
            }

            for (int k = 0; k < VideoQuadList.Count; k++)
            {
                VideoQuadList[k].GetComponent<UnityEngine.Video.VideoPlayer>().url = GifVidsList[randomVidIntNW];
            }

            MiniPerf_Script_SceneManager_n1.instance.DebugLogMessage("YYYYYYYY");
        }
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority)]
    private void RPC_ChangeVideo()
    {
        //Runner.Spawn(SpherePrefabToSpawn, position: new Vector3(UnityEngine.Random.Range(-0.5f, 0.5f), UnityEngine.Random.Range(1.5f, 1.8f), UnityEngine.Random.Range(-0.5f, 0.5f)), Quaternion.identity);
    }
}

