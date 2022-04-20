using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool mIsWatering;
    public bool mIsRunning;
    public bool mHasWater;

    public GameObject mBucket;
    public GameObject mBucketWater;
    public GameObject mWateringCan;

    private void Start()
    {
        mWateringCan.SetActive(false);
        mBucketWater.SetActive(true);
        mHasWater = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            mBucketWater.SetActive(true);
            mHasWater = true;
        }
    }

    public void WaterUsed()
    {
        mBucketWater.SetActive(false);
        mHasWater = false;
    }
}
