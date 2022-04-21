using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
    UIScript mUIScript;
    public GameObject mFollowTransform;
    public GameObject mPauseMenu;

    AudioSource mAudioSource;

    readonly int isRunningHash = Animator.StringToHash("IsRunning");
    readonly int isWateringHash = Animator.StringToHash("IsWatering");
    readonly int isHitHash = Animator.StringToHash("IsHit");
    // Start is called before the first frame update
    void Start()
    {
        mUIScript = FindObjectOfType<UIScript>();
        mPauseMenu.gameObject.SetActive(false);
        mPlayerController = GetComponent<PlayerController>();
        mAnimator = GetComponent<Animator>();
        mRigidBody = GetComponent<Rigidbody>();
        mAudioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    public void OnMovement(InputValue value)
    {
        if ((mPlayerController.mIsWatering && mPlayerController.mInRangeOfFlower) || mPlayerController.mWasHitByAnvil)
        {
            return;
        }
            mInputVector = value.Get<Vector2>();
        mAnimator.SetBool(isRunningHash, mPlayerController.mIsRunning);
    }

    public void OnLook(InputValue value)
    {
        if (mPlayerController.mWasHitByAnvil)
            return;

        mLookInput = value.Get<Vector2>();
    }

    public void OnWater(InputValue value)
    {
        if (mPlayerController.mIsWatering || mPlayerController.mWasHitByAnvil)
            return;

        mPlayerController.mIsWatering = value.isPressed;
        mAnimator.SetBool(isWateringHash, mPlayerController.mIsWatering);

        if (mPlayerController.mHasWater && mPlayerController.mInRangeOfFlower)
        {
            mRigidBody.velocity = Vector3.zero;
            mPlayerController.mFlower.GetComponentInChildren<FlowerScript>().mIsBeingWatered = true;
        }
        WaterUsed();
    }

    public void OnPause(InputValue value)
    {
        mPlayerController.mGameIsPaused = !mPlayerController.mGameIsPaused;

        if (mPlayerController.mGameIsPaused)
        {
            Time.timeScale = 0.0f;
            mPauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1.0f;
            mPauseMenu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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

        if (!(mPlayerController.mIsWatering && mPlayerController.mInRangeOfFlower))
        {
            Vector3 vec = mMoveDirection * mWalkSpeed * Time.deltaTime;
            mRigidBody.AddForce(vec, ForceMode.VelocityChange);
        }
        mRigidBody.velocity *= 0.95f;
    }

    public void WaterUsed()
    {
        mPlayerController.mWateringCan.SetActive(true);
        mPlayerController.mBucket.SetActive(false);
        mPlayerController.mBucketWater.SetActive(false);
        mPlayerController.mHasWater = false;
    }

    public void WateringEnded()
    {
        mPlayerController.mIsWatering = false;
        mPlayerController.mWateringCan.SetActive(false);
        mPlayerController.mBucket.SetActive(true);
        mAnimator.SetBool(isWateringHash, mPlayerController.mIsWatering);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Anvil"))
        {
            mPlayerController.mWasHitByAnvil = true;
            mAnimator.SetBool(isHitHash, true);
            mRigidBody.constraints = RigidbodyConstraints.FreezePositionX;
            mRigidBody.constraints = RigidbodyConstraints.FreezePositionY;
            mRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;

            mRigidBody.constraints = RigidbodyConstraints.FreezeRotationX;
            mRigidBody.constraints = RigidbodyConstraints.FreezeRotationY;
            mRigidBody.constraints = RigidbodyConstraints.FreezeRotationZ;

            mAudioSource.Play();
            mFollowTransform.isStatic = true;
            Invoke(nameof(OpenGameOverLevel), 2.0f);
        }
    }

    private void OpenGameOverLevel()
    {
        PlayerPrefs.SetInt("Score",mUIScript.mScore);
        PlayerPrefs.Save();
        SceneManager.LoadScene("GameOverScene");
    }
}
