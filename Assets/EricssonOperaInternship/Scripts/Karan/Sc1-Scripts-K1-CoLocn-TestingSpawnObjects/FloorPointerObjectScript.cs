using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorPointerObjectScript : MonoBehaviour
{
    public TMP_Text FloorMarkerValuesObj;
    public TMP_Text DistanceValuesObj;
    public GameObject FloorMarkerObj;
    private float distanceFromCenter;
    private Vector3 OriginLocation;
    private Vector3 FloorMarkerObjSansYaxisLocation;

    // Start is called before the first frame update
    void Start()
    {
        FloorMarkerValuesObj.text = "Values";
        DistanceValuesObj.text = "...";
        OriginLocation = new Vector3(0, 0, 0);
        FloorMarkerObjSansYaxisLocation = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        FloorMarkerObj.transform.position = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, 1, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).z);
        FloorMarkerObjSansYaxisLocation = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).x, 0, OVRInput.GetLocalControllerPosition(OVRInput.Controller.LTouch).z);
    }

    // Update is called once per frame
    void Update()
    {
        
        FloorMarkerValuesObj.text = FloorMarkerObj.transform.position.ToString("F1");

        distanceFromCenter = Vector3.Distance(FloorMarkerObjSansYaxisLocation, OriginLocation);

        DistanceValuesObj.text = "Distance from Origin : " + distanceFromCenter.ToString("F1");
    }
    
}
