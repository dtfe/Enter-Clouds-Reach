using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using EnterCloudsReach.Player;

namespace EnterCloudsReach.Combat
{
    public enum statusEffects
    {
        None,
        Stunned,
        Bleed,
        Poison,

    }

    [System.Serializable]
    public class DefendTimings
    {
        public GameObject timing = null;
        public int timingWeight = 1;
    }

    public class Unit : MonoBehaviour
    {
        private PlayerStats playerStats;
        public string unitName;
        private Animator anim;

        public MGSequence defaultSequence;

        public DefendTimings[] defendTimings = new DefendTimings[1];

        public MGSequence[] sequences = new MGSequence[1];

        [Header("Health Parameters")]
        public int maxHP;
        public int curHP;

        [Header("Damage Parameters")]
        public int damage = 1;

        [Header("Status Effects")]
        public bool stunnedImmune;
        public int stunned;

        public bool bleedImmune;
        public int bleed;

        public bool poisonImmune;
        public int poison;
        public bool player;
        CombatSFX unitNoise;
        float t;
        float randT;

        private void Awake()
        {
            if (sequences[0] == null)
            {
                sequences[0] = defaultSequence;
            }
        }

        private void Start()
        {
            playerStats = FindObjectOfType<PlayerStatDDOL>().playerStats;
            if (player)
            {
                unitName = playerStats.playerName;
            }
            unitNoise = gameObject.GetComponent<CombatSFX>();
            t = 0;
            randT = Random.Range(1, 11);
            anim = GetComponentInChildren<Animator>();
        }
        void Update()
        {
            if (!player && unitNoise != null)
            {
                t += Time.deltaTime;
                if (!unitNoise.CheckIfAudioPlay && t >= randT)
                {
                    unitNoise.AmbientNoises();
                    t = 0;
                    randT = Random.Range(20, 30);
                }
            }
        }

        public int takeDamage(int damage)
        {
            anim.SetTrigger("damaged");
            curHP -= damage;
            GetComponentInChildren<BattleHUD>().SetHP(curHP);
            return damage;
        }

        public void addStatus(statusEffects statusToAdd, int amount)
        {
            switch (statusToAdd)
            {
                default:
                    break;

                case statusEffects.Stunned:
                    stunned += amount;
                    break;

                case statusEffects.Bleed:
                    bleed += amount;
                    break;

                case statusEffects.Poison:
                    poison += amount;
                    break;


            }

            GetComponentInChildren<BattleHUD>().refreshStatus(this);
        }

        public void animationStart(string animName) => anim.SetTrigger(animName);
    }
}