using UnityEngine;
using System.Collections;
using Fusion;
using Fusion.Sockets;
using Discover.Networking;
using System.Collections.Generic;
using System;

    public class DissolveController : NetworkBehaviour
    {
        [SerializeField, Tooltip("The Sphere Dissolve Portal")]
        private GameObject dissolveSphere;
        [SerializeField, Tooltip("The CenterEyeCamera Transform")]
        private Transform hmdTransform;
        [SerializeField, Tooltip("The speed at which the variable X pendulums between 0 and 1."), Range(0f, 1f)]
        private float animationSpeed = 1f;

        [SerializeField, Tooltip("Give a threshold value that affects Cutoff Height in case the portal isn't fully dissolving (Used for pingpong function only)")]
        private float portalSizeThreshold = 2f;

        [SerializeField, Tooltip("Value Cutoff Height in shader we want to achieve"), Range(-1f, 1f)]
        private float targetCutoffValue;

        private float cutoffValue;

        [Networked]
        public float networkedCutoffValue {
            get; set;
        }


        public void updateCutoffValue(float newValue)
        {
            targetCutoffValue = newValue;

        }


        private void Start()
        {
            float cutOff = dissolveSphere.gameObject.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");
            //hmdTransform = GameObject.Find("CenterEyeAnchor").transform;
        }
        public void FixedUpdate()
        {
        //animatePingPong();

                
                animatePortal();
            

            if (Input.GetKeyDown(KeyCode.M))
            {
                Debug.Log(networkedCutoffValue);
            }


            //this.gameObject.transform.position = hmdTransform.position;

        }

        private void animatePortal()
        {

            float currentCutoffValue = dissolveSphere.gameObject.GetComponent<Renderer>().material.GetFloat("_Cutoff_Height");

            if (currentCutoffValue <= -.55 || currentCutoffValue >= .55)
            {
                this.gameObject.transform.position = hmdTransform.position;
                this.gameObject.transform.eulerAngles = new Vector3(-90f + hmdTransform.eulerAngles.x, hmdTransform.eulerAngles.y, hmdTransform.eulerAngles.z);


            }

        if (Runner.IsSharedModeMasterClient)
        {
            networkedCutoffValue = targetCutoffValue;
            //Debug.Log("Updated networkCutout to: " + networkedCutoffValue);

        }

        if (Mathf.Abs(networkedCutoffValue) - Mathf.Abs(networkedCutoffValue) > 0.5f)
            {
                dissolveSphere.gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", Mathf.Lerp(currentCutoffValue, networkedCutoffValue * portalSizeThreshold, animationSpeed / 200));

            }
            else
            {
                dissolveSphere.gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", Mathf.Lerp(currentCutoffValue, networkedCutoffValue * portalSizeThreshold, animationSpeed / 50));
            }

        }

        private void animatePingPong()
        {
            cutoffValue = Mathf.PingPong(Time.time * animationSpeed, portalSizeThreshold) - portalSizeThreshold / 2;
            dissolveSphere.gameObject.GetComponent<Renderer>().material.SetFloat("_Cutoff_Height", cutoffValue);
        }
    }
