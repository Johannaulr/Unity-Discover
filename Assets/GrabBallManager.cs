using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class GrabBallManager : NetworkBehaviour
{
    private List<GameObject> grabBalls = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
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
}
