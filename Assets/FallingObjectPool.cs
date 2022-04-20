using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjectPool : MonoBehaviour
{
    public int mNumberOfFallingObjects;
    public GameObject mFallingObjectPrefab;

    private void Awake()
    {
        for (int i = 0; i < mNumberOfFallingObjects; i++)
        {
            GameObject fallingObject = Instantiate(mFallingObjectPrefab, transform);
        }
    }

    public void DeactivateAllObjects()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}
