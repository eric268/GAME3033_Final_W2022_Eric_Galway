using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaindropFalling : MonoBehaviour
{
    public Animator mAnimator;
    public float mFallSize;
    readonly int fallingHash = Animator.StringToHash("FallSize");
    // Start is called before the first frame update
    void Start()
    {
        mAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        print(mFallSize);
        mAnimator.SetFloat(fallingHash, mFallSize);
    }
}
