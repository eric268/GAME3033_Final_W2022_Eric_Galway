using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI mTimerText;
    public float mTimer;
    public bool mPauseTimer;
    // Start is called before the first frame update
    void Start()
    {
        mTimer = 10.0f;    
    }

    // Update is called once per frame
    void Update()
    {
        if (mPauseTimer)
            return;

        mTimer -= Time.deltaTime;

        if (mTimer <= 0.0f)
        {
            mTimer = 0.0f;
            mTimerText.text = mTimer.ToString();
            return;
        }
        mTimerText.text = mTimer.ToString();
    }

    public void PauseTimer()
    {
        mPauseTimer = true;
        mTimer = 10.0f;
        mTimerText.text = mTimer.ToString();
    }

    public void ResetTimer()
    {
        mPauseTimer = false;
        mTimerText.text = mTimer.ToString();
    }
}
