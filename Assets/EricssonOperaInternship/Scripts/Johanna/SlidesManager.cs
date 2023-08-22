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

    public int activeSlideIndex;
    [Networked(OnChanged =nameof(NetworkSlideIndexChanged))]
    private int networkedActiveSlideIndex { get; set; }
    private UnityEvent slideChangeEvent;
    

    
    // Start is called before the first frame update
    void Start()
    {
        activeSlideIndex = 0;

        ChangeSlide();

        if (slideChangeEvent == null)
            slideChangeEvent = new UnityEvent();

        slideChangeEvent.AddListener(ChangeSlide);
    }

    // Update is called once per frame
    void Update()
    {
        //Next slide
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            activeSlideIndex += 1;

            if (activeSlideIndex > slidesCollection.Count - 1)
            {
                activeSlideIndex = slidesCollection.Count - 1;
            }
            slideChangeEvent.Invoke();
        }

        //Previous slide
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            activeSlideIndex -= 1;

            if (activeSlideIndex < 0)
            {
                activeSlideIndex = 0;
            }
            slideChangeEvent.Invoke();
        }

    }

    public void ChangeSlide()
    {
        activeSlide.GetComponent<Image>().sprite = slidesCollection[activeSlideIndex];
        Debug.Log("Changing to slide " + activeSlideIndex);
    }
    
    public static void NetworkSlideIndexChanged(Changed<SlidesManager> changed)
    {
       changed.Behaviour.activeSlideIndex = changed.Behaviour.networkedActiveSlideIndex;
       //slideChangeEvent.Invoke();
    }

}
