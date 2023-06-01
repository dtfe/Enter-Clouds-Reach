using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class ThreeGobModifier: MonoBehaviour
    {
        [SerializeField] private Unit unit;
        private void Start()
        {
            if(unit == null)
            {
                unit = GetComponent<Unit>();
            }

            if(PlayerPrefs.GetInt("3GobSneakAttack") == 1)
            {
                unit.addStatus(statusEffects.Stunned, 1);
            }

            if(PlayerPrefs.GetInt("3GobSneakFail") == 1)
            {
                unit.damage = 2;
            }
        }
    }
}
