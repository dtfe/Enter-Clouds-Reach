using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class PlayerModifier : MonoBehaviour
    {
        [SerializeField] private Unit unit;
        // Start is called before the first frame update
        void Start()
        {
            if (unit == null)
            {
                unit = GetComponent<Unit>();
            }
            if (PlayerPrefs.GetInt("PotPoisonFailed") == 1)
            {
                unit.addStatus(statusEffects.Poison, 6);
                PlayerPrefs.DeleteKey("PotPoisonFailed");
            }
            if (PlayerPrefs.GetInt("PlayerStunned") == 1)
            {
                unit.addStatus(statusEffects.Stunned, 1);
                PlayerPrefs.DeleteKey("PlayerStunned");
            }

        }
    }
}
