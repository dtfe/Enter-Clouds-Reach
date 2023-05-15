using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using EnterCloudsReach.GUI;
using EnterCloudsReach.Player;
[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{   
    [SerializeField]internal NavMeshPlayerController navPlayer;
    AudioSource audioSource;
    public AudioClip bSFX;
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    public void goToScene(string sceneName)
    { 
        if(!audioSource.isPlaying)
        {
            StartCoroutine(LoadScene(sceneName));
        }
    }

    IEnumerator LoadScene(string sceneName)
    {
        Debug.Log("Loading Scene" + sceneName);
        if(navPlayer != null)
        {
            yield return new WaitUntil(() => navPlayer.MovementFinished());
        }
        audioSource.PlayOneShot(bSFX); 
        if(sceneName == "MAIN_MENU_NEW")
        {
        GUI_Manager.DialogueBox.MainMenuReset();
        }
        yield return new WaitForSeconds(bSFX.length);
        SceneManager.LoadScene(sceneName);
        if (!SceneManager.GetSceneByName(sceneName).IsValid())
        {
            Debug.Log("Did not find Scene with name: " + sceneName);
        }
    }
    
}


