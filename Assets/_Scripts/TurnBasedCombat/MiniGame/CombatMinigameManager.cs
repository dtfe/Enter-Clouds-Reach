using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EnterCloudsReach.Combat
{
    public class CombatMinigameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text hit;
        [SerializeField] private TMP_Text miss;
        private int hits;
        private int misses;

        public void Hit()
        {
            hits++;
            UpdateUI();
        }

        public void Miss()
        {
            misses++;
            UpdateUI();
        }

        private void UpdateUI()
        {
            hit.text = "Hits: " + hits;
            miss.text = "Miss: " + misses;
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
