using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerScript : MonoBehaviour
{
    public bool mIsBeingWatered;
    public float mRegrowSpeed;
    bool mFlowerDied = false;
    Vector3 mStartingScale;
    UIScript mUIScript;

    // Start is called before the first frame update
    void Start()
    {
        mStartingScale = transform.localScale;
        mUIScript = FindObjectOfType<UIScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!mFlowerDied)
        {
            if (!mIsBeingWatered)
            {

                float decreaseAmount = Time.fixedDeltaTime * 2.5f;
                transform.localScale = new Vector3(transform.localScale.x - decreaseAmount, transform.localScale.y - decreaseAmount, transform.localScale.z - decreaseAmount);

                if (transform.localScale.x <= 0.0f)
                {
                    mFlowerDied = true;
                }
            }
            else
            {
                mUIScript.PauseTimer();
                transform.localScale = Vector3.Lerp(transform.localScale, mStartingScale, mRegrowSpeed);
                if (mStartingScale.magnitude - transform.localScale.magnitude <= 0.1f)
                {
                    transform.localScale = mStartingScale;
                    mIsBeingWatered = false;
                    mUIScript.ResetTimer();
                }
            }
        }
    }
}
