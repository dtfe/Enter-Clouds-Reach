using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class MainMenu : MonoBehaviour
{   
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
      audioSource.PlayOneShot(bSFX); 
      yield return new WaitForSeconds(bSFX.length);
      SceneManager.LoadScene(sceneName);
   }

}


