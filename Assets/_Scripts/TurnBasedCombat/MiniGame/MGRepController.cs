using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnterCloudsReach.Combat
{
    public enum RepType
    {
        Point,
        Slide
    }
    
    public class MGRepController : MonoBehaviour
    {
        // Point Params
        private Vector3 positionToAppear;

        // Slide Params
        private Vector3 startPointToAppear;
        private Vector3 endPointToAppear;

        // General Params
        private float whenToAppear;
        private float whenToDisappear;

        public void Reveal()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
