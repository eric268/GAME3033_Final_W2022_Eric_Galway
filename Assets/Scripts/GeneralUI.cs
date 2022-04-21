using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GeneralUI : MonoBehaviour
{
    public Canvas mPauseCanvas;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void OnPlayPressed()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnInstructionsPressed()
    {

        SceneManager.LoadScene("InstructionsScene");
    }

    public void OnResumePressed()
    {
        mPauseCanvas.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OnMainMenuPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
