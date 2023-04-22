using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EnterCloudsReach.Combat
{
    public class MGTimingController : MonoBehaviour, IPointerClickHandler
    {
        [Tooltip("The time from spawn and to when you have to click it")]
        public float timeToHit = 2f;
        [Tooltip("The amount of leeway the timing has in seconds")]
        public float timeDeadzone = 0.5f;
        private float curTime;

        private bool hasBeenClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            hasBeenClicked = true;
            if(curTime > timeToHit + timeDeadzone || curTime < timeToHit - timeDeadzone)
            {
                Miss();
            }
            else
            {
                Hit();
            }
        }

        private void Update()
        {
            if (hasBeenClicked)
            {
                return;
            }
            else
            {
                curTime += Time.deltaTime;
            }
        }

        private void Miss()
        {

        }

        private void Hit()
        {

        }
    }
}
