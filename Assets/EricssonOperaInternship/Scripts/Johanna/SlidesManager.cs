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

    public int activeSlideIndex = 0;


    [Networked(OnChanged = nameof(NetworkSlideIndexChanged))]
    private int networkedActiveSlideIndex
    {
        get
        {
            return networkedActiveSlideIndex;
        }
        set { networkedActiveSlideIndex = activeSlideIndex; }
    }

    private UnityEvent slideChangeEvent;


    // Start is called before the first frame update
    void Start()
    {
        //networkedActiveSlideIndex = 0;

        //ChangeSlide();

        if (slideChangeEvent == null)
            slideChangeEvent = new UnityEvent();

        slideChangeEvent.AddListener(ChangeSlide);
    }

    // Update is called once per frame
    void Update()
    {
        //Next slide
        if (OVRInput.GetDown(OVRInput.Button.One) || Input.GetKeyDown("s"))
        {
            activeSlideIndex += 1;

            if (activeSlideIndex > slidesCollection.Count - 1)
            {
                activeSlideIndex = slidesCollection.Count - 1;
            }
            //slideChangeEvent.Invoke();
            ChangeSlide();
        }

        //Previous slide
        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown("w"))
        {
            activeSlideIndex -= 1;

            if (activeSlideIndex < 0)
            {
                activeSlideIndex = 0;
            }
            //slideChangeEvent.Invoke();
            ChangeSlide();
        }


    }

    public void ChangeSlide()
    {
        activeSlide.GetComponent<Image>().sprite = slidesCollection[activeSlideIndex];
        Debug.Log("Changing to slide " + activeSlideIndex);
        Debug.Log("Networked index is " + networkedActiveSlideIndex);
    }
    
    public static void NetworkSlideIndexChanged(Changed<SlidesManager> changed)
    {
       changed.Behaviour.networkedActiveSlideIndex = changed.Behaviour.activeSlideIndex;
        //slideChangeEvent.Invoke();

    }

}
