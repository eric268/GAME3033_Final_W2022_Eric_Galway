using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator mAnimator;
    readonly int isRunningHash = Animator.StringToHash("IsRunning");
    readonly int isWateringHash = Animator.StringToHash("IsWatering");

    public bool mIsWatering;
    public bool mIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        mAnimator.SetBool(isRunningHash, mIsRunning);
        mAnimator.SetBool(isWateringHash, mIsWatering);
    }
}
