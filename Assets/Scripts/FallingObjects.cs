using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    private Animator mAnimator;
    private Rigidbody mRigidBody;
    public float mScale;
    public float mFallSize;

    public float mFallSpeed;

    public float mCurrentYPos;
    public float mStartingHeght = 50.0f;
    public float mFallenPosition = 0.0f;
    public bool mIsActive;

    public bool mShowDamagingAnvil = false;
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

            mRigidBody.AddForce(Vector3.down * mFallSpeed, ForceMode.Acceleration);
        }
    }

    private void OnEnable()
    {
        if (!mRigidBody)
            mRigidBody = GetComponent<Rigidbody>();

        mIsActive = true;
        transform.localScale = new Vector3(mScale, mScale, mScale);
        mFallSpeed += 1.0f;
        mFallSpeed = Mathf.Clamp(mFallSpeed, 0.0f, 100.0f);
        mRigidBody.velocity = Vector3.zero;
    }

    public void UpdateFallingSpeed(float speed)
    {
        mFallSpeed = speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mRigidBody.velocity = Vector3.zero;
            mShowDamagingAnvil = true;
        }
        else if (!mShowDamagingAnvil)
        {
            gameObject.SetActive(false);
        }
    }
}
