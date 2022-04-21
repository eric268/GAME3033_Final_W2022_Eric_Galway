using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIScript : MonoBehaviour
{
    public TextMeshProUGUI mTimerText;
    public TextMeshProUGUI mScoreText;
    FallingObjectSpawner mFallingObjectSpawner;
    public float mTimer;
    public int mScore;
    public bool mPauseTimer;
    // Start is called before the first frame update
    void Start()
    {
        mFallingObjectSpawner = FindObjectOfType<FallingObjectSpawner>();
        mTimer = 10.0f;
        mScore = 0;
        InvokeRepeating(nameof(IncrementScore), 0.0f, 1.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        if (mPauseTimer)
            return;

        mTimer -= Time.deltaTime;

        if (mTimer <= 0.0f)
        {
            PlayerPrefs.SetInt("Score", mScore);
            PlayerPrefs.Save();
            SceneManager.LoadScene("GameOverScene");
        }
        mTimerText.text = "Time: " + mTimer.ToString("F");
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

    public void IncrementScore()
    {
        mScore++;
        mScoreText.text = "Score: " + mScore;

        if (mScore % 30 == 0 && mScore <= 120)
        {
            mFallingObjectSpawner.mChanceOfSpawningAnvil += 0.1f;
            mFallingObjectSpawner.mSpawnRate -= 0.1f;
        }
    }

    public void FlowerDied()
    {
        CancelInvoke();
        PlayerPrefs.SetInt("Score", mScore);
        PlayerPrefs.Save();
    }
}
