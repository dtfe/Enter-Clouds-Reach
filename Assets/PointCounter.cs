using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace EnterCloudsReach.Player
{
    public class PointCounter : MonoBehaviour
    {
        CharacterCreator characterCreator;
        internal TMP_Text pointLeft;
        // Start is called before the first frame update
        void Start()
        {
            characterCreator = FindObjectOfType<CharacterCreator>();
            pointLeft = gameObject.GetComponent<TMP_Text>();
        }

    }
}
