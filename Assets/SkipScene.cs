using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class SkipScene : MonoBehaviour
{
    public PlayableDirector playableDirector;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var timeline = (TimelineAsset)playableDirector.playableAsset;
            playableDirector.time = timeline.duration;
        }
    }
}

