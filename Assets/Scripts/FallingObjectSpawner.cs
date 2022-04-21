using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectSpawner : MonoBehaviour
{
    public GameObject mAnvilParent;
    public GameObject mWaterDropletParent;

    public FallingObjects[] mAnvilArray;
    public FallingObjects[] mDropletArray;

    private int mAnvilIndex;
    private int mDropletIndex;
    public int mMaxAnvilAmount;
    public int mMaxDropletAmount;

    public float mMinXBounds;
    public float mMaxXBounds;
    public float mMinZBounds;
    public float mMaxZBounds;

    public float mChanceOfSpawningAnvil = 0.1f;
    public float mSpawnRate;

    public float mDropletScale = 100.0f;
    public float mAnvilScale = 100.0f;

    public float mYPos = 50.0f;
    // Start is called before the first frame update
    void Start()
    {
        mAnvilIndex = 0;
        mDropletIndex = 0;

        mAnvilArray = mAnvilParent.GetComponentsInChildren<FallingObjects>();
        mDropletArray = mWaterDropletParent.GetComponentsInChildren<FallingObjects>();

        mMaxAnvilAmount = mAnvilArray.Length;
        mMaxDropletAmount = mDropletArray.Length;

        mAnvilParent.GetComponent<FallingObjectPool>().DeactivateAllObjects();
        mWaterDropletParent.GetComponent<FallingObjectPool>().DeactivateAllObjects();

        InvokeRepeating(nameof(Spawn), 1.0f, mSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        float chance = Random.Range(0.0f, 1.0f);
        float xPos = Random.Range(mMinXBounds, mMaxXBounds);
        float zPos = Random.Range(mMinZBounds, mMaxZBounds);

        if (chance <= 0.1f)
        {
            mAnvilArray[mAnvilIndex].gameObject.SetActive(true);
            mAnvilArray[mAnvilIndex++].gameObject.transform.position = new Vector3(xPos, mYPos, zPos);
            if (mAnvilIndex >= mMaxAnvilAmount)
            {
                mAnvilIndex = 0;
            }
        }
        else
        {
            mDropletArray[mDropletIndex].gameObject.SetActive(true);
            mDropletArray[mDropletIndex++].gameObject.transform.position = new Vector3(xPos, mYPos, zPos);
            if (mDropletIndex >= mMaxDropletAmount)
            {
                mDropletIndex = 0;
            }
        }
    }
}
