using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerObject_New_Script : MonoBehaviour
{
    AudioSource audSrc;
    public AudioClip AudioFile;

    // Start is called before the first frame update
    void Start()
    {
        audSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioTrack()
    {
        audSrc.PlayOneShot(AudioFile, 0.7f);
    }

    public void StopAudioTrack()
    {
        audSrc.Stop();
    }
}
