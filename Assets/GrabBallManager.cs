using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GrabBallManager : NetworkBehaviour
{
    [Networked(OnChanged = nameof(ManageGrabBalls))]
    private bool grabBallsActive { get; set; }
    private List<GameObject> grabBalls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        grabBallsActive = false;
        foreach (var grabBall in GameObject.FindGameObjectsWithTag("Grab Ball"))
        {
            grabBalls.Add(grabBall);
        }
        foreach (var grabBall in grabBalls)
        {
            foreach (Transform child in grabBall.transform)
            {
                if (child.gameObject.name == "Grab Ball")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }

    public void ShowGrabBallsButtonPressed()
    {
        if (!grabBallsActive)
        {
            grabBallsActive = true;
        }
    }
    
    public void ShowGrabBalls()
    {
        foreach (var grabBall in grabBalls)
        {
            foreach (Transform child in grabBall.transform)
            {
                if (child.gameObject.name == "Grab Ball")
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }

    public void HideGrabBallsButtonPressed()
    {
        if (grabBallsActive)
        {
            grabBallsActive = false;
        }
    }

    public void HideGrabBalls()
    {
        foreach (var grabBall in grabBalls)
        {
            foreach (Transform child in grabBall.transform)
            {
                if (child.gameObject.name == "Grab Ball")
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }


    public static void ManageGrabBalls(Changed<GrabBallManager> changed)
    {
        if (changed.Behaviour.grabBallsActive)
        {
            changed.Behaviour.ShowGrabBalls();
        }

        else
        {
            changed.Behaviour.HideGrabBalls();
        }
    }
}
