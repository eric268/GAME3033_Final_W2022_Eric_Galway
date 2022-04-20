using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIScript : MonoBehaviour
{
    TextMeshProUGUI mTimerText;
    public float mTimer;
    // Start is called before the first frame update
    void Start()
    {
        mTimer = 10.0f;    
    }

    // Update is called once per frame
    void Update()
    {
        mTimer -= Time.deltaTime / 60.0f;
        mTimerText.text = mTimer.ToString();
    }
}
