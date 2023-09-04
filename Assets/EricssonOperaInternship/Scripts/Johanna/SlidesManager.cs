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

    [Networked(OnChanged = nameof(ManageSlides))]
    public bool slidesActive { get; set; }

    private Image slideImage;
    //private NetworkRunner sessionRunner;

    private int activeSlideIndex;
    //private int activeSlideIndexPrivate;
    //public int activeSlideIndexPublic;


    public void previousSlide()
    {
        networkedActiveSlideIndex -= 1;
        activeSlideIndex -= 1;

        if (networkedActiveSlideIndex < 0)
        {
            activeSlideIndex = 0;
            networkedActiveSlideIndex = 0;
        }

    }

    public void nextSlide()
    {
        networkedActiveSlideIndex += 1;
        activeSlideIndex += 1;

        if (networkedActiveSlideIndex > slidesCollection.Count - 1)
        {
            networkedActiveSlideIndex = slidesCollection.Count - 1;
            activeSlideIndex = slidesCollection.Count - 1;
        }
    }


    [Networked(OnChanged = nameof(NetworkSlideIndexChanged))]
    private int networkedActiveSlideIndex { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        slideImage = activeSlide.GetComponent<Image>();
        slideImage.enabled = false;
        //networkedActiveSlideIndex = 0;
        //activeSlideIndexPrivate = 0;
        //activeSlideIndexPublic = 0;
        activeSlideIndex = 0;
    }

    // PlayerRef = IsSharedModeMasterClient

    // Update is called once per frame
   /* public override void FixedUpdateNetwork()
    {
        //Next slide
        if (OVRInput.GetUp(OVRInput.Button.One) && Runner.IsSharedModeMasterClient)
        {
            //activeSlideIndexPublic += 1;
            activeSlideIndex += 1;

            Debug.Log(activeSlideIndex);
            
            if (activeSlideIndexPublic > slidesCollection.Count - 1)
            {
                activeSlideIndexPublic = slidesCollection.Count - 1;
            }
            networkedActiveSlideIndex = activeSlideIndexPublic;
            

            if (activeSlideIndex > slidesCollection.Count - 1)
            {
                activeSlideIndex = slidesCollection.Count - 1;
            }

            networkedActiveSlideIndex = activeSlideIndex;
        }

        //Previous slide
        if (OVRInput.GetUp(OVRInput.Button.Two) && Runner.IsSharedModeMasterClient)
        {
            //activeSlideIndexPublic -= 1;
            activeSlideIndex -= 1;

            if (activeSlideIndexPublic < 0)
            {
                activeSlideIndexPublic = 0;
            }
            networkedActiveSlideIndex = activeSlideIndexPublic;
            
            if (activeSlideIndex < 0)
            {
                activeSlideIndex = 0;
            }

            networkedActiveSlideIndex = activeSlideIndex;
        }

    }
   */

    public void ChangeSlide()
    {
        activeSlide.GetComponent<Image>().sprite = slidesCollection[networkedActiveSlideIndex];
        Debug.Log("Changing to slide " + networkedActiveSlideIndex);
    }
    
    public static void NetworkSlideIndexChanged(Changed<SlidesManager> changed)
    {
       changed.Behaviour.ChangeSlide();
    }

    public void StartPresentationButtonPressed()
    {
        slidesActive = true;

    }

    public void StartPresentation()
    {
        if (!slideImage.enabled)
        {
            slideImage.enabled = true;
        }
    }

    public void EndPresentationButtonPressed()
    {
        slidesActive = false;
    }

    public void EndPresentation()
    {
        if (slideImage.enabled)
        {
            slideImage.enabled = false;
        }
    }

    public static void ManageSlides(Changed<SlidesManager> changeVariable)
    {

        if (changeVariable.Behaviour.slidesActive)
        {
            changeVariable.Behaviour.StartPresentation();
        }

        else
        {
            changeVariable.Behaviour.EndPresentation();
        }
    }
}
