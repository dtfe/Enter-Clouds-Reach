using System.Collections;
using System.Collections.Generic;
using EnterCloudsReach.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnterCloudsReach.EventSystem
{
    [Event(false, 1, 1)]
    public class NextLevelEvent : EventClass
    {   
        public string sceneName;
        public override void StartEvent()
        {
          StartCoroutine(LoadYourAsyncScene());
        }
    

        IEnumerator LoadYourAsyncScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            // Wait until the asynchronous scene fully loads
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }
    }
}
