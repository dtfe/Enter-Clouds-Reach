using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class CombatMinigameManager : MonoBehaviour
    {
        [SerializeField] private GameObject timingSpawner;
        private List<GameObject> timings = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {

        
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddTimingToList(GameObject timing)
        {
            timings.Add(timing);
        }

        public void StartMinigame()
        {

        }

        public void EndMinigame()
        {

        }

        public void TakeDamage()
        {

        }
    }
}
