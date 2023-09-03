using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Oculus.Interaction {
    public class GrabBall : MonoBehaviour
    {
        public GameObject attachedObject;
        //public GameObject attachedUIPanel;
        //public Vector3 uIPanelOffset;
        public Vector3 offset;

        [SerializeField, Tooltip("Lock position of attached object on X axis")]
        private bool x;
        [SerializeField, Tooltip("Lock position of attached object on Y axis")]
        private bool y;
        [SerializeField, Tooltip("Lock position of attached object on Z axis")]
        private bool z;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

            if (x)
            {
                attachedObject.transform.position = new Vector3(0, transform.position.y, transform.position.z) + offset;
            }

            if (y)
            {
                attachedObject.transform.position = new Vector3(transform.position.x, 0, transform.position.z) + offset;

            }

            if (z)
            {
                attachedObject.transform.position = new Vector3(transform.position.x, transform.position.y, 0) + offset;

            }

            else
            {
                attachedObject.transform.position = transform.position + offset;
            }
        }
    }
}

