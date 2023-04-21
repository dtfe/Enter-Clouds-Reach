using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnterCloudsReach.Player;

namespace EnterCloudsReach.EventSystem
{
    [Event(false, 1, 1)]   
    public class MoveEvent : EventClass
    {
        [SerializeField] Animator terrainAmin;
        public override void StartEvent()
        {
            if (terrainAmin != null){
            terrainAmin.enabled = true;}
            NavMeshPlayerController.QueUpMovement(transform.position, FinishMovement);
        }

        public void FinishMovement()
        {
            EndEvent(0);
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + Vector3.down * 1000, transform.position + Vector3.up * 1000);
            Gizmos.DrawSphere(transform.position, 0.25f);
        }
    }
}
