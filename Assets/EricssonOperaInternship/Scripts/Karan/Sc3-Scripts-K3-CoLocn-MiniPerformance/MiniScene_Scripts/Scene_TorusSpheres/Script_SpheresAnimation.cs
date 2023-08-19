using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_SpheresAnimation : MonoBehaviour
{
    private GameObject[] spheresArray;
    private float[] phaseShift = new float[9];

    private float speedUpDown = 0.002f;
    private float distanceUpDown = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        spheresArray = GameObject.FindGameObjectsWithTag("Sph");

        for (int i = 0; i < 9; i++)
        {
            phaseShift[i] = i * 2;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        for (int i = 0; i < 9; i++)
        {
            spheresArray[i].transform.position = new Vector3(spheresArray[i].transform.position.x, spheresArray[i].transform.position.y + (Mathf.Sin(speedUpDown * Time.time) * distanceUpDown), spheresArray[i].transform.position.z);
        }
    }
}
