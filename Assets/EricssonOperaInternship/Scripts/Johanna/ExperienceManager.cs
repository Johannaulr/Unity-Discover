using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public GameObject portal;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnPortal()
    {
        portal.SetActive(true);
    }

    public void TriggerVR()
    {

    }


}
