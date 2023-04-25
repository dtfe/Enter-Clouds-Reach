using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace EnterCloudsReach.Player
{
    public class ExplorationHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text healthTxt;
        private int health;
        private PlayerStats playerStats;
        // Start is called before the first frame update
        void Start()
        {
            if(playerStats == null)
            {
                playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
            }
            if(slider == null)
            {
                slider = gameObject.GetComponentInChildren<Slider>();
            }
            if(healthTxt == null)
            {
                healthTxt = gameObject.GetComponentInChildren<TMP_Text>();
            }
            healthTxt.SetText(health + "/" + slider.maxValue);
        }

        // Update is called once per frame
        void Update()
        {
            if(slider.maxValue != playerStats.maxHealth)
            {
                slider.maxValue = playerStats.maxHealth;
            }
            if(health != playerStats.health)
            {   
                health = playerStats.health;
                healthTxt.SetText(health + "/" + slider.maxValue);
            }
            if(slider.value != health)
            { 
                slider.value = health;
                healthTxt.SetText(health + "/" + slider.maxValue);
            }

        }
    }
}
