using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCan : MonoBehaviour
{
    public Material mRed;
    public Material mBlue;

    public GameObject mWater;
    public PlayerController mPlayerController;

    private void OnEnable()
    {
        if (mPlayerController.mHasWater)
        {
            mWater.GetComponent<MeshRenderer>().material = mBlue;
            //mPlayerController.mHasWater = false;
        }
        else
        { 
            mWater.GetComponent<MeshRenderer>().material = mRed;
        }
    }
}
