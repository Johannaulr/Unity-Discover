using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAudioManager : MonoBehaviour
{
    private AudioSource ambientSound;

    // Start is called before the first frame update
    void Start()
    {
        ambientSound = GetComponentInChildren<AudioSource>();
    }
    public void PlayAmbientLoop()
    {
        if (!ambientSound.isPlaying)
        {
        ambientSound.Play();
        }
    }

    public void StopAmbientLoop()
    {
        if (ambientSound.isPlaying)
        {
        ambientSound.Stop();
        }
    }
}
