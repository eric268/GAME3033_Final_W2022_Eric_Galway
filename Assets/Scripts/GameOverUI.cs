using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI mScoreText;
    
    int mScore;
    // Start is called before the first frame update
    void Start()
    {
        mScore = PlayerPrefs.GetInt("Score");
        mScoreText.text = "Final Score: " + mScore;
    }
}
