using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("TUT_LEVEL");
    }
    public void EnterScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Settings()
    {
        Debug.Log("Open Settings");
    }
    public void PageTurn()
    {
        GameObject.Find("Continue").GetComponent<Button>().interactable = true;
        GameObject.Find("Pivot").GetComponent<Animator>().enabled = true;
    }
}
