using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("CHARACTER_CREATOR");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        Debug.Log("Open Settings");
    }
}
