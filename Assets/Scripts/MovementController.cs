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
        mInputVector = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        mLookInput = value.Get<Vector2>();
    }

    public void OnWater(InputValue value)
    {
        mPlayerController.mIsWatering = value.isPressed;
        mAnimator.SetBool(isWateringHash, mPlayerController.mIsWatering);

        if (mPlayerController.mIsWatering)
        {
            mPlayerController.mWateringCan.SetActive(true);
            mPlayerController.mBucket.SetActive(false);
        }
        else
        {
            mPlayerController.mWateringCan.SetActive(false);
            mPlayerController.mBucket.SetActive(true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mFollowTransform.transform.rotation *= Quaternion.AngleAxis(mLookInput.x * mAimSensativity, Vector3.up);
        mFollowTransform.transform.rotation *= Quaternion.AngleAxis(mLookInput.y * mAimSensativity, Vector3.left);

        var angles = mFollowTransform.transform.localEulerAngles;
        angles.z = 0;
        var angle = mFollowTransform.transform.localEulerAngles.x;


        if (angle > 180 && angle < 340)
        {
            angles.x = 340;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        mFollowTransform.transform.localEulerAngles = angles;
        transform.rotation = Quaternion.Euler(0, mFollowTransform.transform.rotation.eulerAngles.y, 0);
        mFollowTransform.transform.localEulerAngles = new Vector3(angles.x, 0, 0);


        if (!(mInputVector.magnitude > 0))
        {
            mMoveDirection = Vector3.zero;
        }
        else
        {
            mAnimator.SetBool(isRunningHash, mPlayerController.mIsRunning);
        }

        mMoveDirection = transform.forward * mInputVector.y + transform.right * mInputVector.x;

        Vector3 vec = mMoveDirection * mWalkSpeed * Time.deltaTime;

        mRigidBody.AddForce(vec, ForceMode.VelocityChange);

        //Assist in slowing player 
        mRigidBody.velocity *= 0.99f;
    }
}
