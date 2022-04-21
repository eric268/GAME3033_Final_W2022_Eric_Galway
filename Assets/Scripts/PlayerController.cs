using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool mIsWatering;
    public bool mIsRunning;
    public bool mHasWater;
    public bool mInRangeOfFlower;
    public bool mWasHitByAnvil;
    public bool mGameIsPaused;

    public GameObject mBucket;
    public GameObject mBucketWater;
    public GameObject mWateringCan;
    public GameObject mFlower;

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
            other.gameObject.SetActive(false);
        }
        if (other.gameObject.CompareTag("Flower"))
        {
            mInRangeOfFlower = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Flower"))
        {
            mInRangeOfFlower = false;
        }
    }
}
