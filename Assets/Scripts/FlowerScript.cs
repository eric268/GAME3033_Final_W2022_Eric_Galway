using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    bool mIsCollidingWithPlayer;
    bool mIsBeingWatered;
    float mStartingScale;
    float mRegrowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        mStartingScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (!mIsBeingWatered)
        {
            float decreaseAmount = /*mStartingScale **/ Time.deltaTime /10.0f;
            transform.localScale = new Vector3(transform.localScale.x - decreaseAmount, transform.localScale.y- decreaseAmount, transform.localScale.z - decreaseAmount);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, mRegrowSpeed);
            if (transform.localScale == Vector3.one)
            {
                mIsBeingWatered = false;
            }
        }
    }
}
