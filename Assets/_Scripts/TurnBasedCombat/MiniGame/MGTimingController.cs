using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.VFX;

namespace EnterCloudsReach.Combat
{
    public class MGTimingController : MonoBehaviour, IPointerClickHandler
    {
        public CombatMinigameManager manager;

        [Tooltip("The time from spawn and to when you have to click it")]
        [SerializeField] private float timeToHit = 2f;
        [Tooltip("The amount of leeway the timing has in seconds")]
        [SerializeField] private float timeDeadzone = 0.5f;
        private float curTime;
        private Renderer rend;

        //VFX stuff
        private VisualEffect vfxSelf;

        private bool hasBeenClicked;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            vfxSelf = GetComponent<VisualEffect>();
            vfxSelf.SetFloat("TimingToHit", timeToHit);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            List<GameObject> obj = eventData.hovered;

            Debug.Log("Clicked");
            hasBeenClicked = true;
            if(curTime > timeToHit + timeDeadzone || curTime < timeToHit - timeDeadzone)
            {
                Miss();
            }
            else if (curTime > timeToHit - timeDeadzone && curTime < timeToHit + timeDeadzone)
            {
                Hit();
            }
        }

        private void Update()
        {
            if (curTime > timeToHit + timeDeadzone || curTime < timeToHit - timeDeadzone)
            {
                vfxSelf.SetVector4("ShieldColor", Color.red);
            }
            if(curTime > timeToHit + timeDeadzone)
            {
                Miss();
            }
            else if (curTime > timeToHit - timeDeadzone && curTime < timeToHit + timeDeadzone)
            {
                vfxSelf.SetVector4("ShieldColor", Color.green);
                Debug.Log("Turn Green!");
            }

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
            Debug.Log("Missed");
            manager?.Miss();
            Destroy(gameObject);
        }

        private void Hit()
        {
            vfxSelf.SetBool("Clicked", true);
            Debug.Log("Hit!");
            manager?.Hit();
            Destroy(gameObject);
        }
    }
}
