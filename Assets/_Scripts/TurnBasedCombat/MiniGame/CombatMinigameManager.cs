using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace EnterCloudsReach.Combat
{
    public class CombatMinigameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text hit;
        [SerializeField] private TMP_Text miss;

        //Parameters
        [SerializeField] private MGLoader loader;
        [SerializeField] private MGSequence sequence;
        public bool isPlaying = false;

        //Minigame specific UI elements
        [SerializeField] private Image background;

        //Regular UI elements
        [SerializeField] private GameObject actions;
        [SerializeField] private GameObject curActions;
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
            background.gameObject.SetActive(true);
            actions.SetActive(false);
            curActions.SetActive(false);
            loader.LoadSequence(sequence);
            isPlaying = true;
        }

        public void EndMinigame()
        {
            actions.SetActive(true);
            curActions.SetActive(true);
            isPlaying = false;
        }

        public void SetSequence(MGSequence seq)
        {
            sequence = seq;
        }

        public void TakeDamage()
        {

        }
    }
}
