using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_GifManager : MonoBehaviour
{
    public List<GameObject> VideoQuadList = new List<GameObject>();

    private string path1 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-1.mp4";
    private string path2 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-2.mp4";
    private string path3 = "Assets/EricssonOperaInternship/Media/Video/Karan/TestMP4s/gif2mp4-3.mp4";

    private List<string> GifVidsList = new List<string>();

    private int randomVidInt;

    // Start is called before the first frame update
    void Start()
    {
        GifVidsList.Add(path1);
        GifVidsList.Add(path2);
        GifVidsList.Add(path3);

        randomVidInt = Random.Range(0, GifVidsList.Count);

        for (var k = 0; k < VideoQuadList.Count; k++) 
        {
            VideoQuadList[k].GetComponent<UnityEngine.Video.VideoPlayer>().url = GifVidsList[randomVidInt];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.X))
        {
            randomVidInt++;
            
            if (randomVidInt > GifVidsList.Count - 1) 
            {
                randomVidInt = 0;
            }

            for (int k = 0; k < VideoQuadList.Count; k++)
            {
                VideoQuadList[k].GetComponent<UnityEngine.Video.VideoPlayer>().url = GifVidsList[randomVidInt];
            }
        }
    }
}
