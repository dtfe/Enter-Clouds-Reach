using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class MinigameTimingSpawner : MonoBehaviour
    {
        public void SpawnTiming(GameObject timing)
        {
            GameObject go = Instantiate(timing);
        }
    }
}
