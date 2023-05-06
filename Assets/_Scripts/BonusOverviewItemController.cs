using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EnterCloudsReach.Inventory
{
    public class BonusOverviewItemController : MonoBehaviour
    {
        [SerializeField] public Item itemReference;

        [Header("TextFields")]
        [SerializeField] private TMP_Text bonusOriginName;
        [SerializeField] private TMP_Text brawn;
        [SerializeField] private TMP_Text agility;
        [SerializeField] private TMP_Text endurance;
        [SerializeField] private TMP_Text knowledge;
        [SerializeField] private TMP_Text wisdom;
        [SerializeField] private TMP_Text charm;

        public void ApplyItem(Item item)
        {
            itemReference = item;

            bonusOriginName.text = itemReference.itemName;

            #region Brawn
            brawn.text = "Brawn: ";
            if (itemReference.Brawn != 0)
            {
                if(itemReference.Brawn > 0)
                {
                    brawn.text += "+";
                }
                brawn.text += itemReference.Brawn;
            }
            else
            {
                Destroy(brawn.gameObject);
            }
            #endregion

            #region Agility
            agility.text = "Agility: ";
            if (itemReference.Agility != 0)
            {
                if (itemReference.Agility > 0)
                {
                    agility.text += "+";
                }
                agility.text += itemReference.Agility;
            }
            else
            {
                Destroy(agility.gameObject);
            }
            #endregion

            #region Endurance
            endurance.text = "Endurance: ";
            if (itemReference.Endurance != 0)
            {
                if (itemReference.Endurance > 0)
                {
                    endurance.text += "+";
                }
                endurance.text += itemReference.Endurance;
            }
            else
            {
                Destroy(endurance.gameObject);
            }
            #endregion

            #region Knowledge
            knowledge.text = "Knowledge: ";
            if (itemReference.Knowledge != 0)
            {
                if (itemReference.Knowledge > 0)
                {
                    knowledge.text += "+";
                }
                knowledge.text += itemReference.Knowledge;
            }
            else
            {
                Destroy(knowledge.gameObject);
            }
            #endregion

            #region Wisdom
            wisdom.text = "Wisdom: ";
            if (itemReference.Wisdom != 0)
            {
                if (itemReference.Wisdom > 0)
                {
                    wisdom.text += "+";
                }
                wisdom.text += itemReference.Wisdom;
            }
            else
            {
                Destroy(wisdom.gameObject);
            }
            #endregion

            #region Charm
            charm.text = "Charm: ";
            if (itemReference.Charm != 0)
            {
                if (itemReference.Charm > 0)
                {
                    charm.text += "+";
                }
                charm.text += itemReference.Charm;
            }
            else
            {
                Destroy(charm.gameObject);
            }
            #endregion
        }
    }
}
