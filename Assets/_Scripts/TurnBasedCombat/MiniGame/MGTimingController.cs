using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        [SerializeField] private statusEffects statusToApply;
        private float curTime;
        private Renderer rend;

        //VFX stuff
        private VisualEffect vfxSelf;

        [SerializeField]private bool hasBeenClicked = false;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            vfxSelf = GetComponent<VisualEffect>();
            vfxSelf.SetFloat("TimingToHit", timeToHit);
            Destroy(gameObject, 7f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!hasBeenClicked)
            {
                hasBeenClicked = true;
                Debug.Log("Clicked");
                if (curTime < timeToHit - timeDeadzone || curTime > timeToHit + timeDeadzone)
                {
                    Miss();
                }
                else if (curTime > timeToHit - timeDeadzone && curTime < timeToHit + timeDeadzone)
                {
                    Hit();
                }
            }
        }

        private void Update()
        {
            if (curTime > timeToHit + timeDeadzone || curTime < timeToHit - timeDeadzone)
            {
                vfxSelf.SetVector4("ShieldColor", Color.red);
            }
            if(!hasBeenClicked && curTime > timeToHit + timeDeadzone )
            {
                Miss();
            }
            if (curTime > timeToHit - timeDeadzone && curTime < timeToHit + timeDeadzone)
            {
                vfxSelf.SetVector4("ShieldColor", Color.green);
                Debug.Log("Turn Green!");
            }
            curTime += Time.deltaTime;
        }

        private void Miss()
        {
            Destroy(GetComponent<Image>());
            hasBeenClicked = true;
            vfxSelf.SetBool("Clicked", true);
            vfxSelf.SendEvent("OnMiss");
            manager?.Miss(statusToApply);
            Destroy(gameObject, 1);
        }

        private void Hit()
        {
            Destroy(GetComponent<Image>());
            hasBeenClicked = true;
            vfxSelf.SetBool("Clicked", true);
            vfxSelf.SendEvent("OnHit");
            manager?.Hit();
            Destroy(gameObject, 1);
        }
    }
}
