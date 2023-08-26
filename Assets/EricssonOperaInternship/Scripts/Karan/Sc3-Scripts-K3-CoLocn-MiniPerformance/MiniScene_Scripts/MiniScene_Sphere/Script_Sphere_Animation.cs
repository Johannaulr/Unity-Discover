using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Sphere_Animation : MonoBehaviour
{
    private Vector3 startPosition;
    private float speedUpDown = 0.5f;
    private float distanceUpDown = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = startPosition + new Vector3(0.0f, Mathf.Sin(speedUpDown * Time.time) * distanceUpDown, 0.0f);
    }
}
