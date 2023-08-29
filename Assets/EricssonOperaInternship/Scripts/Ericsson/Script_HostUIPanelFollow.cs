using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_HostUIPanelFollow : MonoBehaviour
{

    public GameObject CameraObject;
    public GameObject HostUIPanel;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float step;

    // Start is called before the first frame update
    void Start()
    {
        HostUIPanel.transform.position = CameraObject.transform.position + new Vector3(-0.2f, -0.1f, 0.7f);
        step = 8.0f * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        step = 8.0f * Time.deltaTime;

        targetPosition = CameraObject.transform.position + new Vector3(-1f, -1f, 0f);
        targetRotation = Quaternion.LookRotation(HostUIPanel.transform.position - CameraObject.transform.position);

        HostUIPanel.transform.position = Vector3.Lerp(HostUIPanel.transform.position, targetPosition, step);
        HostUIPanel.transform.rotation = Quaternion.Slerp(HostUIPanel.transform.rotation, targetRotation, step);

        //HostUIPanel.transform.rotation = Quaternion.LookRotation(HostUIPanel.transform.position - CameraObject.transform.position);
    }


}