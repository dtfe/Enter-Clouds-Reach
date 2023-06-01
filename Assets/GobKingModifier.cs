using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public class GobKingModifier : MonoBehaviour
    {
        [SerializeField] private Unit unit;

        // Start is called before the first frame update
        void Start()
        {
            if(unit == null)
            {
                unit = GetComponent<Unit>();
            }
            if(PlayerPrefs.GetInt("GobKingPotPoisoned") == 1)
            {
                unit.addStatus(statusEffects.Poison, 30);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
