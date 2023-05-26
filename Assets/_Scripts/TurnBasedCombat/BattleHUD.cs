using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace EnterCloudsReach.Combat
{
    public class BattleHUD : MonoBehaviour
    {

        public TMP_Text nameText;
        public Slider hpSlider;

        public GameObject statusEffects;

        [Header("Stun Params")]
        [SerializeField] private Sprite stunTexture;
        [SerializeField] private Sprite stunImmunityTexture;
        [SerializeField] private GameObject stunned;
        [SerializeField] private Image stunnedImage;
        [SerializeField] private TMP_Text StunnedText;

        [Header("Blood Params")]
        [SerializeField] private Sprite bloodTexture;
        [SerializeField] private Sprite bloodImmunityTexture;
        [SerializeField] private GameObject bleed;
        [SerializeField] private Image bleedImage;
        public GameObject bloodPS;
        [SerializeField] private TMP_Text bleedText;

        [Header("Poison Params")]
        [SerializeField] private Sprite poisonTexture;
        [SerializeField] private Sprite poisonImmunityTexture;
        [SerializeField] private GameObject poison;
        [SerializeField] private Image poisonImage;
        [SerializeField] private TMP_Text poisonText;


        private void Start()
        {
            nameText = transform.Find("Name").GetComponent<TMP_Text>();
            hpSlider = transform.Find("Health").GetComponent<Slider>();
            statusEffects = transform.Find("Status Effects").gameObject;
            #region status Effect Params
            #endregion status Effect Params
        }

        public void SetHUD(Unit unit)
        {
            nameText.text = unit.unitName;
            hpSlider.maxValue = unit.maxHP;
            hpSlider.minValue = 0;
            hpSlider.value = unit.curHP;
            refreshStatus(unit);
        }

        public void SetHP(int hp)
        {
            hpSlider.value = hp;
            if (hp <= 0)
            {
                hpSlider.transform.Find("Fill Area").gameObject.SetActive(false);
            }
        }

        public void refreshStatus(Unit unit)
        {
            if (unit.stunnedImmune)
            {
                if (stunned.GetComponent<Image>().sprite != Resources.Load<Sprite>("StunImmunityIcon.png"))
                {
                    stunned.GetComponent<Image>().sprite = Resources.Load<Sprite>("StunImmunityIcon");
                    StunnedText.gameObject.SetActive(false);
                }
                stunned.SetActive(true);
            }
            else
            {
                StunnedText.text = unit.stunned.ToString();
                if (unit.stunned != 0)
                {
                    stunned.SetActive(true);
                }
                else
                {
                    stunned.SetActive(false);
                }
            }

            if (unit.bleedImmune)
            {
                if (bleed.GetComponent<Image>().sprite != Resources.Load<Sprite>("BleedImmunityIcon"))
                {
                    bleed.GetComponent<Image>().sprite = Resources.Load<Sprite>("BleedImmunityIcon");
                    bleedText.gameObject.SetActive(false);
                }
                bleed.SetActive(true);
            }
            else
            {
                bleedText.text = unit.bleed.ToString();
                if (unit.bleed != 0)
                {
                    bleed.SetActive(true);
                    //bloodPS.SetActive(true);
                }
                else
                {
                    bleed.SetActive(false);
                    //bloodPS.SetActive(false);
                }
            }

            if (unit.poisonImmune)
            {
                if (poison.GetComponent<Image>().sprite != Resources.Load<Sprite>("PoisonImmunityIcon"))
                {
                    poison.GetComponent<Image>().sprite = Resources.Load<Sprite>("PoisonImmunityIcon");
                    poisonText.gameObject.SetActive(false);
                }
                poison.SetActive(true);
            }
            else
            {
                poisonText.text = unit.poison.ToString();
                if (unit.poison != 0)
                {
                    poison.SetActive(true);
                }
                else
                {
                    poison.SetActive(false);
                }
            }
        }
    }
}