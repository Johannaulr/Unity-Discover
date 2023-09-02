using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_LocalInputHandler : MonoBehaviour
{
    [SerializeField]
    private int localinputInt;
    
    [SerializeField]
    private bool localinputBool;


    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        localinputInt = 0;
        localinputBool = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0.0f)
        {
            localinputInt = 2023;
            localinputBool = true;
        }
        else 
        {
            localinputInt = 0;
            localinputBool = false;
        }
    }

    public NetworkInputData GetInputForNetwork() 
    {
        var nwInput = new NetworkInputData();

        
        //nwInput.nidFloat = localinputBool;

        //localinputInt = 0;
        //localinputBool = false;

        return nwInput;
    }
}
