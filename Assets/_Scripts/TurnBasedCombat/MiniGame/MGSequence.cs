using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    [CreateAssetMenu(fileName = "Sequence",menuName = "Minigame/Create New Sequence")]
    public class MGSequence : ScriptableObject
    {
        public List<MGPoint> points = new List<MGPoint>();
    }
}
