using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    private Animator mAnimator;
    public float mFallSize;

    public float mCurrentYPos;
    public float mStartingHeght = 50.0f;
    public float mFallenPosition = 0.0f;
    public bool mIsActive;
    readonly int fallingHash = Animator.StringToHash("FallSize");
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (mIsActive)
        {
            mCurrentYPos = transform.position.y;
            mFallSize = mCurrentYPos / mStartingHeght;
            mAnimator.SetFloat(fallingHash, mFallSize);


        }
    }

    private void OnEnable()
    {
        mIsActive = true;
    }

    private void OnDisable()
    {
        mIsActive = false;
    }
}
