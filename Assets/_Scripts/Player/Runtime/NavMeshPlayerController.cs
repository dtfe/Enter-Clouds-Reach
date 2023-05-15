using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace EnterCloudsReach.Player
{
    public delegate void VoidDelegate();

    public class NavMeshPlayerController : MonoBehaviour
    {
        [Header("Referances")]
        [SerializeField] private NavMeshAgent playerAgent;

        private static List<Vector3> positions = new List<Vector3>();
        private static List<VoidDelegate> callbacks = new List<VoidDelegate>();

        public static void QueUpMovement(Vector3 Position, VoidDelegate Callback = null)
        {
            positions.Insert(positions.Count, Position);
            callbacks.Insert(callbacks.Count, Callback);
        }

        public void Update()
        {
            if (positions.Count != callbacks.Count)
            {
                Debug.LogError("Holly fuck some real bad shit is happening!");
                positions = new List<Vector3>();
                callbacks = new List<VoidDelegate>();
                return;
            }

            if (positions.Count > 0)
            {
                playerAgent.SetDestination(positions[0]);

                if (MovementFinished())
                {
                    callbacks[0]?.Invoke();

                    positions.RemoveAt(0);
                    callbacks.RemoveAt(0);
                }
            }
        }

        internal bool MovementFinished()
        {
            if (!playerAgent.pathPending)
            {
                if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
                {
                    if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude <= 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}