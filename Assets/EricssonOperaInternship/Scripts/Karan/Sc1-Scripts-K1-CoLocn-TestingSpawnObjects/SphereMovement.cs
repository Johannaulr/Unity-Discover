using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMovement : MonoBehaviour
{

    private Vector3 startPosition;
    private float speedUpDown = 0.25f;
    private float distanceUpDown = 2;

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
