using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool mIsWatering;
    public bool mIsRunning;

    public GameObject mBucket;
    public GameObject mWateringCan;

    private void Start()
    {
        mWateringCan.SetActive(false);
    }
}
