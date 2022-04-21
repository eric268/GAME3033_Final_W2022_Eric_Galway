using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    public float mAimSensativity;
    public float mWalkSpeed;

    Vector2 mInputVector = Vector2.zero;
    Vector2 mLookInput = Vector2.zero;
    Vector3 mMoveDirection = Vector3.zero;

    PlayerController mPlayerController;
    Animator mAnimator;
    Rigidbody mRigidBody;

    public GameObject mFollowTransform;
    public GameObject mLeftHandIKPosition;
    public GameObject mRightHandIKPosition;

    readonly int isRunningHash = Animator.StringToHash("IsRunning");
    readonly int isWateringHash = Animator.StringToHash("IsWatering");
    // Start is called before the first frame update
    void Start()
    {
        mPlayerController = GetComponent<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidBody = GetComponent<Rigidbody>();
    }

    public void OnMovement(InputValue value)
    {
        if (mPlayerController.mIsWatering && mPlayerController.mInRangeOfFlower)
            return;
            mInputVector = value.Get<Vector2>();
        mAnimator.SetBool(isRunningHash, mPlayerController.mIsRunning);
    }

    public void OnLook(InputValue value)
    {
        mLookInput = value.Get<Vector2>();
    }

    public void OnWater(InputValue value)
    {
        if (mPlayerController.mIsWatering)
            return;

        mPlayerController.mIsWatering = value.isPressed;
        mAnimator.SetBool(isWateringHash, mPlayerController.mIsWatering);

        if (mPlayerController.mHasWater && mPlayerController.mInRangeOfFlower)
            mPlayerController.mFlower.GetComponentInChildren<FlowerScript>().mIsBeingWatered = true;
        WaterUsed();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mFollowTransform.transform.rotation *= Quaternion.AngleAxis(mLookInput.x * mAimSensativity, Vector3.up);
        mFollowTransform.transform.rotation *= Quaternion.AngleAxis(mLookInput.y * mAimSensativity, Vector3.left);

        var angles = mFollowTransform.transform.localEulerAngles;
        angles.z = 0;
        var angle = mFollowTransform.transform.localEulerAngles.x;


        if (angle < 10)
        {
            angles.x = 10;
        }
        else if (angle > 70)
        {
            angles.x = 70;
        }

        mFollowTransform.transform.localEulerAngles = angles;
        transform.rotation = Quaternion.Euler(0, mFollowTransform.transform.rotation.eulerAngles.y, 0);
        mFollowTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);

        mMoveDirection = transform.forward * mInputVector.y + transform.right * mInputVector.x;

        if ((mMoveDirection.magnitude > 0.1f))
        {
            mPlayerController.mIsRunning = true;
        }
        else
        {
            mPlayerController.mIsRunning = false;
        }
        mAnimator.SetBool(isRunningHash, mPlayerController.mIsRunning);

        Vector3 vec = mMoveDirection * mWalkSpeed * Time.deltaTime;

        mRigidBody.AddForce(vec, ForceMode.VelocityChange);

        mRigidBody.velocity *= 0.97f;
    }

    public void WaterUsed()
    {
        mPlayerController.mBucketWater.SetActive(false);
        mPlayerController.mHasWater = false;

        mPlayerController.mWateringCan.SetActive(true);
        mPlayerController.mBucket.SetActive(false);
    }

    public void WateringEnded()
    {
        mPlayerController.mIsWatering = false;
        mPlayerController.mWateringCan.SetActive(false);
        mPlayerController.mBucket.SetActive(true);
        mAnimator.SetBool(isWateringHash, mPlayerController.mIsWatering);
    }
}
