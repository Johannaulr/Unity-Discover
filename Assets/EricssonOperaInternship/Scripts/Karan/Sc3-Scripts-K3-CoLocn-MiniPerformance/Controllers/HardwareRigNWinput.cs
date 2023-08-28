using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

// Include all rig parameters in an network input structure
public struct HardwareRigNWinput : INetworkInput
{
    public Vector3 headsetPosition;
    public Quaternion headsetRotation;
    //public Vector3 playAreaPosition;
    //public Quaternion playAreaRotation;
    //public Vector3 leftHandPosition;
    //public Quaternion leftHandRotation;
    //public Vector3 rightHandPosition;
    //public Quaternion rightHandRotation;
    //public HandCommand leftHandCommand;
    //public HandCommand rightHandCommand;
}
