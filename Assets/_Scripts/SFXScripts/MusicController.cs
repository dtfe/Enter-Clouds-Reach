using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace EnterCloudsReach.Audio
{
    public class MusicController : MonoBehaviour
    {
        public static MusicController musicController {get; private set;}
        [SerializeField] AudioMixer mixer;
        // Start is called before the first frame update
        void Awake()
        {
            if(musicController == null)
            {
                musicController = this;
            }
            else
            {
                Destroy(this);
            }
        }
        public void ExplorationMusic()
        {
            StartCoroutine(FadeMixerGroup.StartFade(mixer,"Exploration",1.5f,1));
            StartCoroutine(FadeMixerGroup.StartFade(mixer,"Combat",0.5f,0));
        }
        
        public void CombatMusic()
        {
            StartCoroutine(FadeMixerGroup.StartFade(mixer,"Combat",1,1));
            StartCoroutine(FadeMixerGroup.StartFade(mixer,"Exploration",1,0));
        }
    }
}
