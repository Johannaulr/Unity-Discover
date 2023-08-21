using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidesManager : MonoBehaviour
{
    public Canvas slidesCanvas;
    private List<GameObject> slidesCollection;
    private int m_newActiveSlideIndex;
    private int m_oldActiveSlideIndex;

    // Start is called before the first frame update
    void Start()
    {
        m_newActiveSlideIndex = 0;
        var panel = slidesCanvas.transform.GetChild(0);

        foreach (Transform slide in panel)
        {
            slidesCollection.Add(slide.gameObject);
        }

        slidesCollection[m_newActiveSlideIndex].SetActive(true);
        m_oldActiveSlideIndex = m_newActiveSlideIndex;

    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            m_newActiveSlideIndex += 1;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            m_newActiveSlideIndex -= 1;
        }
    }

    private void ChangeSlide(int newActiveSlide, int oldActiveSlide)
    {
        slidesCollection[newActiveSlide].SetActive(true);

    }
}
