using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SlidesManager : NetworkBehaviour
{
    public List<Sprite> slidesCollection = new List<Sprite>();
    public GameObject activeSlide;
    //private NetworkRunner sessionRunner;

    private int activeSlideIndexPrivate;
    public int activeSlideIndexPublic;

    [Networked(OnChanged = nameof(NetworkSlideIndexChanged))]
    public int networkedActiveSlideIndex { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        networkedActiveSlideIndex = 0;
        activeSlideIndexPrivate = 0;
        activeSlideIndexPublic = 0;
    }


    // PlayerRef = IsSharedModeMasterClient
    // Update is called once per frame
    public override void FixedUpdateNetwork()
    {
        //Next slide
        if (OVRInput.GetDown(OVRInput.Button.One) && Runner.IsSharedModeMasterClient)
        {
            activeSlideIndexPublic += 1;

            if (activeSlideIndexPublic > slidesCollection.Count - 1)
            {
                activeSlideIndexPublic = slidesCollection.Count - 1;
            }
            networkedActiveSlideIndex = activeSlideIndexPublic;
        }

        //Previous slide
        if (OVRInput.GetDown(OVRInput.Button.Two) && Runner.IsSharedModeMasterClient)
        {
            activeSlideIndexPublic -= 1;

            if (activeSlideIndexPublic < 0)
            {
                activeSlideIndexPublic = 0;
            }
            networkedActiveSlideIndex = activeSlideIndexPublic;
        }
    }

    public void ChangeSlide()
    {
        activeSlide.GetComponent<Image>().sprite = slidesCollection[activeSlideIndexPrivate];
        Debug.Log("Changing to slide " + activeSlideIndexPrivate);
        Debug.Log("Networked index is " + networkedActiveSlideIndex);
    }
    
    public static void NetworkSlideIndexChanged(Changed<SlidesManager> changed)
    {
       changed.Behaviour.activeSlideIndexPrivate = changed.Behaviour.networkedActiveSlideIndex;
       changed.Behaviour.ChangeSlide();
    }
}
